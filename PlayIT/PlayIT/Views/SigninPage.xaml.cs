using PlayIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayIT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SigninPage : ContentPage
	{
		public SigninPage ()
		{
			InitializeComponent ();
            Init();
		}

        void Init()
        {
            ActivitySpinner.IsVisible = false;
            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => LoginAsync(s, e);
        }

        async void LoginAsync(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);

            try
            {
                if (user.CheckInformation())
                {
                    DisplayAlert("Login", "Login Success", "OK");
                    var result = await App.RestService.Login(user);
                    if(result.access_token != null)
                    {
                        App.UserDatabase.SaveUser(user);
                    }
                }
                else
                {
                    DisplayAlert("Login", "Login failed. Invalid Username or Password", "Try Again");
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Warning", "Username or Password cannot be empty", "Try Again");
            }
            
        }
	}
}