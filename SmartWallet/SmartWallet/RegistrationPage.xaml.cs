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
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        [Obsolete]
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //NoviTestniUser test123
            Hasher hash = new Hasher();
            ConnectionClass connection = new ConnectionClass();
            connection.OpenConnection();

            var data = connection.DataReader("select email from uporabnik");
            
            List<string> mails = new List<string>();

            while (data.Read())
            {
                mails.Add(data["email"].ToString());
            }


            if (mails.Any(x => x.ToString() == txtEmail.Text))
            {
                await DisplayAlert("Napaka", "Mail ze obstaja!", "Ok");
            }
            else
            {
                if (txtPassword.Text == txtPassCheck.Text)
                {
                    string query = $"insert into uporabnik (ime,priimek,uporabnisko_ime,email,geslo) values('{txtIme.Text}','{txtPriimek.Text}','{txtUsername.Text}','{txtEmail.Text}','{hash.GetHashString(txtPassword.Text)}')";
                    connection.ExecuteQueries(query);
                    connection.CloseConnection();
                    await DisplayAlert("Success", "Uporabnik registriran", "Ok");
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    connection.CloseConnection();
                    await DisplayAlert("Napaka", "Gesli se morata ujemati", "Ok");
                    await Navigation.PushAsync(new RegistrationPage());
                }
            }
            





        }
    }
}