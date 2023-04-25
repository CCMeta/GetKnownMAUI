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

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        private readonly Action SetMainPage;

        public LoginPage()
        {
            InitializeComponent();
            SetMainPage = () => Application.Current.MainPage = new MainPage();

            var AppConfiguration = Services.AppConfiguration.GetInstence();
            var httpClient = new HttpRequest(AppConfiguration["Host"]);
            Task.Run(async () =>
            {
                Console.WriteLine(httpClient._host);
                var users = await httpClient.GetAsync<List<Users>>("/api/token", new Dictionary<string, string>());
                Console.WriteLine(users.Count);
                collectionView.BindingContext = users;
            }).Wait();
        }

        private void OnLoginClick(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            MessageLabel.Text = "LOADING...";
            collectionView.IsVisible = false;
            collectionView.SelectedItem = null;
            BaseViewModel.GetInstance().OnLogin(username: (e.CurrentSelection[0] as Users).username);
            MainThread.BeginInvokeOnMainThread(SetMainPage);
        }

    }
}