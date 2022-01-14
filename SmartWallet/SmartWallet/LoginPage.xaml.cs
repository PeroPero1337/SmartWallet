using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartWallet.Models;
using MySqlConnector;

namespace SmartWallet
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //public ICommand ForgotPasswordCommand => new Command(linkLostPass_Clicked); -> kao bulši način sam ne dela lol?

        public ConnectionClass conn;
        [Obsolete] // -> zarad TapGestureRecognizer ker se kao to ne uporabla vec ampak dela
        public LoginPage()
        {
            
            InitializeComponent();
            conn = new ConnectionClass();
            linkLostPass.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkLostPass_Clicked()));
            linkPrivacy.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkPrivacy_Clicked()));
            linkTerms.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkTerms_Clicked()));
            linkAbout.GestureRecognizers.Add(new TapGestureRecognizer((view) => linkAbout_Clicked()));
            
        }


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            Hasher hash = new Hasher();
            try
            {

                //test login
                conn.OpenConnection();

                var data = conn.DataReader($"select iduporabnik, uporabnisko_ime, geslo from uporabnik where uporabnisko_ime = '{txtUsername.Text}'");
                data.Read();
                

                if (txtUsername.Text == data["uporabnisko_ime"].ToString() && hash.GetHashString(txtPassword.Text) == data["geslo"].ToString())
                {
                    
                    Uporabnik.SetUser(int.Parse(data["iduporabnik"].ToString()));
                    conn.CloseConnection();
                    await Navigation.PushAsync(new MainPage());
                    txtUsername.Text = null;
                    txtPassword.Text = null;
                }
                else
                    await DisplayAlert("Neuspešna prijava", "Napačno uporabniško ime ali geslo!", "Zapri");



            }
            catch
            {
                await DisplayAlert("Obvestilo", "Nepričakovana napaka! Poskusite ponovno kasneje!", "Ok");
            }
            


        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        [Obsolete]
        private async void linkLostPass_Clicked()
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
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