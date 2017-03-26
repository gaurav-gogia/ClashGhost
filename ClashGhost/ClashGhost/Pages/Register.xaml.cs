using ClashGhost.Managers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ClashGhost.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private RegisterData regData;
        private static string sexinfo;
        public static bool insert_update = true;
        public Register()
        {
            this.InitializeComponent();
            regData = new RegisterData();
            fillSexBox();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            insert_update = (bool)e.Parameter;
        }

        private void uidbox_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            uidbox.Text = "";
        }

        private void namebox_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            namebox.Text = "";
        }

        private void emailbox_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            emailbox.Text = "";
        }

        private void sexbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sexinfo = sexbox.SelectedValue as string;
        }        

        private async void regbox_Click(object sender, RoutedEventArgs e)
        {
            regbox.Visibility = Visibility.Collapsed;
            theRing.IsActive = true;
            theRing.Visibility = Visibility.Visible;

            regData.UID = uidbox.Text;
            regData.Name = DataSecurity.EncryptThisData(namebox.Text, Constants.PRIV);
            regData.Email = DataSecurity.EncryptThisData(emailbox.Text, Constants.PRIV);
            regData.Sex = DataSecurity.EncryptThisData(sexinfo, Constants.PRIV);
            #region ni dikhana jb
            const string priv = Constants.PRIV;
            const string publ = Constants.PUBL;
            #endregion
            regData.Password = HashKarenge.CreateHash(priv, publ, passbox.Password);

            try
            {
                var resultingResult = await CommonCall.RegisterUpdateAsync(regData, insert_update);
                Frame.Navigate(typeof(MainPage));
            }
            catch (Exception) { }
            finally
            {
                theRing.Visibility = Visibility.Collapsed;
                theRing.IsActive = false;
                regbox.Visibility = Visibility.Visible;
            }
        }

        private void fillSexBox()
        {
            sexbox.Items.Add("Female");
            sexbox.Items.Add("Male");
            sexbox.Items.Add("Transgender");
            sexbox.Items.Add("Refuse to Disclose");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (insert_update == true)
                statusBlock.Text = "Register Here";
            else
                statusBlock.Text = "Update Data";
        }
    }

    class RegisterData
    {
        public string Name { get; set; }
        public string UID { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string State { get; set; }
        public string Password { get; set; }
    }
}
