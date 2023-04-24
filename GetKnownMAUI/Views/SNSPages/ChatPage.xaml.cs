using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin_Forms_demo.Models;
using Xamarin_Forms_demo.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Xamarin_Forms_demo.Views
{
    [DesignTimeVisible(false)]
    public partial class ChatPage : ContentPage
    {
        private readonly ChatsViewModel _chatsViewModel;
        private readonly MyContacts _partner;

        public ChatPage(MyContacts partner)
        {
            InitializeComponent();
            _partner = partner;
            Title = _partner.nickname;
            BindingContext = _chatsViewModel = new ChatsViewModel(_partner);
            Task.Run(async () =>
            {
                await ChatsViewModel.GetListRemoteAsync(_partner.partner_id);
                await _chatsViewModel.GetListAsync();
                //从本地数据库 读取的内容标记已读

            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnEnterMyContactsAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyContactsPage());
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            //HeightRequest = HeightRequest / 2;
        }

        private async void OnSendMessage(object sender, EventArgs e)
        {
            await _chatsViewModel.PostAsync(_partner.partner_id, ((Entry)sender).Text);
            ((Entry)sender).Text = null;
        }
    }
}