using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GetKnownMAUI.Models;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectsPage : ContentPage
    {
        public SubjectsViewModel SubjectsViewModel;

        public SubjectsPage()
        {
            InitializeComponent();
            SubjectsViewModel = new SubjectsViewModel();
            BindingContext = SubjectsViewModel;
        }


        async void OnSelectionItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            var selected = e.CurrentSelection[0] as Subjects;
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new SubjectPage(selected));
        }

        public void OnRemainingItemsThresholdReached(object sender, EventArgs e)
        {
            Console.WriteLine("fuck this OnRemainingItemsThresholdReached ");
            //SubjectsViewModel.GetSubjectsAsync();
        }
    }
}
