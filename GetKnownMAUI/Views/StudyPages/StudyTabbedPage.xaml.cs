using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Forms_demo.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Xamarin_Forms_demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudyTabbedPage : TabbedPage
    {
        private readonly BaseViewModel vm;
        public StudyTabbedPage()
        {
            InitializeComponent();
            BindingContext = vm = BaseViewModel.GetInstance();
        }
    }
}