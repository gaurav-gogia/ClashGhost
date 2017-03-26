using ClashGhost.Managers;
using ClashGhost.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ClashGhost.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        RegisterData data;
        ProfileResponse thisData;

        public Profile()
        {
            this.InitializeComponent();
            data = new RegisterData();
            thisData = new ProfileResponse();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            data = (RegisterData)e.Parameter;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            profiring.IsActive = true;
            profiring.Visibility = Visibility.Visible;
            try
            {
                thisData = await ProfileCall.GetUserAsync(data);
                nameblock.Text = DataSecurity.DecryptThisCipher(thisData.Name, Constants.PRIV);
                sexblock.Text = DataSecurity.DecryptThisCipher(thisData.Sex, Constants.PRIV);
                emailblock.Text = DataSecurity.DecryptThisCipher(thisData.Email, Constants.PRIV);
            }
            catch (Exception)
            {
                switch (thisData.Operation)
                {
                    case "Register":
                        nameblock.Text = "Gaurav Gogia";
                        sexblock.Text = "Male";
                        emailblock.Text = "desmondmiles36@gmail.com";
                        break;
                    case "Update":
                        nameblock.Text = "Nuong Xinh";
                        sexblock.Text = "Female";
                        emailblock.Text = "nuong@gmail.com";
                        break;
                }
            }
            finally
            {
                profiring.Visibility = Visibility.Collapsed;
                profiring.IsActive = false;
                nameblock.Visibility = Visibility.Visible;
                sexblock.Visibility = Visibility.Visible;
                emailblock.Visibility = Visibility.Visible;
            }
        }

        private void updateBut_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register), false);
        }
    }
}
