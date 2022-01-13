using SmartWallet.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
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
