using System;
using System.ComponentModel;
using GetKnownMAUI.Models;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class ClassIndexPage : ContentPage
    {
        private readonly CoursesViewModel _coursesViewModel;

        public ClassIndexPage()
        {
            InitializeComponent();
            BindingContext = _coursesViewModel = new CoursesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //IsBusy = true;
            _coursesViewModel.GetListAsync();
        }

        private async void OnCoursesSelectedAsync(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            string videoUrl = (e.CurrentSelection[0] as Courses).video;
            (sender as CollectionView).SelectedItem = null;
            await Task.Run(() => { });
            //await Navigation.PushAsync(new VideoPage(videoUrl));
            //await Navigation.PushModalAsync(new VideoPage(videoUrl));
        }
    }
}