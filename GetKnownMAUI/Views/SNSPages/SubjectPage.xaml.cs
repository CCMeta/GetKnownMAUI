using System;
using System.ComponentModel;
using Xamarin_Forms_demo.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Xamarin_Forms_demo.Views
{
    [DesignTimeVisible(false)]
    public partial class SubjectPage : ContentPage
    {
        public SubjectPage(Subjects subject)
        {
            InitializeComponent();
            Title = subject.vname;
            BindingContext = subject;
        }
    }
}