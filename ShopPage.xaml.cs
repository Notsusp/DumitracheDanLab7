using DumitracheDanLab7.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Plugin.LocalNotification;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DumitracheDanLab7
{
    public partial class ShopPage : ContentPage
    {
        public ShopPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var shop = (Shop)BindingContext;
            await App.Database.SaveShopAsync(shop);
            await Navigation.PopAsync();
        }

        async void OnShowMapButtonClicked(object sender, EventArgs e)
        {
            var shop = (Shop)BindingContext;
            if (shop == null || string.IsNullOrWhiteSpace(shop.Adress))
                return;

            var address = shop.Adress;
            var locations = await Geocoding.GetLocationsAsync(address);

            var options = new MapLaunchOptions
            {
                Name = "Magazinul meu preferat"
            };

            var location = locations?.FirstOrDefault();
            if (location == null)
                return;

            Location myLocation = null;
            try
            {
                myLocation = await Geolocation.GetLocationAsync();
            }
            catch
            {
                myLocation = new Location(46.7731796289, 23.6213886738);
            }

            var distance = myLocation.CalculateDistance(location, DistanceUnits.Kilometers);
            if (distance < 4)
            {
                var request = new NotificationRequest
                {
                    NotificationId = 100,
                    Title = "Ai de facut cumparaturi in apropiere!",
                    Description = address,
                    ReturningData = "shop",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(1)
                    }
                };
                LocalNotificationCenter.Current.Show(request);
            }

            await Map.OpenAsync(location, options);
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var shop = (Shop)BindingContext;
            if (shop == null)
                return;

            try
            {
                await App.Database.DeleteShopAsync(shop);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return;
            }

            await Navigation.PopAsync();
        }
    }
}
