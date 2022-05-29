using DDCS.Firebase;
using DDCS.Windows;
using Firebase.Auth;
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

namespace DDCS
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        //instantiating variable for total experience points
        private double totalExpNeeded = 500;

        //instantiating variable for user level
        private int level;

        //instantiating variable for user curent experience points
        private double exp;

        public Home()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //set the colour progress bar outer brush
            pgbEXP.OuterBackgroundBrush = Brushes.White;

            //set progress bar arc background width
            pgbEXP.ArcBackgroundWidth = 10;

            //set user current level
            level = Convert.ToInt32(UserInfo.user.UserLevel);

            //set user curent experience points
            exp = Convert.ToDouble(UserInfo.user.UserProgress);

            //set the lable to the user first name
            lblUsername.Content = "Account: " + UserInfo.user.UserFirstName;
            
            //set the level lable to the user current level
            lblUserLevel.Content = "Level: " + UserInfo.user.UserLevel;

            //display the percentage of how far away the user is to the next level
            progressDisplay();

            //set progress bar value 
            pgbEXP.Value = (exp / totalExpNeeded) * 100;
        }

        public void progressDisplay()
        {
            //if the user level is 2
            if (level == 2)
            {
                //calculate the persentage for progress bar
                totalExpNeeded = 1000;
                exp = exp - 500;
            }
            //if the user level is 3
            else if (level == 3)
            {
                //calculate the persentage for progress bar
                totalExpNeeded = 1500;
                exp = exp - 1000;
            }
            //if the user level is 4
            else if (level == 4)
            {
                //calculate the persentage for progress bar
                totalExpNeeded = 2000;
                exp = exp - 1500;
            }
            //if the user level is 5
            else if (level == 5)
            {
                //calculate the persentage for progress bar
                totalExpNeeded = 2500;
                exp = exp - 2000;
            }
        }

        private void btnSortBooksGame_Click(object sender, RoutedEventArgs e)
        {
            //Takes user to the sorting of call numbers game
            //--------------------------------------
            SortingGame replaceBookGameWin = new SortingGame();
            replaceBookGameWin.Show();
            this.Close();
            //--------------------------------------
        }

        private void btnIdentifyAreasGame_Click(object sender, RoutedEventArgs e)
        {
            IdentifyingGame identifyGameWin = new IdentifyingGame();
            identifyGameWin.Show();
            this.Close();
        }

        private void btnFindCallNumbers_Click(object sender, RoutedEventArgs e)
        {
            FindingCallNumbersGame findingCallNumbersGame = new FindingCallNumbersGame();
            findingCallNumbersGame.Show();
            this.Close();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            //Sign the user out of the apllication
            //--------------------------------------
            Auth.currentUser = "";
            UserInfo.user = null;

            LogIn logInWin = new LogIn();
            logInWin.Show();
            this.Close();
            //--------------------------------------
        }
    }
}
