using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using GetKnownMAUI.ViewModels;
using System.Collections.ObjectModel;
using GetKnownMAUI.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class PostsPage : ContentPage
    {
        private readonly PostsViewModel _postsViewModel;

        public PostsPage()
        {
            InitializeComponent();
            BindingContext = _postsViewModel = new PostsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //IsRefreshing = true;
            _ = Task.Run(async () => await _postsViewModel.GetListAsync());
        }

        private async void OnPushPost(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendPostPage());
            //_ = Task.Run(async () => await _postsViewModel.GetListAsync());
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

        private async void OnFollowButtonClickAsync(object sender, EventArgs e)
        {
            var uid = ((Posts)(sender as Button).BindingContext).uid;
            var result = await MyContactsViewModel.PostAsync(partner_id: uid);
            if (result is true)
            {
                _postsViewModel.OnFollowStateChange(uid: uid);
                await DisplayAlert("关注", "关注成功", "确定");
            }
            else
            {
                await DisplayAlert("关注", "关注失败", "确定");
            }
        }
    }
}