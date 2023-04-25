using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

using GetKnownMAUI.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : FlyoutPage
    {
        readonly Dictionary<MenuItemType, NavigationPage> MenuPages = new();
        public MainPage()
        {
            InitializeComponent();
            FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
            // define index page
            MenuPages.Add(MenuItemType.StudyTabbed, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(MenuItemType id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                MenuPages.Add(id, new NavigationPage(FlyoutPageItem.GetPageById(id)));
            }

            var newPage = MenuPages[id];
            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;
                if (DeviceInfo.Platform == DevicePlatform.Android)
                    await Task.Delay(100);
                IsPresented = false;
            }
        }
    }
}