﻿using ProgettoEsame.Model;
using ProgettoEsame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgettoEsame.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CorsoDetailsPage2 : ContentPage
    {
        String idcorso;
        Corso corso;
        CorsoDetailsVM2 vm;
        public CorsoDetailsPage2()
        {
            InitializeComponent();

            vm = Resources["vm"] as CorsoDetailsVM2;
            
        }

        public CorsoDetailsPage2(Corso selectedCorso)
        {
            InitializeComponent();
            Title = "Visualizzazione Corso";
            vm = Resources["vm"] as CorsoDetailsVM2;
            vm.Corso = selectedCorso;
            corso = selectedCorso;
            idcorso = selectedCorso.Id;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.ReadAppunti();
        }


        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NewAppuntoPage(idcorso));
        }

        void DettaglioCorsoClicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CorsoDetailsPage(corso));
        }

    }
}