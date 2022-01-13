using SmartWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        [Obsolete]
        public ForgotPasswordPage()
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

        public string RandomString(int length)
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            //TODO
            //nared da poslje reset link ko bos vedu kak
            ConnectionClass connection = new ConnectionClass();
            connection.OpenConnection();

            var data = connection.DataReader($"select email from uporabnik where email = '{txtEmail.Text}'");
            data.Read();
            connection.CloseConnection();

            string tempPass = RandomString(8);

            Hasher hash = new Hasher();

            connection.OpenConnection();
            connection.ExecuteQueries($"update uporabnik set geslo = '{hash.GetHashString(tempPass)}' where email = '{txtEmail.Text}'");
            connection.CloseConnection();

            await DisplayAlert("Bravo", $"Changed pass {tempPass} {hash.GetHashString(tempPass)}!", "ok");
            await Navigation.PushAsync(new LoginPage());

        }
    }
}