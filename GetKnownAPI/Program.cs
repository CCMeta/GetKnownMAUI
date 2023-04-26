using GetKnownAPI.Models;
using GetKnownAPI.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.Encodings.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<SessionService>()
                .AddTransient<KnowledgesRepository>()
                .AddTransient<ExamTranscriptsRepository>()
                .AddTransient<ExamAnswersRepository>()
                .AddTransient<ExamQuestionsRepository>()
                .AddTransient<ExamsRepository>()
                .AddTransient<SubjectsRepository>()
                .AddTransient<CoursesRepository>()
                .AddTransient<ContactsRepository>()
                .AddTransient<ChatsRepository>()
                .AddTransient<UsersRepository>()
                .AddTransient<PostsRepository>();


builder.Services.AddSingleton<IUserIdProvider, ChatHubUserProvider>();
builder.Services.AddSingleton<ChatHub>();
builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

//SESSION 
app.UseMiddleware<AuthMiddleware>();
//SESSION CHECK
app.UseWhen(
    predicate: context =>
    {
        return context.Request.Path.Value is null || (context.Request.Path.Value.ToLower().Contains("/maui_api") && context.Request.Path.Value.ToLower() != "/maui_api/token");
    },
    configuration: static (IApplicationBuilder app) =>
    {
        app.Use(async (context, next) =>
        {
            if (!context.Items.ContainsKey("uid"))
            {
                context.Response.StatusCode = 401;
                await context.Response.CompleteAsync();
            }
            else
            {
                await next.Invoke();
            }
        });
    });

/**
 * Be care with nginx proxy_pass options, especially [Connection "upgrade"]
 * location /maui_chathub {
      proxy_pass http://localhost:5000;

      # Configure WebSockets
      proxy_set_header Upgrade $http_upgrade;
      proxy_set_header Connection "upgrade";
      proxy_cache_bypass $http_upgrade;

      # Configure ServerSentEvents
      proxy_buffering off;

      # Configure LongPolling
      proxy_read_timeout 100s;

      proxy_set_header Host $host;
   }
 */
app.MapHub<ChatHub>("/maui_chathub");

app.UseEndpoints(endpoints =>
{
    //endpoints.MapHub<ChatHub>("/maui_chathub");
    endpoints.MapControllers();
});

app.Run();
