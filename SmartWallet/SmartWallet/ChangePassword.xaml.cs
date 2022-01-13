using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartWallet.Models;

namespace SmartWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        ConnectionClass connection;
        public ChangePassword()
        {
            InitializeComponent();
            connection = new ConnectionClass();
        }

        private async void btnChange_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                await DisplayAlert("Obvestilo", "Prosim vnesite novo geslo!", "OK");
            else
            {
                Hasher hash = new Hasher();
                connection.OpenConnection();
                var userId = Uporabnik._id;

                string query = $"update uporabnik set geslo = '{hash.GetHashString(txtPassword.Text)}' where iduporabnik = {userId}";

                await DisplayAlert("Obvestilo", "Vaše geslo je spremenjeno!", "ok");

                connection.ExecuteQueries(query);
                connection.CloseConnection();

                await this.Navigation.PopAsync();
            }
        }
    }
}