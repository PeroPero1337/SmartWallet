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
    public partial class ProfilePage : ContentPage
    {
        public ConnectionClass connection;
        public ProfilePage()
        {
            InitializeComponent();
            connection = new ConnectionClass();
            connection.OpenConnection();
            string query = $"select uporabnisko_ime, ime, priimek, email from uporabnik where iduporabnik = {Uporabnik._id}";
            var data = connection.DataReader(query);
            data.Read();
            txtGreetings.Text = $"Pozdravljeni {data["ime"].ToString()} {data["priimek"].ToString()}";
            txtGreetingsEmail.Text = $"Vaš e-naslov: {data["email"].ToString()}";
            txtGreetingsUname.Text = $"Vaše uporabniško ime: {data["uporabnisko_ime"].ToString()}";
            connection.CloseConnection();
        }

        private decimal GetPrisparano()
        {
            return 0;
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            var userId = Uporabnik._id;
            
            var datum_porabljeno_od = datePickerPorabljenoSkupajDO.Date.ToString("yyyy-MM-dd");
            var datum_porabljeno_do = datePickerPorabljenoSkupajOD.Date.ToString("yyyy-MM-dd");

            var datum_prisparano_od = datePickerPrisparanoSkupajDO.Date.ToString("yyyy-MM-dd");
            var datum_prisparano_do = datePickerPrisparanoSkupajOD.Date.ToString("yyyy-MM-dd");

            //porabljeno
            
            decimal porabljeno = 0;
            connection.OpenConnection();
            var data = connection.DataReader($"select cena from podatek where datum between '{datum_porabljeno_do}' and '{datum_prisparano_do}' and status_idstatus = 1 and uporabniki_iduporabniki = {Uporabnik._id}");
            while (data.Read())
            {
                porabljeno += decimal.Parse(data["cena"].ToString());
            }
            connection.CloseConnection();

            txtPorabljeno.Text += " " +porabljeno.ToString("#.##")+" €";

            //prisparano


        }

        private async void btnChangePass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }
    }
}