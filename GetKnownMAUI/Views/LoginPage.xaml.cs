﻿using System;
using System.ComponentModel;
using GetKnownMAUI.ViewModels;
using GetKnownMAUI.Models;
using Microsoft.Extensions.Configuration;
using GetKnownMAUI.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        //private readonly Action SetMainPage;
        private List<Users> users;

        public LoginPage()
        {
            InitializeComponent();
            //SetMainPage = () => Application.Current.MainPage = new MainPage();

            var AppConfiguration = Services.AppConfiguration.GetInstence();
            var httpClient = new HttpRequest(AppConfiguration["HOST"]);
            Task.Run(async () =>
            {
                Debug.WriteLine($"httpClient._host = {httpClient._host}");
                users = await httpClient.GetAsync<List<Users>>("/maui_api/token", new Dictionary<string, string>());
                Debug.WriteLine($"users.Count = {users.Count}");
                collectionView.BindingContext = users;
            }).Wait();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var need_auto_login = true;
            //need_auto_login = false;
            if (need_auto_login && users.Count > 0)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    BaseViewModel.GetInstance().OnLogin("wy");
                    await MainThread.InvokeOnMainThreadAsync(() => Application.Current.MainPage = new MainPage());
                });
            }
        }

        private void OnLoginClick(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            MessageLabel.Text = "LOADING...";
            collectionView.IsVisible = false;
            collectionView.SelectedItem = null;
            BaseViewModel.GetInstance().OnLogin(username: (e.CurrentSelection[0] as Users).username);
            //Navigation.PushAsync(new MainPage());

            MainThread.BeginInvokeOnMainThread(
                 () => Application.Current.MainPage = new MainPage()
            );
        }

    }
}