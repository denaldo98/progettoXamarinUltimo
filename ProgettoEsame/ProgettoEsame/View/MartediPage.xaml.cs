﻿using ProgettoEsame.ViewModel;
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
    public partial class MartediPage : ContentPage
    {
        MartediVM vm;

        public MartediPage()
        {
            InitializeComponent();

            vm = Resources["vm"] as MartediVM; //accedo alla risorsa tramite chiave
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.ReadMartedi();  
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NewMartediPage());
        }
    }
}