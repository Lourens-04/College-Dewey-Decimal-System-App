using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace DDCS.Firebase
{
    class SortGame
    {
        //instantiating static list of call numbers 
        public static List<string> listOfCallNumbers = new List<string>();

        //string of alphabet letters
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        //instantiating random object
        Random random = new Random();

        public void RandomCallNumber()
        {
            //loop 10 times to get 10 random call numbers
            for (int j = 0; j < 10; j++)
            {
                //set call number string
                string callNum = "";

                //get the 6 digets - first three for catagory and last three for sub catagory
                for (int i = 0; i < 7; i++)
                {
                    //check if the postion is 3 to add the "."
                    if (i != 3)
                    {
                        //set number to string
                        callNum = callNum + random.Next(0, 10);
                    }
                    else
                    {
                        //set the "."
                        callNum = callNum + ".";
                    }
                }

                //set a space in the string to split numbers and letters
                callNum = callNum + " ";

                //set call number letters for the auth name
                for (int i = 0; i < 3; i++)
                {
                    //set the letter to the string
                    callNum = callNum + letters[random.Next(letters.Length)];
                }

                //save the call number to the list of all call numbers
                listOfCallNumbers.Add(callNum);
            }
        }
    }
}
