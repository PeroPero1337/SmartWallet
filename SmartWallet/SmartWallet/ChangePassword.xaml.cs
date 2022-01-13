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
            Hasher hash = new Hasher();
            connection.OpenConnection();
            var userId = Uporabnik._id;

            string query = $"update uporabnik set geslo = '{hash.GetHashString(txtPassword.Text)}' where iduporabnik = {userId}";

            await DisplayAlert("Password changed", "Vaše geslo je spremenjeno!", "ok");

            connection.ExecuteQueries(query);
            connection.CloseConnection();

            await this.Navigation.PopAsync();
        }
    }
}