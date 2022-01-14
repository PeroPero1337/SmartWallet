using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWallet.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioPage : ContentPage
    {
        ConnectionClass connection;
        public PortfolioPage()
        {
            InitializeComponent();
            radioStroski.IsChecked = true;
            try
            {
                connection = new ConnectionClass();
                connection.OpenConnection();
                string query = $"select uporabnisko_ime from uporabnik where iduporabnik = {Uporabnik._id}";
                var data = connection.DataReader(query);
                data.Read();
                txtGreetings.Text = $"{data["uporabnisko_ime"].ToString()}";
                connection.CloseConnection();
            }
            catch
            {
                DisplayAlert("Obvestilo", "Nepričakovana napaka! Poskusite ponovno kasneje!", "Ok");
            }
            
        }

        private void Search(List<string> podatki,StackLayout stack, string query)
        {
            try
            {
                connection.OpenConnection();
                var data = connection.DataReader(query);
                while (data.Read())
                {
                    var ime = data["ime_podatek"].ToString();
                    var cena = data["cena"].ToString();
                    var datum = DateTime.Parse(data["datum"].ToString()).ToString("dd/MM/yyyy");

                    podatki.Add($"Naziv: {ime};Znesek: {cena}€;Datum: {datum}");
                }
                connection.CloseConnection();

                foreach (var item in podatki)
                {
                    Frame frame = new Frame
                    {
                        BorderColor = Color.Black,
                        CornerRadius = 2,
                        Content = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = Color.Black,
                            Text = item.Replace(';', '\n')
                        }
                    };

                    stack.Children.Add(frame);
                }
            }
            catch
            {
                DisplayAlert("Obvestilo", "Nepričakovana napaka! Poskusite ponovno kasneje!", "Ok");
            }
        }

        private void btnIsci_Clicked(object sender, EventArgs e)
        {
            try
            {
                var dateFrom = datePickerPortfolioOd.Date.ToString("yyyy-MM-dd");
                var dateTo = datePickerPortfolioDo.Date.ToString("yyyy-MM-dd");
                var myStack = stackPortfolio;
                myStack.Children.Clear();

                if (radioStroski.IsChecked)
                {
                    List<string> podatki = new List<string>();
                    string query = $"select * from podatek p join uporabnik u on u.iduporabnik = p.uporabniki_iduporabniki where iduporabnik = {Uporabnik._id} and status_idstatus = 1 and datum between '{dateFrom}' and '{dateTo}'";
                    Search(podatki, myStack, query);
                }
                else if (radioPrihodki.IsChecked)
                {
                    List<string> podatki = new List<string>();
                    string query = $"select * from podatek p join uporabnik u on u.iduporabnik = p.uporabniki_iduporabniki where iduporabnik = {Uporabnik._id} and status_idstatus = 2 and datum between '{dateFrom}' and '{dateTo}'";
                    Search(podatki, myStack, query);
                }
                else
                {
                    DisplayAlert("Pozor!", "Izberite Stroške ali Prihodke!", "ok");
                }
                    
            }
            catch
            {
                DisplayAlert("Obvestilo", "Nepričakovana napaka! Poskusite ponovno kasneje!", "Ok");
            }
            
        }
    }
}