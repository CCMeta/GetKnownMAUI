using System;
using System.ComponentModel;
using GetKnownMAUI.Models;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class DataIndexPage : ContentPage
    {
        private readonly ExamTranscriptsViewModel _examTranscriptsViewModel;
        public DataIndexPage()
        {
            InitializeComponent();
            Title = "数据中心";
            BindingContext = _examTranscriptsViewModel = new ExamTranscriptsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _examTranscriptsViewModel.GetListAsync();
        }

        private async void OnEnterExamTranscriptsPageAsync(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            (sender as CollectionView).SelectedItem = null;
            ExamTranscripts transcripts = e.CurrentSelection[0] as ExamTranscripts;
            await Navigation.PushAsync(new ExamTranscriptsPage(transcripts.id, transcripts.title));
        }

        private void OnTypeButtonToggle(object sender, EventArgs e)
        {
            //make all box to transparent.
            BoxView boxView;
            foreach (var child in listTabNavbar.Children)
            {
                boxView = ((StackLayout)child).Children[1] as BoxView;
                boxView.Color = Colors.Transparent;
            }

            //this is a important thing to get a element in a event just remeber the |as| act
            boxView = (((Button)sender).Parent as StackLayout).Children[1] as BoxView;
            Console.WriteLine(boxView.ClassId);
            boxView.Color = Colors.Gray;
        }

    }
}