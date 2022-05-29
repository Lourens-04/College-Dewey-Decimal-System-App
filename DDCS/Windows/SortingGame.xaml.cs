using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF.JoshSmith.ServiceProviders.UI;
using DDCS.Firebase;
using System.Windows.Threading;

namespace DDCS
{
    /// <summary>
    /// Interaction logic for ReplacingBooksGame.xaml
    /// </summary>
    public partial class SortingGame : Window
    {
        //instantiating the DispatcherTimer variable 
        DispatcherTimer timer;

        //instantiating the TimeSpan variable
        TimeSpan time;

        //instantiating a list that will hold user answers
        List<string> UserAns = new List<string>();

        //instantiating SortGame class
        SortGame sortingBookGame = new SortGame();

        //instantiating UserInfo class
        UserInfo userInfo = new UserInfo();

        //instantiating GridView class
        GridView grdView = new GridView();

        //instantiating style class
        Style style = new Style();

        //instantiating Color class
        Color color = new Color();

        //instantiating SolidColorBrush class
        SolidColorBrush solidColorBrush = new SolidColorBrush();

        //instantiating user mark variable
        int mark = 0;

        //instantiating user experience points variable
        int exp = 0;

        public SortingGame()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //set finish button visability to hidden
            btnFinish.Visibility = Visibility.Hidden;

            //set finish button visability to hidden
            new ListViewDragDropManager<string>(this.lvRandomBooks);

            //set the time for 30 seconds
            time = TimeSpan.FromSeconds(30);

            //set a colour for a column background
            color = Color.FromRgb(62, 130, 132);

            //set the colour as a solid color brush
            solidColorBrush = new SolidColorBrush(color);

            //set the style target type
            style.TargetType = typeof(GridViewColumnHeader);

            //set the style for the Grid View ColumnHeader background
            style.Setters.Add(new Setter(GridViewColumnHeader.BackgroundProperty, solidColorBrush));

            //set a colour for a column Foreground
            color = Color.FromRgb(14, 39, 55);

            //set the colour as a solid color brush
            solidColorBrush = new SolidColorBrush(color);

            //set the style for the Grid View ColumnHeader Foreground
            style.Setters.Add(new Setter(GridViewColumnHeader.ForegroundProperty, solidColorBrush));

            //set the style for the Grid View ColumnHeader Max Width
            style.Setters.Add(new Setter(GridViewColumnHeader.MaxWidthProperty, 100d));

            //set the style for the Grid View ColumnHeader Height
            style.Setters.Add(new Setter(GridViewColumnHeader.HeightProperty, 228d));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //get and set 10 random call numbers
            sortingBookGame.RandomCallNumber();

            //show the finish button
            btnStart.Visibility = Visibility.Hidden;

            //hide the start button
            btnFinish.Visibility = Visibility.Visible;

            //foreach go through all the call numbers
            foreach (var item in SortGame.listOfCallNumbers)
            {
                //instantiating GridViewColumn class
                GridViewColumn nameColumn = new GridViewColumn();

                //set the column properties
                //----------------------------------------
                nameColumn.HeaderContainerStyle = style;
                nameColumn.Header = item;
                nameColumn.Width = 100;
                //----------------------------------------

                //set the grid View to the column created
                grdView.Columns.Add(nameColumn);
            }

            //sort the list of call numbers
            SortGame.listOfCallNumbers.Sort();

            //set the gridview to the list view
            lvRandomBooks.View = grdView;

            // start the timer
            StartTimer();
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            // stop the timer
            timer.Stop();

            //check if the column count is not equl to 0
            if (grdView.Columns.Count != 0)
            {
                //foreach to set user answer
                foreach (var item in grdView.Columns)
                {
                    //put the colun in the user answer list
                    UserAns.Add(item.Header.ToString());
                }

                //for loop to check the user makr and experience points
                for (int i = 0; i < SortGame.listOfCallNumbers.Count; i++)
                {
                    //check if the listOfCallNumbers item in position i is equel to the user answer list item in position i
                    if (UserAns[i].Equals(SortGame.listOfCallNumbers[i]))
                    {
                        //set the user mark
                        mark++;

                        //set user experience points 
                        exp = exp + 15;
                    }
                }

                //set user score
                userInfo.setUserScore(exp);

                //tell the user what they mark and experience points are
                MessageBox.Show("You got " + mark.ToString() + " right and ernded " + exp.ToString() + " EXP");

                //reset the the sorting game 
                //--------------------------------------------------------------
                color = Color.FromRgb(244, 230, 117);

                solidColorBrush = new SolidColorBrush(color);

                tbTimer.Foreground = solidColorBrush;

                time = TimeSpan.FromSeconds(31);
                btnStart.Visibility = Visibility.Visible;
                btnFinish.Visibility = Visibility.Hidden;
                tbTimer.Text = "Time: 30";
                grdView.Columns.Clear();
                lvRandomBooks.View = grdView;
                SortGame.listOfCallNumbers.Clear();
                UserAns.Clear();
                mark = 0;
                exp = 0;
                //--------------------------------------------------------------
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //check if the counter is less than 30
            if (time.Seconds < 30)
            {
                //ask the user are they are sure they want to leave
                if (MessageBox.Show("Are you sure you want to quit?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Take the user back to the home screen
                    //--------------------------------------
                    timer.Stop();
                    Home homeWin = new Home();
                    homeWin.Show();
                    this.Close();
                    //--------------------------------------
                }
            }
            else
            {
                //Take the user back to the home screen
                //--------------------------------------
                Home homeWin = new Home();
                homeWin.Show();
                this.Close();
                //--------------------------------------
            }
        }

        private void StartTimer()
        {
            //set the timer variable 
            //-----------------------------------------------------------------------------------------
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                //set the text for the time
                tbTimer.Text = "Time: " + time.Seconds;

                //check the time to display the colour for the time text
                if (time.Seconds == 15)
                {
                    //set text colour to orange 
                    tbTimer.Foreground = Brushes.Orange;
                }
                else if(time.Seconds == 5)
                {
                    //set text colour to red 
                    tbTimer.Foreground = Brushes.Red;
                }
                //check if the time equals 0
                else if(time == TimeSpan.Zero)
                {
                    //reset the the sorting game 
                    //--------------------------------------------------------------
                    timer.Stop();

                    MessageBox.Show("You ran out of time !!!, Try Again");

                    color = Color.FromRgb(244, 230, 117);

                    solidColorBrush = new SolidColorBrush(color);

                    tbTimer.Foreground = solidColorBrush;

                    time = TimeSpan.FromSeconds(31);
                    tbTimer.Text = "Time: 30";
                    btnStart.Visibility = Visibility.Visible;
                    btnFinish.Visibility = Visibility.Hidden;
                    grdView.Columns.Clear();
                    lvRandomBooks.View = grdView;
                    SortGame.listOfCallNumbers.Clear();
                    UserAns.Clear();
                    mark = 0;
                    exp = 0;
                    //--------------------------------------------------------------
                }

                time = time.Add(TimeSpan.FromSeconds(-1));

            }, Application.Current.Dispatcher);
            //-----------------------------------------------------------------------------------------

            //start the timer
            timer.Start();
        }
    }
}
