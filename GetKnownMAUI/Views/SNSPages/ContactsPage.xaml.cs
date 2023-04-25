using System;
using System.ComponentModel;
using GetKnownMAUI.Models;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class MyContactsPage : ContentPage
    {
        //private readonly MyContactsViewModel _contactsViewModel;

        public MyContactsPage()
        {
            InitializeComponent();
            collectionView.BindingContext = MyContactsViewModel.MyContacts;
        }

        private void OnTypeButtonToggle(object sender, EventArgs e)
        {
            //make all box to transparent.
            BoxView boxView;
            foreach (var child in listTabNavbar.Children)
            {
                boxView = ((StackLayout)child).Children[1] as BoxView;
                boxView.Color = Colors.White;
            }

            //this is a important thing to get a element in a event just remeber the |as| act
            boxView = (((Button)sender).Parent as StackLayout).Children[1] as BoxView;
            Console.WriteLine(boxView.ClassId);
            boxView.Color = Colors.Gray;
        }

        private async void OnMyContactsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            var partner = e.CurrentSelection[0] as MyContacts;
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new ChatPage(partner));
        }
    }
}