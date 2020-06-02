﻿using ProgettoEsame.Model;
using ProgettoEsame.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProgettoEsame.ViewModel
{
    class SubscriptionDetailsVM : INotifyPropertyChanged
    {


        private Subscription subscription;

        public Subscription Subscription
        {
            get { return subscription; }
            set
            {
                subscription = value;
                Name = subscription.Name;
                IsActive = subscription.IsActive;
                OnPropertyChanged("Subscription");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Subscription.Name = name;
                OnPropertyChanged("Name");
                OnPropertyChanged("Subscription");
            }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                Subscription.IsActive = isActive;
                OnPropertyChanged("IsActive");
                OnPropertyChanged("Subscription");
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SubscriptionDetailsVM()
        {
            UpdateCommand = new Command(Update, UpdateCanExecute);
            DeleteCommand = new Command(Delete);
        }

        private bool UpdateCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private async void Update(object parameter)
        {
            bool result = await DatabaseHelper.UpdateSubscription(Subscription);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "There was an error, please try again", "Ok");
        }

        private async void Delete(object parameter)
        {
            bool result = await DatabaseHelper.DeleteSubscription(Subscription);

            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "There was an error, please try again", "Ok");
        }



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}