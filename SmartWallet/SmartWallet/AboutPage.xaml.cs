﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        [Obsolete]
        public AboutPage()
        {
            InitializeComponent();
            linkPrivacy.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkPrivacy_Clicked()));
            linkTerms.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkTerms_Clicked()));
            linkAbout.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkAbout_Clicked()));
        }

        [Obsolete]
        private async void linkPrivacy_Clicked()
        {
            await Navigation.PushAsync(new PrivacyPage());
        }

        [Obsolete]
        private async void linkTerms_Clicked()
        {
            await Navigation.PushAsync(new TermsPage());

        }

        [Obsolete]
        private async void linkAbout_Clicked()
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}