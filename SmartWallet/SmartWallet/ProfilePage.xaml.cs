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


            connection.OpenConnection();
            string queryU = $"SELECT strategije_varcevanja_idstrategije_varcevanja FROM uporabnik WHERE iduporabnik = {Uporabnik._id}";
            var strategija = connection.DataReader(queryU);
            strategija.Read();
            string id = strategija["strategije_varcevanja_idstrategije_varcevanja"].ToString();
            StrategijeVarcevanje.setVarcevanje(Convert.ToInt32(id));
            connection.CloseConnection();
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            txtPrisparano.Text = "Vrednost: ";
            txtPorabljeno.Text = "Vrednost: ";
            var userId = Uporabnik._id;
            
            var datum_porabljeno_od = datePickerPorabljenoSkupajOD.Date.ToString("yyyy-MM-dd");
            var datum_porabljeno_do = datePickerPorabljenoSkupajDO.Date.ToString("yyyy-MM-dd");

            var datum_prisparano_od = datePickerPrisparanoSkupajOD.Date.ToString("yyyy-MM-dd");
            var datum_prisparano_do = datePickerPrisparanoSkupajDO.Date.ToString("yyyy-MM-dd");

            //porabljeno
            
            decimal porabljeno = 0;
            connection.OpenConnection();
            var odhodki = connection.DataReader($"select cena from podatek where datum between '{datum_porabljeno_od}' and '{datum_porabljeno_do}' and status_idstatus = 1 and uporabniki_iduporabniki = {Uporabnik._id}");
            while (odhodki.Read())
            {
                porabljeno += decimal.Parse(odhodki["cena"].ToString());
            }
            connection.CloseConnection();

            txtPorabljeno.Text += " " +porabljeno.ToString("#.##")+" €";

            //prisparano
            decimal prisparano = 0;

            decimal kes_dobljen = 0;
            connection.OpenConnection();
            var prihodki = connection.DataReader($"select cena from podatek where datum between '{datum_prisparano_od}' and '{datum_prisparano_do}' and status_idstatus = 2 and uporabniki_iduporabniki = {Uporabnik._id}");
            while (prihodki.Read())
            {
                kes_dobljen += decimal.Parse(prihodki["cena"].ToString());
            }
            connection.CloseConnection();

            connection.OpenConnection();
            var procent = connection.DataReader($"select procent_varcevanja from strategije_varcevanja where idstrategije_varcevanja = {StrategijeVarcevanje._id}");
            procent.Read();
            var x = decimal.Parse(procent["procent_varcevanja"].ToString());
            connection.CloseConnection();


            prisparano = kes_dobljen / 100 * x;

            txtPrisparano.Text += " " + prisparano.ToString("#.##") + " €";


        }

        private async void btnChangePass_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePassword());
        }
    }
}