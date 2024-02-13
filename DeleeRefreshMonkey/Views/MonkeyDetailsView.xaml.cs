using DeleeRefreshMonkey.ViewModels;

namespace DeleeRefreshMonkey.Views;

public partial class MonkeyDetailsView : ContentPage
{
	public MonkeyDetailsView(MonkeyDetailsViewModel mdvm)
	{
		InitializeComponent();
		this.BindingContext = mdvm;
	}
}