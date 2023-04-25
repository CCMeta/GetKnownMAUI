using System;
using System.ComponentModel;
using GetKnownMAUI.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
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