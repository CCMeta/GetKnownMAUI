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
    public partial class ExamAnswersPage : ContentPage
    {
        private readonly ExamQuestionsViewModel _examQuestionsViewModel;

        public ExamAnswersPage(ExamQuestionsViewModel examQuestionsViewModel)
        {
            InitializeComponent();
            BindingContext = _examQuestionsViewModel = examQuestionsViewModel;
        }

        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            (sender as CollectionView).SelectedItem = null;
            //int current_id = ((ExamQuestions)e.CurrentSelection[0]).id;
            int position = _examQuestionsViewModel.ExamQuestions.IndexOf((ExamQuestions)e.CurrentSelection[0]);
            if (position < 0)
            {
                throw new Exception($"The position of OnItemSelected is {position}");
            }
            var ExamQuestionsPage = Navigation.NavigationStack.First(q => q.GetType() == typeof(ExamQuestionsPage)) as ExamQuestionsPage;
            ExamQuestionsPage.SetCurrentPosition(position);
            Navigation.PopAsync();
        }

        private async void OnCommitPaperAsync(object sender, EventArgs e)
        {
            Content.IsEnabled = false;
            var examAnswers = _examQuestionsViewModel.examAnswers;
            var _examAnswersViewModel = new ExamAnswersViewModel();

            //This using is super cool for async var to go with life before not async expressions.
            using Task<int> result = _examAnswersViewModel.PostListAsync(examAnswers.ToArray());
            int transcriptId = ((await result) < 0) ? throw new Exception("ExamAnswersViewModel.PostListAsync") : await result;

            await DisplayAlert("提交结果", "提交成功", "确定");
            Content.IsEnabled = true;

            Page MainPage = Navigation.NavigationStack.ElementAt(0);
            await Navigation.PopToRootAsync(animated: false);
            await MainPage.Navigation.PushAsync(new ExamTranscriptsPage(transcriptId, _examQuestionsViewModel.Title));
        }
    }
}