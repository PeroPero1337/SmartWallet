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
            connection = new ConnectionClass();
            connection.OpenConnection();
            string query = $"select uporabnisko_ime from uporabnik where iduporabnik = {Uporabnik._id}";
            var data = connection.DataReader(query);
            data.Read();
            txtGreetings.Text = $"Pozrdavljen {data["uporabnisko_ime"].ToString()}";
            connection.CloseConnection();
        }

        private void Search(List<string> podatki,StackLayout stack, string query)
        {
            
            connection.OpenConnection();
            var data = connection.DataReader(query);
            while (data.Read())
            {
                var ime = data["ime_podatek"].ToString();
                var cena = data["cena"].ToString();
                var datum = data["datum"].ToString();

                podatki.Add($"{ime};{cena};{datum}");
            }
            connection.CloseConnection();

            foreach (var item in podatki)
            {
                Frame frame = new Frame
                {
                    BorderColor = Color.Black,
                    CornerRadius = 20,
                    Content = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Black,
                        Text = item
                    }
                };

                stack.Children.Add(frame);
            }
        }

        private void btnIsci_Clicked(object sender, EventArgs e)
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
                DisplayAlert("Pozor!", "Izberite Stroške ali Prihodke!", "ok");
        }
    }
}