﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading.Tasks;
using GetKnownMAUI.Models;
using GetKnownMAUI.Services;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        private List<FlyoutPageItem> flyoutPageItems { get; set; } = new List<FlyoutPageItem>();
        private readonly BaseViewModel _baseViewModel;

        public MenuPage()
        {
            InitializeComponent();
            _baseViewModel = BaseViewModel.GetInstance();
            BindingContext = _baseViewModel.Me;
            foreach (var item in Enum.GetValues(typeof(MenuItemType)))
            {
                flyoutPageItems.Add(new FlyoutPageItem { Id = Convert.ToInt32(item), Title = item.ToString(), IconSource = "" });
            }

            NavSNSViewCell.Tapped += async (sender, e) =>
            {
                await RootPage.NavigateFromMenu(MenuItemType.SNSTabbed);
            };
            NavStudyViewCell.Tapped += async (sender, e) =>
            {
                await RootPage.NavigateFromMenu(MenuItemType.StudyTabbed);
            };
            //ListViewMenu.ItemsSource = flyoutPageItems;
            //ListViewMenu.ItemSelected += async (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //        return;
            //    var id = ((FlyoutPageItem)e.SelectedItem).Id;
            //    await RootPage.NavigateFromMenu(id);
            //};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BaseViewModel._chatHub is not null)
                WebSocketStateLabel.Text = BaseViewModel._chatHub.GetState();

        }

        private void OnLogout(object sender, EventArgs e)
        {
            HttpRequest.Token = null;
            Application.Current.MainPage = new LoginPage();
        }

        private void OnToggleMajor(object sender, EventArgs e)
        {
            var major = ((sender as StackLayout).Children[1] as Label).Text;
            _baseViewModel.OnToggleMajor(major);
            //await RootPage.NavigateFromMenu(MenuItemType.StudyTabbed);
        }
    }
}