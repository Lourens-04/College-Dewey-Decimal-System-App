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
using DDCS.Windows;
using Firebase.Auth;

namespace DDCS
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        //instantiating the Auth object
        Auth auth = new Auth();

        public LogIn()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            //set visability for error lable and progress bar
            //------------------------------------------
            lblError.Visibility = Visibility.Hidden;
            pbLoding.Visibility = Visibility.Visible;
            //------------------------------------------

            //Vlidate the user
            ValidateUser();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //set visability for error lable and progress bar
            //------------------------------------------
            lblError.Visibility = Visibility.Hidden;
            pbLoding.Visibility = Visibility.Hidden;
            //------------------------------------------
        }

        public async void ValidateUser()
        {
            //check firebase if the user is signed in
            await auth.LogIn(txbEmail.Text, txbPassword.Password);

            //if the boolean is true or false
            //-------------------------------------
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
                //set visability for error lable and progress bar
                //------------------------------------------
                lblError.Visibility = Visibility.Visible;
                pbLoding.Visibility = Visibility.Hidden;
                //------------------------------------------
            }
            //-------------------------------------
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //Takes the user to the signup creen
            //--------------------------------------------
            SignUp signUpWin = new SignUp();
            signUpWin.Show();
            this.Close();
            //--------------------------------------------
        }
    }
}
