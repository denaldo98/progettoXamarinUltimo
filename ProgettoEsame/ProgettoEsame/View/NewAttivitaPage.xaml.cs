﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgettoEsame.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAttivitaPage : ContentPage
    {
        public NewAttivitaPage()
        {
            InitializeComponent();
            Title = "Inserimento Attività";
            scadenza.MinimumDate = DateTime.Today;
        }
    }
}