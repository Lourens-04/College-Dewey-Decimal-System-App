using DDCS.Classes;
using DDCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for IdentifyingGame.xaml
    /// </summary>
    public partial class IdentifyingGame : Window
    {
        //instantiating UserInfo class
        UserInfo userInfo = new UserInfo();

        //instantiating the Isentify Game class
        IdentifyGame identifyGame = new IdentifyGame();

        //instantiating static list of call randomly generated questions
        static List<QuestionDataGridTable> randomQuestionList = new List<QuestionDataGridTable>();

        //instantiating static list of a structured layout of the randomly generated questions
        static List<QuestionDataGridTable> structuredQuestionList = new List<QuestionDataGridTable>();

        //instantiating static list of to display the answer table values
        static List<UserDataGridTable> answerDataGridDisplay = new List<UserDataGridTable>();

        //instantiating static list that will hold the questions column values to be used for structering the questions
        static List<string> questionsHolder = new List<string>();

        //instantiating list that will hold randomly generated numbers
        List<int> randomDigitList = new List<int>();

        //string of alphabet letters
        string letters = "ABCDEFG";

        //Boolean to see if the user wants to play again
        bool playAgain = false;

        public IdentifyingGame()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //loading all the books and their call numbers in a dictonary 
            identifyGame.LoadDictionary();

            //setting the finish button visability to hidden
            btnFinish.Visibility = Visibility.Hidden;

            //Set the randomly generated questions
            setQuestions();
        }

        public void StartGame()
        {
            //setting the Start button visability to Hidden
            btnStart.Visibility = Visibility.Hidden;

            //setting the finish button visability to Visible
            btnFinish.Visibility = Visibility.Visible;

            //loding values to the combobox in the answer datagrid table
            LoadDataGridComboBoxRows();

            //A index to help structure the questions
            int index = 0;

            //A second index to help structure the questions
            int secondIndex = 0;

            //check if the user selected to play again to be able to change the questions up every time a user replay the game
            if (playAgain == false)
            {
                //while loop to add the structured question to the structuredQuestionList list
                while (structuredQuestionList.Count < randomQuestionList.Count)
                {
                    //check if the letter is equel to the randomly generated question letter (for example A == A)
                    if (letters[index].ToString().Equals(randomQuestionList[secondIndex].AnswerID))
                    {
                        //check if the index is less than 4
                        if (index < 4)
                        {
                            //Add the structerd question to the list
                            structuredQuestionList.Add(new QuestionDataGridTable((index + 1).ToString(), questionsHolder[index], randomQuestionList[secondIndex].AnswerID, randomQuestionList[secondIndex].BookTitle));
                        }
                        else
                        {
                            //Add the structerd question to the list
                            structuredQuestionList.Add(new QuestionDataGridTable("", questionsHolder[index], randomQuestionList[secondIndex].AnswerID, randomQuestionList[secondIndex].BookTitle));
                        }
                        
                        //set the second index back to 0
                        secondIndex = 0;

                        //increment the index to check for the next letter
                        index++;
                    }
                    else
                    {
                        //increment the second index to check for the next letter in the randoly generated questions list
                        secondIndex++;
                    }
                }

                //Help to control the changing of columns 
                index = 0;

                //Changing the order of the colums in the datagrid to ensure the user know the diffrence between ansers and questions
                foreach (var item in questionsDisplay.Columns)
                {
                    //check if the column index is 1 
                    if (index == 1)
                    {
                        //change the column poition to 1
                        item.DisplayIndex = 1;
                    }
                    //check if the column index is 3
                    else if (index == 3)
                    {
                        //change the column poition to 3
                        item.DisplayIndex = 3;
                    }

                    //increment the index to check for the next letter
                    index++;
                }

                //Add the structuredQuestionList to the datagrid itemsource
                questionsDisplay.ItemsSource = structuredQuestionList;
            }
            else
            {
                //while loop to add the structured question to the structuredQuestionList list
                while (structuredQuestionList.Count < randomQuestionList.Count)
                {
                    if (letters[index].ToString().Equals(randomQuestionList[secondIndex].AnswerID))
                    {
                        //check if the index is less than 4
                        if (index < 4)
                        {
                            //Add the structerd question to the list
                            structuredQuestionList.Add(new QuestionDataGridTable((index + 1).ToString(), randomQuestionList[secondIndex].BookID, randomQuestionList[secondIndex].AnswerID, questionsHolder[index]));
                        }
                        else
                        {
                            //Add the structerd question to the list
                            structuredQuestionList.Add(new QuestionDataGridTable("", randomQuestionList[secondIndex].BookID, randomQuestionList[secondIndex].AnswerID, questionsHolder[index]));
                        }

                        //set the second index back to 0
                        secondIndex = 0;

                        //increment the index to check for the next letter
                        index++;
                    }
                    else
                    {
                        //increment the second index to check for the next letter in the randoly generated questions list
                        secondIndex++;
                    }
                }

                //Help to control the changing of columns 
                index = 0;

                //Changing the order of the colums in the datagrid to ensure the user know the diffrence between ansers and questions
                foreach (var item in questionsDisplay.Columns)
                {
                    //check if the column index is 1
                    if (index == 1)
                    {
                        //change the column poition to 3
                        item.DisplayIndex = 3;
                    }
                    //check if the column index is 3
                    else if (index == 3)
                    {
                        //change the column poition to 1
                        item.DisplayIndex = 1;
                    }

                    //incrementing index
                    index++;
                }

                //Add the structuredQuestionList to the datagrid itemsource
                questionsDisplay.ItemsSource = structuredQuestionList;
            }
        }

        //Method Created to generate random values based on constaraints like the total values the lits have and the total loops the method must do
        public void RNG(int listTotalCount, int totalLoops)
        {
            //clears the randomDigitList list
            randomDigitList.Clear();

            //instantiating a new random class
            var random = new Random();

            //instantiating a check varable to check if a value already has been enterd into the list 
            bool check = false;

            //setting the starting point for the loop
            int count = 0;

            //loop to add all the random values into the randomDigitList list
            while (count != totalLoops)
            {
                //getting a random value
                int index = random.Next(listTotalCount);

                //check if the random value exists in the randomDigitList list
                foreach (var item in randomDigitList)
                {
                    //check if the value in the randomDigitList is equel to the one that was just created
                    if (item == index)
                    {
                        //if it does exists the set check to true
                        check = true;
                    }
                }

                //add the value to the randomDigitList list
                randomDigitList.Add(index);

                //check if the check boolean is true 
                if (check == true)
                {
                    //remove the value from the randomDigitList list
                    randomDigitList.Remove(index);
                    //Change the check boolean back to false
                    check = false;
                }
                else
                {
                    //increment the starting point 
                    count++;
                }
            }
        }

        public void setQuestions()
        {
            //Get 7 randomly generated numbers from the books dictonary list
            RNG(IdentifyGame.books.Count, 7);

            //instantiating a place holder that will hold the random 7 numbers 
            List<int> placeHolder = new List<int>();

            //adding the random numbers into the placeholder list
            foreach (var item in randomDigitList)
            {
                placeHolder.Add(item);
            }

            //Get 7 randomly generated numbers
            RNG(7, 7);

            //A index to help add questions randomly
            int index = 0;

            //variable to help control the numbering of the questions
            int questionNum = 1;

            //check if the user selected to play again
            if (playAgain == false)
            {
                //loop to add the questions to the randomQuestionList list
                while (randomQuestionList.Count != 7)
                {
                    //check if the randomQuestionList is lower than 4
                    if (randomQuestionList.Count < 4)
                    {
                        //Add the question to the randomQuestionList list
                        randomQuestionList.Add(new QuestionDataGridTable(questionNum.ToString(), IdentifyGame.books[placeHolder[index]].BookID, letters[randomDigitList[index]].ToString(), IdentifyGame.books[placeHolder[index]].BookTitle));

                        //Adding the questions column values
                        questionsHolder.Add(IdentifyGame.books[placeHolder[index]].BookID);

                        //increment the question number 
                        questionNum++;

                        //increment the index 
                        index++;
                    }
                    else
                    {
                        //Add the question to the randomQuestionList list
                        randomQuestionList.Add(new QuestionDataGridTable(" ", " ", letters[randomDigitList[index]].ToString(), IdentifyGame.books[placeHolder[index]].BookTitle));

                        //Adding the questions column values
                        questionsHolder.Add("");

                        //increment the question number 
                        questionNum++;

                        //increment the index 
                        index++;
                    }
                }
            }
            else
            {
                //loop to add the questions to the randomQuestionList list
                while (randomQuestionList.Count != 7)
                {
                    //check if the randomQuestionList is lower than 4
                    if (randomQuestionList.Count < 4)
                    {
                        //Add the question to the randomQuestionList list
                        randomQuestionList.Add(new QuestionDataGridTable(questionNum.ToString(), IdentifyGame.books[placeHolder[index]].BookID, letters[randomDigitList[index]].ToString(), IdentifyGame.books[placeHolder[index]].BookTitle));

                        //Adding the questions column values
                        questionsHolder.Add(IdentifyGame.books[placeHolder[index]].BookTitle);

                        //increment the question number 
                        questionNum++;

                        //increment the index 
                        index++;
                    }
                    else
                    {
                        //Add the question to the randomQuestionList list
                        randomQuestionList.Add(new QuestionDataGridTable("", IdentifyGame.books[placeHolder[index]].BookID, letters[randomDigitList[index]].ToString(), ""));

                        //Adding the questions column values
                        questionsHolder.Add("");

                        //increment the question number 
                        questionNum++;

                        //increment the index 
                        index++;
                    }
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //start the game
            StartGame();
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            //check the user anser and see if they want to play again
            checkUserAnswers();
        }

        public void checkUserAnswers()
        {
            //List that will be used to store all the user answers
            List<UserDataGridTable> userAnsersfromtheAnswerTableDataGrid = new List<UserDataGridTable>();

            //Setting a index variable to 0
            int index = 0;

            //setting a user mark variable to 0
            int mark = 0;

            //setting a user exp variable to 0
            int exp = 0;

            //setting a variable to display what the user got correct
            string correct = "";

            //getting all the users answers from the Answer Table DataGrid
            foreach (var item in userAnserTable.Items)
            {
                //Adding it to the userAnsersfromtheAnswerTableDataGrid list
                userAnsersfromtheAnswerTableDataGrid.Add((UserDataGridTable) item);
            }

            //loop to check what the user got correct and calculating thier mark and exp
            foreach (var item in userAnsersfromtheAnswerTableDataGrid)
            {
                //Check if the question letter assigned to the correct answer is the same as what the user selected
                if (randomQuestionList[index].AnswerID == item.Answer)
                {
                    //set the user mark
                    mark++;

                    //set all the correct answers
                    correct = correct + " " + randomQuestionList[index].AnswerID + ", ";

                    //set user experience points 
                    exp = exp + 10;
                }
                else
                {
                    //set all the correct answers
                    correct = correct + " " + randomQuestionList[index].AnswerID + ", ";
                }

                //increment index
                index++;
            }

            if (mark != 0)
            {
                //set user score
                userInfo.setUserScore(exp);
            }
            
            //tell the user what they mark and experience points are
            MessageBox.Show("Correct ansers were" + correct + "You got " + mark + " correct and ernded " + exp.ToString() + " EXP");

            //ask the user if they want to play the game again
            if (MessageBox.Show("Do you want to play again?",
            "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                //check if the play agin variable is equel to false
                if (playAgain == false)
                {
                    //change the boolean to true
                    playAgain = true;
                }
                else
                {
                    //change the boolean to false
                    playAgain = false;
                }

                //reset all the variables, as well UI components
                Reset();

                //set randomly generated questions
                setQuestions();

                //start the game again
                StartGame();
            }
            else
            {
                //reset all the variables, as well UI components
                Reset();

                //Making the start button visable
                btnStart.Visibility = Visibility.Visible;

                //Hiding the finish button
                btnFinish.Visibility = Visibility.Hidden;

                //set randomly generated questions if the user start the game again
                setQuestions();
            }
        }

        public void Reset()
        {
            //clear the randomQuestionList list
            randomQuestionList.Clear();

            //clear the structuredQuestionList list
            structuredQuestionList.Clear();

            //clear the questionsHolder list
            questionsHolder.Clear();

            //clear the answerDataGridDisplay list
            answerDataGridDisplay.Clear();

            //set the Anser Table datagrid items source to null
            userAnserTable.ItemsSource = null;

            //set the datagrid combo box items source to null
            Answer.ItemsSource = null;

            //set the datagrid items source to null
            questionsDisplay.ItemsSource = null;
        }

        public void LoadDataGridComboBoxRows()
        {
            //answerOptions list that will hold all the options a user can select
            List<string> answerOptions = new List<string>();

            //string of alphabet letters
            string letters = "ABCDEFG";

            //Adding the Select word for display purposes 
            answerOptions.Add("Select");

            //for loop to add all the options to the list 
            for (int i = 0; i < letters.Length; i++)
            {
                //Adding the letter to the options list 
                answerOptions.Add(letters[i].ToString());
            }

            //loop to add the questions numbers and the option to the datagrid 
            for (int i = 0; i < 4; i++)
            {
                //Adding the questions numbers and the option to the answerDataGridDisplay list
                answerDataGridDisplay.Add(new UserDataGridTable((i + 1).ToString(), "Select"));
            }

            //set the datagrid combo box items source to answerOptions list
            Answer.ItemsSource = answerOptions;

            //set the datagrid items source to the answerDataGridDisplay list
            userAnserTable.ItemsSource = answerDataGridDisplay;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //check if the counter is less than 30
            if (btnFinish.IsVisible)
            {
                //ask the user are they are sure they want to leave
                if (MessageBox.Show("Are you sure you want to quit?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Reset();
                    //Take the user back to the home screen
                    //--------------------------------------
                    Home homeWin = new Home();
                    homeWin.Show();
                    this.Close();
                    //--------------------------------------
                }
            }
            else
            {
                Reset();
                //Take the user back to the home screen
                //--------------------------------------
                Home homeWin = new Home();
                homeWin.Show();
                this.Close();
                //--------------------------------------
            }
        }

        private void questionsDisplay_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //Setting the background and foreground of the datagrid Rows
            //-----------------------------------------------------------------
            var row = e.Row;

            row.Background = new SolidColorBrush(Color.FromRgb(14, 39, 55));
            row.Foreground = new SolidColorBrush(Color.FromRgb(244, 230, 117));
            //-----------------------------------------------------------------
        }

        private void userAnserTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //Setting the background and foreground of the datagrid Rows
            //-----------------------------------------------------------------
            var row = e.Row;

            row.Background = new SolidColorBrush(Color.FromRgb(14, 39, 55));
            row.Foreground = new SolidColorBrush(Color.FromRgb(244, 230, 117));
            //-----------------------------------------------------------------
        }
    }
}
