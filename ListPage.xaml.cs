using DumitracheDanLab7.Models;

namespace DumitracheDanLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        if (shopl != null)
        {
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var selected = listView.SelectedItem as Product;
        if (selected == null)
            return;

        var shopl = (ShopList)BindingContext;
        if (shopl == null)
            return;

        // Delete the ListProduct entry linking this product and shoplist
        await App.Database.DeleteListProductByShopAndProductAsync(shopl.ID, selected.ID);

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
}