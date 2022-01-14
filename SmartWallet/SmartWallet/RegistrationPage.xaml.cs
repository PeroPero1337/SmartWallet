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

        private bool checkPolja()
        {

            if (String.IsNullOrEmpty(txtIme.Text) || String.IsNullOrWhiteSpace(txtIme.Text))
                return false;
            else if (String.IsNullOrEmpty(txtPriimek.Text) || String.IsNullOrWhiteSpace(txtPriimek.Text))
                return false;
            else if (String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrWhiteSpace(txtEmail.Text))
                return false;
            else if (String.IsNullOrEmpty(txtUsername.Text) || String.IsNullOrWhiteSpace(txtUsername.Text))
                return false;
            else if (String.IsNullOrEmpty(txtPassword.Text) || String.IsNullOrWhiteSpace(txtPassword.Text))
                return false;
            else if (String.IsNullOrEmpty(txtPassCheck.Text) || String.IsNullOrWhiteSpace(txtPassCheck.Text))
                return false;
            else
                return true;



        }

        [Obsolete]
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //NoviTestniUser test123
            try
            {
                Hasher hash = new Hasher();
                ConnectionClass connection = new ConnectionClass();

                connection.OpenConnection();

                var data = connection.DataReader("select email from uporabnik");
            
                List<string> mails = new List<string>();

                while (data.Read())
                {
                    mails.Add(data["email"].ToString());
                }
                connection.CloseConnection();

                if (checkPolja())
                {
                    if (mails.Any(x => x.ToString() == txtEmail.Text))
                        await DisplayAlert("Napaka", "Ta email že obstaja!", "Ok");
                    else
                    {
                        if (txtPassword.Text == txtPassCheck.Text)
                        {
                            if (radioTOU.IsChecked)
                            {
                                connection.OpenConnection();
                                string query = $"insert into uporabnik (ime,priimek,uporabnisko_ime,email,geslo) values('{txtIme.Text}','{txtPriimek.Text}','{txtUsername.Text}','{txtEmail.Text}','{hash.GetHashString(txtPassword.Text)}')";
                                if(connection.ExecuteQueries2(query))
                                {
                                    await DisplayAlert("Uspešno", "Uporabnik registriran!", "OK");
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Napaka!", "Uporabnik ni bil registriran!", "OK");
                                }
                                connection.CloseConnection();
                                
                            }
                            else
                            {
                                await DisplayAlert("Napaka", "Prosim strinjajte se z pogoji uporabe", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Napaka", "Gesli se morata ujemati", "OK");
                        }      
                    }
                }
                else
                {
                    await DisplayAlert("Napaka", "Obrazec ni pravilno izpolnjen", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Obvestilo", "Nepričakovana napaka! Poskusite ponovno kasneje!", "Ok");
            }



        }
    }
}