using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GetKnownMAUI.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.ViewModels
{
    public class SubjectsViewModel : BaseViewModel
    {
        private readonly string path = "/maui_api/subjects";
        public ObservableCollection<Subjects> subjects = new ObservableCollection<Subjects>();
        public ObservableCollection<Subjects> Subjects
        {
            get { return subjects; }
            set
            {
                foreach (var item in value)
                {
                    subjects.Add(item);
                }
            }
        }
        public ICommand GetSubjectsCommand { protected set; get; }

        public SubjectsViewModel()
        {
            Title = "SubjectsViewModel";
            GetSubjectsCommand = new Command(() =>
            {
                GetSubjectsAsync();
            });
            GetSubjectsAsync();
        }

        public async void GetSubjectsAsync()
        {
            if (IsWorking)
                return;
            IsWorking = true;
            int maxId = Subjects.Count > 0 ? Subjects[0].id : 0;
            var queryParams = new Dictionary<string, string>() {
                    { "p",maxId.ToString() }
            };
            Subjects = await HttpRequest.GetAsync<ObservableCollection<Subjects>>(path, queryParams: queryParams);
            IsWorking = false;
        }

        public ICommand OpenWebCommand { get; }
    }
}