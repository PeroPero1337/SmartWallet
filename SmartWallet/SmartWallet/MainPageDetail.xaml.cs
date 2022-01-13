using Microcharts;
using SkiaSharp;
using SmartWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{

    //Vse za MainPage se dela v tem filu pa njegovim xaml
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public ConnectionClass connection;
        public MainPageDetail()
        {
            InitializeComponent();
            connection = new ConnectionClass();
            connection.OpenConnection();
            string query = $"select uporabnisko_ime, ime, priimek, email from uporabnik where iduporabnik = {Uporabnik._id}";
            var data = connection.DataReader(query);
            data.Read();
            greetTxt.Text = $"{data["ime"].ToString()} {data["priimek"].ToString()}";
            greetTxt.FontSize = 18;
            connection.CloseConnection();

            DateTime date = DateTime.Today;
            var startDate = new DateTime(date.Year, date.Month, 1); string startDate1 = startDate.ToString("yyyy-MM-dd");
            var endDate = startDate.AddMonths(1).AddDays(-1); string endDate1 = endDate.ToString("yyyy-MM-dd");

            var myStack = stackMain;
            myStack.Children.Clear();

            List<string> podatki2 = new List<string>();
            string queryData2 = $"select * from podatek p join uporabnik u on u.iduporabnik = p.uporabniki_iduporabniki where iduporabnik = {Uporabnik._id} and datum between '{startDate1}' and '{endDate1}'";
            Search(podatki2, myStack, queryData2);

            List<ChartEntry> listek = new List<ChartEntry>();

            connection.OpenConnection();


            string queryData = $"select * from podatek p join uporabnik u on u.iduporabnik = p.uporabniki_iduporabniki where iduporabnik = {Uporabnik._id} and datum between '{startDate1}' and '{endDate1}'";
            var podatki = connection.DataReader(queryData);

            float totalStroski = 0; float totalPrihodki = 0;
            while (podatki.Read())
            {
                //znesek
                float znesek = float.Parse(podatki["cena"].ToString());
                int status = int.Parse(podatki["status_idstatus"].ToString());
                if (status == 1)
                {
                    totalStroski += znesek;
                }
                else if (status == 2) { totalPrihodki += znesek; }
            }
            connection.CloseConnection();

            connection.OpenConnection();

            string queryPrisparat = $"select opis, procent_varcevanja from strategije_varcevanja s join uporabnik u on s.idstrategije_varcevanja = u.strategije_varcevanja_idstrategije_varcevanja where iduporabnik = {Uporabnik._id}";
            var prisparano = connection.DataReader(queryPrisparat);
            prisparano.Read();
            int procnet = int.Parse(prisparano["procent_varcevanja"].ToString());

            //za prihranit
            float prihranitTreba = (totalPrihodki * procnet) / 100;

            connection.CloseConnection();


            //Stroški
            ChartEntry tmp = new ChartEntry(totalStroski)
            {
                Label = "Vsi stroški za tekoči mesec",
                ValueLabel = totalStroski.ToString(),
                Color = SKColor.Parse("#E85F6D")
            };
            //Vsi prihodki za tekoči mesec...
            ChartEntry tmp1 = new ChartEntry(totalPrihodki)
            {
                Label = "Vsi prihodki za tekoči mesec",
                ValueLabel = totalPrihodki.ToString(),
                Color = SKColor.Parse("#71E373")
            };
            ChartEntry tmp2 = new ChartEntry(prihranitTreba)
            {
                Label = "Potrebno privarčevati",
                ValueLabel = prihranitTreba.ToString(),
                Color = SKColor.Parse("#5FB4E8")
            };


            listek.Add(tmp); listek.Add(tmp1); listek.Add(tmp2);

            var chart = new DonutChart() { Entries = listek, LabelTextSize = 20f };
            this.chartView.Chart = chart;

        }

        private void Search(List<string> podatki, StackLayout stack, string query)
        {

            connection.OpenConnection();
            var data = connection.DataReader(query);
            while (data.Read())
            {
                var ime = data["ime_podatek"].ToString();
                var cena = data["cena"].ToString();
                var datum = DateTime.Parse(data["datum"].ToString()).ToString("dd/MM/yyyy");

                podatki.Add($"Naziv: {ime}; Vrednost: {cena}€; Datum: {datum}");
            }
            connection.CloseConnection();

            foreach (var item in podatki)
            {
                Frame frame = new Frame
                {
                    BorderColor = Color.Black,
                    CornerRadius = 5,
                    Content = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Black,
                        Text = item.Replace(";", "\n")
                    }
                };

                stack.Children.Add(frame);
            }
        }
    }
}