﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using System.Diagnostics;

namespace VendingMachineX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Menu());
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
