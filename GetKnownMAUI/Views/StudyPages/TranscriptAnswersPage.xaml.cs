using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GetKnownMAUI.Models;
using GetKnownMAUI.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.Views
{
    [DesignTimeVisible(false)]
    public partial class TranscriptAnswersPage : ContentPage
    {
        private readonly ExamAnswersViewModel _examAnswersViewModel;

        public TranscriptAnswersPage(ExamAnswersViewModel ExamAnswersViewModel)
        {
            InitializeComponent();
            BindingContext = _examAnswersViewModel = ExamAnswersViewModel;
        }

        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            (sender as CollectionView).SelectedItem = null;
            //int current_id = ((ExamQuestions)e.CurrentSelection[0]).id;
            int position = _examAnswersViewModel.ExamAnswers.IndexOf((ExamAnswers)e.CurrentSelection[0]);
            if (position < 0)
            {
                throw new Exception($"The position of OnItemSelected is {position}");
            }
            var ExamQuestionsPage = Navigation.NavigationStack.First(q => q.GetType() == typeof(ExamTranscriptsPage)) as ExamTranscriptsPage;
            ExamQuestionsPage.SetCurrentPosition(position);
            Navigation.PopAsync();
        }
    }
}