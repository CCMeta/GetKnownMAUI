﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GetKnownMAUI.Models;
using GetKnownMAUI.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace GetKnownMAUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static ChatHub _chatHub;
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        private readonly static IConfiguration _appConfiguration = Services.AppConfiguration.GetInstence();
        public static IConfiguration AppConfiguration => _appConfiguration;

        private static readonly HttpRequest _httpRequest = new(AppConfiguration["HOST"]);
        public static HttpRequest HttpRequest { get => _httpRequest; }

        private bool isWorking = false;
        public bool IsWorking
        {
            get { return isWorking; }
            set { SetProperty(ref isWorking, value); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private Users me;
        public Users Me
        {
            get => me;
            set => SetProperty(ref me, value);
        }

        public string currentMajor = "Unknown";
        public string CurrentMajor
        {
            get => currentMajor;
            set => SetProperty(ref currentMajor, value);
        }

        private static BaseViewModel _instance;
        public static BaseViewModel GetInstance()
        {
            _instance ??= new BaseViewModel();
            return _instance;
        }

        protected BaseViewModel() { }

        public void OnLogin(string username = null)
        {
            string _username = username is null ?
                AppConfiguration["USERNAME"] : username;
            var _password = AppConfiguration["PASSWORD"];
            Login(_username, _password);
        }

        protected void Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(HttpRequest.Token))
                return;
            var identity = new Users { username = username, password = password };

            var user = Task.Run(async () => await HttpRequest.PostAsync("/maui_api/token", identity)).Result;
            if (string.IsNullOrEmpty(user.username))
                throw new Exception($"No token responsed result = {user}");

            //check success setting token 
            HttpRequest.Token = user.token;
            Debug.WriteLine($"HttpRequest.Token = {HttpRequest.Token}");
            Me = user;

            //init local message signalr
            _chatHub = new ChatHub(AppConfiguration["HOST"] + "/maui_chathub", Me.token);

            //init contacts
            MyContactsViewModel.GetListAsync();
        }

        public void OnToggleMajor(string major)
        {
            CurrentMajor = major;
        }

        //Tookit region begin
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
