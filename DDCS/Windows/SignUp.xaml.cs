using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DDCS.Windows
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        Auth auth = new Auth();

        public SignUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //set visability for error lables and progress bar
            //------------------------------------------
            pbLoding.Visibility = Visibility.Hidden;
            lblError.Visibility = Visibility.Hidden;
            lblError2.Visibility = Visibility.Hidden;
            lblError3.Visibility = Visibility.Hidden;
            //------------------------------------------
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //set visability for error lables and progress bar
            //------------------------------------------
            lblError.Visibility = Visibility.Hidden;
            lblError2.Visibility = Visibility.Hidden;
            lblError3.Visibility = Visibility.Hidden;

            pbLoding.Visibility = Visibility.Visible;
            //------------------------------------------

            //create the user  
            CreateUser();
        }

        public async void CreateUser()
        {
            //Check if the user information fields are empty or not 
            if (!txbEmail.Text.Equals("") || !txbPassword.Password.Equals("") || !txbFirstName.Text.Equals("") || !   txbLastName.Text.Equals(""))
            {
                //check if the password length is grater or equel to 6
                if (txbPassword.Password.Length >= 6)
                {
                    //go to the auth class and create user in firebase
                    await auth.SignUp(txbFirstName.Text, txbLastName.Text, txbEmail.Text, txbPassword.Password);

                    //check if the user is valid
                    if (Auth.userIsValid)
                    {
                        //If the user is invalid take the user to the home screen
                        //--------------------------------------------
                        Home homeWin = new Home();
                        homeWin.Show();
                        this.Close();
                        //--------------------------------------------
                    }
                    else
                    {
                        //set visability for error lables 
                        //------------------------------------------
                        lblError2.Visibility = Visibility.Visible;
                        pbLoding.Visibility = Visibility.Hidden;
                        //------------------------------------------
                    }
                }
                else
                {
                    //set visability for error lables 
                    //------------------------------------------
                    lblError3.Visibility = Visibility.Visible;
                    pbLoding.Visibility = Visibility.Hidden;
                    //------------------------------------------
                }
            }
            else
            {
                //set visability for error lables 
                //------------------------------------------
                lblError.Visibility = Visibility.Visible;
                pbLoding.Visibility = Visibility.Hidden;
                //------------------------------------------
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //Takes the user to the login screen
            //--------------------------------------------
            LogIn logInWin = new LogIn();
            logInWin.Show();
            this.Close();
            //--------------------------------------------
        }
    }
}
