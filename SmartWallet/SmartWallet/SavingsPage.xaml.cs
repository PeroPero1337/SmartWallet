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
    public partial class SavingsPage : ContentPage
    {
        ConnectionClass connection;
        public SavingsPage()
        {
            InitializeComponent();
            connection = new ConnectionClass();

            connection.OpenConnection();

            string query = $"select opis from strategije_varcevanja";

            var strategije = new List<string>();
            var data = connection.DataReader(query);
            while (data.Read())
            {
                //podatki.Add(data["ime_podatek"].ToString());
                strategije.Add(data["opis"].ToString());
            }
            connection.CloseConnection();


            foreach (var item in strategije)
            {
                pickerStrategija.Items.Add(item);
            }

            var myStack = stackStrategije;

            pickerStrategija.SelectedIndex = 0;

            connection.OpenConnection();

            var list = new List<string>();
            //select opis, procent_varcevanja from strategije_varcevanja s join uporabnik u on s.idstrategije_varcevanja = u.strategije_varcevanja_idstrategije_varcevanja where iduporabnik = {Uporabnik._id}

            string query2 = $"select opis, procent_varcevanja from strategije_varcevanja";

            var data2 = connection.DataReader(query2);

            while (data2.Read())
            {
                var item ="Opis strategije: " + data2["opis"].ToString() + "; Procent varčevanja: " + data2["procent_varcevanja"].ToString() + "%";
                list.Add(item);
            }
            connection.CloseConnection();

            foreach (var item in list)
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

                myStack.Children.Add(frame);
            }

            connection.OpenConnection();
            string queryU = $"select ime,priimek,strategije_varcevanja.opis from uporabnik inner join strategije_varcevanja on uporabnik.strategije_varcevanja_idstrategije_varcevanja =strategije_varcevanja.idstrategije_varcevanja WHERE uporabnik.iduporabnik = {Uporabnik._id}";
            var dataU = connection.DataReader(queryU);
            dataU.Read();
            greetTxt.Text = $"Za uporabnika {dataU["ime"].ToString()} {dataU["priimek"].ToString()} je izbrana strategija varčevanja: {dataU["opis"].ToString()}";
            connection.CloseConnection();

        }

        private void btnIzberi_Clicked(object sender, EventArgs e)
        {

            connection.OpenConnection();
            string queryU = $"SELECT idstrategije_varcevanja FROM strategije_varcevanja WHERE opis = '{pickerStrategija.Items[pickerStrategija.SelectedIndex]}'";
            var data = connection.DataReader(queryU);
            data.Read();
            string id = data["idstrategije_varcevanja"].ToString();
            string update = $"UPDATE uporabnik  SET strategije_varcevanja_idstrategije_varcevanja = {id} WHERE iduporabnik = {Uporabnik._id}";

            connection.CloseConnection();
            connection.OpenConnection();
            if (connection.ExecuteQueries2(update))
                DisplayAlert("Strategija varčevanja", $"Izbrana strategija varčevanja: {pickerStrategija.Items[pickerStrategija.SelectedIndex]}", "Zapri");
            else
                DisplayAlert("Napaka", $"Prišlo je do napake", "Zapri");


            connection.CloseConnection();

            pickerStrategija.SelectedIndex = 0;




        }
    }
}