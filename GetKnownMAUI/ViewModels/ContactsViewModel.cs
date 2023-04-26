using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GetKnownMAUI.Models;
using GetKnownMAUI.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.ViewModels
{
    public class MyContactsViewModel : BaseViewModel
    {
        private static readonly string path = "/maui_api/contacts";
        public static ObservableCollection<MyContacts> contacts = new ObservableCollection<MyContacts>();
        public static ObservableCollection<MyContacts> MyContacts
        {
            get => contacts;
            set
            {
                foreach (var item in value)
                {
                    item.state = "offline";
                    contacts.Add(item);
                }
            }
        }
        public static ICommand GetListCommand { protected set; get; }

        static MyContactsViewModel()
        {
            MessagingCenter.Subscribe<ChatHub, KeyValuePair<string, string>>(_chatHub, MessageType.OnEventChatSend.ToString(),
                (sender, arg) => GetListAsync());

            MessagingCenter.Subscribe<ChatHub, KeyValuePair<string, string>>(_chatHub, MessageType.OnEventOnline.ToString(),
                (sender, arg) => OnEventOnlinehandler(arg.Key, arg.Value));
            //if OnEventChatSend and user at chat GUI,then should fresh
        }

        private static void OnEventOnlinehandler(string caller, string message)
        {
            var item = MyContacts.FirstOrDefault(i => i.partner_id == int.Parse(caller));
            var index = MyContacts.IndexOf(item);
            item.state = message;
            MyContacts[index] = item;
        }

        public static async void GetListAsync()
        {
            var queryParams = new Dictionary<string, string>() { };
            var result = await HttpRequest.GetAsync<ObservableCollection<MyContacts>>(path, queryParams: queryParams);
            // getallchats of remote and 写到本地数据库
            // 在本地数据库统计所有未读的信息 写到badge上
            contacts.Clear();
            MyContacts = result;
        }

        public static async Task<bool> PostAsync(int partner_id)
        {
            MyContacts queryParams = new MyContacts
            {
                partner_id = partner_id
            };
            var result = await HttpRequest.PostAsync(path, queryParams);
            if (result is MyContacts)
                return true;
            return false;
        }
    }
}