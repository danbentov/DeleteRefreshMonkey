namespace DeleeRefreshMonkey.Views;
using DeleeRefreshMonkey.ViewModels;

public partial class MonkeyView : ContentPage
{
	public MonkeyView(MonkeyViewModel mvm)
	{
		InitializeComponent();
		this.BindingContext = mvm;
	}
}