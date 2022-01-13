using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {
        //ListView listView;
        public ListView ListView { get { return listView; } }
        public MainPageFlyout()
        {
            InitializeComponent();

            var flyoutPageItems = new List<MainPageFlyoutMenuItem>();

            flyoutPageItems.Add(new MainPageFlyoutMenuItem
            {
                Id = 1,
                Title = "Nadzorna plošča",
                TargetType = typeof(MainPageDetail)//MainPageDetails ker MainPage nalozi sam ta meni kurcev
            });

            flyoutPageItems.Add(new MainPageFlyoutMenuItem
            {
                Id = 2,
                Title = "Profil",
                TargetType = typeof(ProfilePage)// -> ProfilePage
            });

            flyoutPageItems.Add(new MainPageFlyoutMenuItem
            {
                Id = 3,
                Title = "Podatki",
                TargetType = typeof(DataPage)// -> DataPage
            });

            flyoutPageItems.Add(new MainPageFlyoutMenuItem
            {
                Id = 4,
                Title = "Pregled podatkov",
                TargetType = typeof(PortfolioPage)// -> PortfolioPage
            });

            flyoutPageItems.Add(new MainPageFlyoutMenuItem
            {
                Id = 5,
                Title = "Varčevanje",
                TargetType = typeof(SavingsPage)// -> SavingsPage
            });
            flyoutPageItems.Add(new MainPageFlyoutMenuItem
            {
                Id = 6,
                Title = "Odjava",
                TargetType = typeof(LoginPage)// -> SavingsPage
            });


            listView.ItemsSource = flyoutPageItems;
            
        }

    }
}