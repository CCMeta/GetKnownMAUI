﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GetKnownMAUI.Models;
using GetKnownMAUI.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GetKnownMAUI.ViewModels
{
    public class ExamTranscriptsViewModel : BaseViewModel
    {
        private readonly string path = "/maui_api/ExamTranscripts";
        public ObservableCollection<ExamTranscripts> examTranscripts = new ObservableCollection<ExamTranscripts>();
        public ObservableCollection<ExamTranscripts> ExamTranscripts
        {
            get { return examTranscripts; }
            set
            {
                foreach (var item in value)
                {
                    examTranscripts.Insert(0, item);
                }
            }
        }
        public ICommand GetListCommand { protected set; get; }

        public ExamTranscriptsViewModel()
        {
            Title = "ExamTranscriptsViewModel";
            GetListCommand = new Command(() =>
            {
                GetListAsync();
            });
        }

        public async void GetListAsync()
        {
            int maxId = ExamTranscripts.Count > 0 ? ExamTranscripts[0].id : 0;
            var queryParams = new Dictionary<string, string>() {
                    { "p",maxId.ToString() }
            };
            ExamTranscripts = await HttpRequest.GetAsync<ObservableCollection<ExamTranscripts>>(path, queryParams: queryParams);
            IsWorking = false;
        }
    }
}