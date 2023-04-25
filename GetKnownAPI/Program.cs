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
builder.Services.AddSignalR();
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
        return context.Request.Path.Value is null || (context.Request.Path.Value.ToLower().Contains("/api") && context.Request.Path.Value.ToLower() != "/api/token");
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
    endpoints.MapControllers();
});

app.Run();
