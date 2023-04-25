using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GetKnownMAUI.Models;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class SessionsPage : ContentPage
    {
        //private readonly ChatSessionsViewModel _chatSessionsViewModel;

        public SessionsPage()
        {
            InitializeComponent();
            collectionView.BindingContext = MyContactsViewModel.MyContacts;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //select all in database chats and find unread items grouping
        }

        private async void OnEnterMyContactsAsync(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ChatPage(partner_id: 2));
            await Navigation.PushAsync(new MyContactsPage());
        }

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            var partner = e.CurrentSelection[0] as MyContacts;
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new ChatPage(partner));
        }
    }
}