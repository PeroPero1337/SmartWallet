using SmartWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataPage : ContentPage
    {
        public ConnectionClass connection;
        public DataPage()
        {
            InitializeComponent();
            connection = new ConnectionClass();

            connection.OpenConnection();
            string query = $"select uporabnisko_ime, ime, priimek, email from uporabnik where iduporabnik = {Uporabnik._id}";
            var data2 = connection.DataReader(query);
            data2.Read();
            greetTxt.Text = $"Dodajanje podatkov za osebo: {data2["ime"].ToString()} {data2["priimek"].ToString()}";
            greetTxt.FontSize = 18;
            
            connection.CloseConnection();

            connection.OpenConnection();

            var data = connection.DataReader("select idkategorija, ime_kategorije from kategorija");
            List<string> kategorija = new List<string>();

            while (data.Read())
            {
                //podatki.Add(data["ime_podatek"].ToString());
                kategorija.Add(data["ime_kategorije"].ToString());
            }
            connection.CloseConnection();

            //foreach (var item in podatki)
            //{
            //    pickerStroski.Items.Add(item);
            //}

            foreach (var item in kategorija)
            {
                pickerKategorija.Items.Add(item);
            }

            

            


        }

        //1 - strosek 2 - prihodek
        private void btnAddFixStroski_Clicked(object sender, EventArgs e)
        {

            var datum = datePickerStroski.Date.ToString("yyyy-MM-dd");

            connection.OpenConnection();
            var data = connection.DataReader($"select idkategorija from kategorija where ime_kategorije = '{pickerKategorija.SelectedItem.ToString()}'");
            data.Read();
            var kategorija_id = data["idkategorija"].ToString();
            connection.CloseConnection();


            connection.OpenConnection();
            string query = $"insert into podatek (ime_podatek,cena,datum,uporabniki_iduporabniki,status_idstatus,kategorija_idkategorija) values('{txtStrosek.Text}','{Convert.ToDecimal(txtVrednostStrosek.Text.ToString())}','{datum}','{Uporabnik._id}',1,'{int.Parse(kategorija_id)}')";

            if (connection.ExecuteQueries2(query) == true)
                DisplayAlert("Uspešno!", "Strošek je bil uspešno dodan!", "Zapri");
            else
                DisplayAlert("Napaka!", "Prišlo je do napake!", "Zapri");


            connection.CloseConnection();

            pickerKategorija.SelectedIndex = -1;
            txtStrosek.Text = "";
            txtVrednostStrosek.Text = "";
        }

        private void btnAddFixPrihodki_Clicked(object sender, EventArgs e)
        {
            var datum = datePickerPrihodki.Date.ToString("yyyy-MM-dd");

            connection.OpenConnection();

            string query = $"insert into podatek (ime_podatek,cena,datum,uporabniki_iduporabniki,status_idstatus,kategorija_idkategorija) values('{txtPrihodek.Text}','{Convert.ToDecimal(txtVrednostPrihodek.Text.ToString())}','{datum}','{Uporabnik._id}',2,null)";

            if (connection.ExecuteQueries2(query) == true)
                DisplayAlert("Uspešno!", "Prihodek je bil uspešno dodan!", "Zapri");
            else
                DisplayAlert("Napaka!", "Prišlo je do napake!", "Zapri");

            connection.CloseConnection();

            txtPrihodek.Text = "";
            txtVrednostPrihodek.Text = "";
        }
    }
}