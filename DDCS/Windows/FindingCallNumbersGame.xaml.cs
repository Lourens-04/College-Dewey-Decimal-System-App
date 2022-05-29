using DDCS.Classes;
using DDCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace DDCS.Windows
{
    /// <summary>
    /// Interaction logic for FindingCallNumbersGame.xaml
    /// </summary>
    public partial class FindingCallNumbersGame : Window
    {
        //instantiating FindCallNumbersGame class
        FindCallNumbersGame findCallNumbersGame = new FindCallNumbersGame();

        //instantiating UserInfo class
        UserInfo userInfo = new UserInfo();

        //List of all the indexes for the correct answer  
        List<int> answer = new List<int>();

        //List of indexes for the first question
        List<int> level_1_Options = new List<int>();

        //List of indexes for the second question
        List<int> level_2_Options = new List<int>();

        //List of indexes for the Third question
        List<int> level_3_Options = new List<int>();

        //List of random numbers that was retrived
        List<int> randomDigitList = new List<int>();

        //correct answer index for the first question
        int correctAnsLv1 = 0;

        //correct answer index for the second question
        int correctAnsLv2 = 0;

        //correct answer index for the Third question
        int correctAnsLv3 = 0;

        //String that contains the correct answer   
        string correctAns = "";

        //variable to help set the question to the radio buttons 
        int questionControler = 0;

        //variable to keep track of the user current question
        int userCurrentQuestion = 1;

        //varable to store the user selected anser from the radio buttons
        int userSelectedAns = -1;

        //setting a user mark variable to 0
        int mark = 0;

        //setting a user exp variable to 0
        int exp = 0;

        public FindingCallNumbersGame()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Load the data into a tree data structure
            if (FindCallNumbersGame.tree.Root == null)
            {
                findCallNumbersGame.LoadTree();
            }
            
            //Setting radio buttons to Hidden
            //------------------------------------------
            rbOption1.Visibility = Visibility.Hidden;
            rbOption2.Visibility = Visibility.Hidden;
            rbOption3.Visibility = Visibility.Hidden;
            rbOption4.Visibility = Visibility.Hidden;
            //------------------------------------------
        }

        public void GetQuestionsAndAnswer()
        {
            //get 4 random numbers from the range 0 to 9
            RNG(9, 4);
            level_1_Options.AddRange(randomDigitList);

            //get 4 random numbers from the range 0 to 5
            RNG(5, 4);
            level_2_Options.AddRange(randomDigitList);

            //get 4 random numbers from the range 0 to 5
            RNG(5, 4);
            level_3_Options.AddRange(randomDigitList);

            //get 1 random numbers from the range 0 to 4
            RNG(4, 1);
            //Adding a random index from the level_1_Options to the anser list to be the correct answer for Q1
            answer.Add(level_1_Options[randomDigitList[0]]);
            //setting the answer chosen to a global variable 
            correctAnsLv1 = level_1_Options[randomDigitList[0]];

            //get 1 random numbers from the range 0 to 4
            RNG(4, 1);
            //Adding a random index from the level_2_Options to the anser list to be the correct answer for Q2
            answer.Add(level_2_Options[randomDigitList[0]]);
            //setting the answer chosen to a global variable 
            correctAnsLv2 = level_2_Options[randomDigitList[0]];

            //get 1 random numbers from the range 0 to 4
            RNG(4, 1);
            //Adding a random index from the level_3_Options to the anser list to be the correct answer for Q3
            answer.Add(level_3_Options[randomDigitList[0]]);
            //setting the answer chosen to a global variable 
            correctAnsLv3 = level_3_Options[randomDigitList[0]];
        }

        public void SetQuestions()
        {
            //check to see if there na answer has been selected
            if (answer.Count == 0)
            {
                //Get questions and anser for the game
                GetQuestionsAndAnswer();

                //sorting the first question list
                level_1_Options.Sort();

                //sorting the second question list
                level_2_Options.Sort();

                //sorting the third question list
                level_3_Options.Sort();

                //Setting radio buttons to Visible
                //------------------------------------------
                rbOption1.Visibility = Visibility.Visible;
                rbOption2.Visibility = Visibility.Visible;
                rbOption3.Visibility = Visibility.Visible;
                rbOption4.Visibility = Visibility.Visible;
                //------------------------------------------

                //set the question view label content to display the call number discription 
                lblQuestionView.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[correctAnsLv3].Data.CallNumDesc;

                //set the submit button text to next
                btnSubmit.Content = "Next";
            }

            //check to see if the user is on question 3
            if (userCurrentQuestion == 3)
            {
                //set the submit button text to finish
                btnSubmit.Content = "Finish";
            }
            
            //switch case to set the question based on the current question the user is on
            switch (userCurrentQuestion)
            {
                case 1:
                    //getting the first index out of the first question list the user needs to anwser
                    questionControler = level_1_Options[0];

                    //seting the radio button content with the call number and discription
                    rbOption1.Content = FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNumDesc;

                    //getting the second index out of the first question list the user needs to anwser
                    questionControler = level_1_Options[1];

                    //seting the radio button content with the call number and discription
                    rbOption2.Content = FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNumDesc;

                    //getting the third index out of the first question list the user needs to anwser
                    questionControler = level_1_Options[2];

                    //seting the radio button content with the call number and discription
                    rbOption3.Content = FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNumDesc;

                    //getting the forth index out of the first question list the user needs to anwser
                    questionControler = level_1_Options[3];

                    //seting the radio button content with the call number and discription
                    rbOption4.Content = FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[questionControler].Data.CallNumDesc;

                    break;

                case 2:
                    //getting the first index out of the second question list the user needs to anwser
                    questionControler = level_2_Options[0];

                    //seting the radio button content with the call number and discription
                    rbOption1.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNumDesc;

                    //getting the second index out of the second question list the user needs to anwser
                    questionControler = level_2_Options[1];

                    //seting the radio button content with the call number and discription
                    rbOption2.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNumDesc;

                    //getting the third index out of the second question list the user needs to anwser
                    questionControler = level_2_Options[2];

                    //seting the radio button content with the call number and discription
                    rbOption3.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNumDesc;

                    //getting the forth index out of the second question list the user needs to anwser
                    questionControler = level_2_Options[3];

                    //seting the radio button content with the call number and discription
                    rbOption4.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[questionControler].Data.CallNumDesc;

                    break;

                case 3:
                    //getting the first index out of the third question list the user needs to anwser
                    questionControler = level_3_Options[0];

                    //seting the radio button content with the call number and discription
                    rbOption1.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNumDesc;

                    //getting the second index out of the third question list the user needs to anwser
                    questionControler = level_3_Options[1];

                    //seting the radio button content with the call number and discription
                    rbOption2.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNumDesc;

                    //getting the third index out of the third question list the user needs to anwser
                    questionControler = level_3_Options[2];

                    //seting the radio button content with the call number and discription
                    rbOption3.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNumDesc;

                    //getting the forth index out of the third question list the user needs to anwser
                    questionControler = level_3_Options[3];

                    //seting the radio button content with the call number and discription
                    rbOption4.Content = FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[questionControler].Data.CallNumDesc;

                    break;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //setting the start and submit button visability to hidden
            //----------------------------------------------
            btnStart.Visibility = Visibility.Hidden;
            btnSubmit.Visibility = Visibility.Visible;
            //----------------------------------------------

            //set the questions for the game when a user click the start button
            SetQuestions();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //get the user selected answer
            GetUserAnser();

            //checks to see if the user selected an answer
            if (userSelectedAns != -1)
            {
                //Switch to check what question a user is on
                switch (userCurrentQuestion)
                {
                    case 1:
                        //check user answer for question 1
                        CheckUserAnswerQ1();
                        break;

                    case 2:
                        //check user answer for question 2
                        CheckUserAnswerQ2();
                        break;

                    case 3:
                        //check user answer for question 3
                        CheckUserAnswerQ3();
                        break;
                }
            }
            else
            {
                //ask the user to select an asnwer
                MessageBox.Show("Please select an answer to continue");
            } 
        }

        public void GetUserAnser()
        {
            //check if the user selected option 1
            if (rbOption1.IsChecked == true)
            {
                //set the user selected varable to the radio buttion they selected
                userSelectedAns = 0;
            }
            //check if the user selected option 2
            else if (rbOption2.IsChecked == true)
            {
                //set the user selected varable to the radio buttion they selected
                userSelectedAns = 1;
            }
            //check if the user selected option 3
            else if (rbOption3.IsChecked == true)
            {
                //set the user selected varable to the radio buttion they selected
                userSelectedAns = 2;
            }
            //check if the user selected option 4
            else if (rbOption4.IsChecked == true)
            {
                //set the user selected varable to the radio buttion they selected
                userSelectedAns = 3;
            }
        }

        public void CheckUserAnswerQ1()
        {
            //check if what the user selected and the answer chosen by the system are the same for Q1
            if (level_1_Options[userSelectedAns] == answer[0])
            {
                //if it was the same that means the user was correct and will go to the correct method
                Correct();
            }
            else
            {
                //if it was not the same then that means that they got the question wrong and it will go to the wrong method
                Wrong();
            }
        }

        public void CheckUserAnswerQ2()
        {
            //check if what the user selected and the answer chosen by the system are the same for Q2
            if (level_2_Options[userSelectedAns] == answer[1])
            {
                //if it was the same that means the user was correct and will go to the correct method
                Correct();
            }
            else
            {
                //if it was not the same then that means that they got the question wrong and it will go to the wrong method
                Wrong();
            }
        }

        public void CheckUserAnswerQ3()
        {
            //check if what the user selected and the answer chosen by the system are the same for Q3
            if (level_3_Options[userSelectedAns] == answer[2])
            {
                //if it was the same that means the user was correct and will go to the correct method
                Correct();
            }
            else
            {
                //if it was not the same then that means that they got the question wrong and it will go to the wrong method
                Wrong();
            }
        }

        private void Wrong()
        {
            //set the correct answer variable to the correct answer to display to the user 
            //---------------------------------------------------------------------------------
            correctAns = "\n\nQ1) " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Data.CallNumDesc;

            correctAns = correctAns + "\n\nQ2) " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Data.CallNumDesc;

            correctAns = correctAns + "\n\nQ3) " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[correctAnsLv3].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[correctAnsLv3].Data.CallNumDesc;
            //---------------------------------------------------------------------------------

            //check how far the user got in the questions to set aproprate exp
            if (mark == 1)
            {
                //set user score
                userInfo.setUserScore(exp);
            }
            else if (mark == 2)
            {
                //set user score
                userInfo.setUserScore(exp);
            }

            //tell the user what their mark and experience points are as well as the correct answer
            MessageBox.Show("Sorry you selected the incorrect answer, you earned " + exp + " EXP. As follow is the correct answer for each question:" + "\n" + correctAns, "Incorrect", MessageBoxButton.OK , MessageBoxImage.Error);

            //Clear everyting in the game to reset it
            clear();

            //setting the start and submit button visability
            //----------------------------------------------
            btnStart.Visibility = Visibility.Hidden;
            btnSubmit.Visibility = Visibility.Visible;
            //----------------------------------------------

            //Because the user got it wrong the questions will be reset
            SetQuestions();
        }

        private void Correct()
        {
            //Check to see if the current question the user is on is not equel to 3
            if (userCurrentQuestion != 3)
            {
                //incrementing the current question
                userCurrentQuestion++;

                //setting the question label to display that it is on a new level
                lblQuestionTitle.Content = "Question " + userCurrentQuestion;

                //set the user mark
                mark++;

                //setting user exp
                exp = exp + 5;

                //Setting radio buttons is checked equels to false
                //------------------------------------------
                rbOption1.IsChecked = false;
                rbOption2.IsChecked = false;
                rbOption3.IsChecked = false;
                rbOption4.IsChecked = false;
                //------------------------------------------

                //set the next question options
                SetQuestions();
            }
            else
            {
                //set the correct answer variable to the correct answer to display to the user 
                //---------------------------------------------------------------------------------
                correctAns = "Q1) " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Data.CallNumDesc;

                correctAns = correctAns + "\n\nQ2) " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Data.CallNumDesc;

                correctAns = correctAns + "\n\nQ3) " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[correctAnsLv3].Data.CallNum + " " + FindCallNumbersGame.tree.Root.Children[correctAnsLv1].Children[correctAnsLv2].Children[correctAnsLv3].Data.CallNumDesc;
                //---------------------------------------------------------------------------------

                //set user score
                userInfo.setUserScore(15);

                //tell the user what their mark and experience points are as well as the correct answer
                MessageBox.Show("Well done you got every question correct, you earned 15 EXP. As follow is the correct answer for each question:" + "\n\n" + correctAns, "Correct", MessageBoxButton.OK, MessageBoxImage.Information);

                //Clear everyting in the game to reset it
                clear();

                //Setting radio buttons back to Hidden
                //------------------------------------------
                rbOption1.Visibility = Visibility.Hidden;
                rbOption2.Visibility = Visibility.Hidden;
                rbOption3.Visibility = Visibility.Hidden;
                rbOption4.Visibility = Visibility.Hidden;
                //------------------------------------------

                //setting the start and submit button visability
                //----------------------------------------------
                btnStart.Visibility = Visibility.Visible;
                btnSubmit.Visibility = Visibility.Hidden;
                //----------------------------------------------

                //reseting Question view content
                lblQuestionView.Content = "Click Start to Begin";
            }
        }

        private void clear()
        {
            //clear the answer list
            answer.Clear();

            //clear the options for first questions list
            level_1_Options.Clear();

            //clear the options for second questions list
            level_2_Options.Clear();

            //clear the options for third questions list
            level_3_Options.Clear();

            //Clear and random digits that are still in randomDigitList list
            randomDigitList.Clear();

            //set the correct variable for Q1 back to 0
            correctAnsLv1 = 0;

            //set the correct variable for Q2 back to 0
            correctAnsLv2 = 0;

            //set the correct variable for Q3 back to 0
            correctAnsLv3 = 0;

            //reseting the questionControler variable back to 0
            questionControler = 0;

            //reseting the userCurrentQuestion variable back to 0
            userCurrentQuestion = 1;

            //reseting the userCurrentQuestion variable back to -1
            userSelectedAns = -1;

            //reseting the mark variable back to 0
            mark = 0;

            //reseting the exp variable back to 0
            exp = 0;

            //resetting the question label content back to Question 1
            lblQuestionTitle.Content = "Question " + userCurrentQuestion;

            //Setting radio buttons is checked equels to false
            //------------------------------------------
            rbOption1.IsChecked = false;
            rbOption2.IsChecked = false;
            rbOption3.IsChecked = false;
            rbOption4.IsChecked = false;
            //------------------------------------------
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //check if the counter is less than 30
            if (btnSubmit.IsVisible)
            {
                //ask the user are they are sure they want to leave
                if (MessageBox.Show("Are you sure you want to quit?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Clear everyting in the game to reset it
                    clear();

                    //Setting radio buttons back to Hidden
                    //------------------------------------------
                    rbOption1.Visibility = Visibility.Hidden;
                    rbOption2.Visibility = Visibility.Hidden;
                    rbOption3.Visibility = Visibility.Hidden;
                    rbOption4.Visibility = Visibility.Hidden;
                    //------------------------------------------

                    //setting the start and submit button visability
                    //----------------------------------------------
                    btnStart.Visibility = Visibility.Visible;
                    btnSubmit.Visibility = Visibility.Hidden;
                    //----------------------------------------------

                    //reseting Question view content
                    lblQuestionView.Content = "Click Start to Begin";

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
                //Clear everyting in the game to reset it
                clear();

                //Setting radio buttons back to Hidden
                //------------------------------------------
                rbOption1.Visibility = Visibility.Hidden;
                rbOption2.Visibility = Visibility.Hidden;
                rbOption3.Visibility = Visibility.Hidden;
                rbOption4.Visibility = Visibility.Hidden;
                //------------------------------------------

                //setting the start and submit button visability
                //----------------------------------------------
                btnStart.Visibility = Visibility.Visible;
                btnSubmit.Visibility = Visibility.Hidden;
                //----------------------------------------------

                //reseting Question view content
                lblQuestionView.Content = "Click Start to Begin";

                //Take the user back to the home screen
                //--------------------------------------
                Home homeWin = new Home();
                homeWin.Show();
                this.Close();
                //--------------------------------------
            }
        }
    }
}
