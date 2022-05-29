using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace DDCS
{
    class UserInfo
    {
        //instantiating refrences to connect to the database
        private FirebaseClient firebaseClient = new FirebaseClient("https://ddcs-e164a.firebaseio.com/");

        //instantiating user static object  
        public static Models.User user;

        public async Task getCurrentUserProgress()
        {
            //Retrieve data from Firebase
            var allUsers = await firebaseClient
              .Child("Users")
              .OnceAsync<Models.User>();

            //Check if the user exists 
            foreach (var usersInfo in allUsers)
            {
                //check if the user email is equel to the current user email
                if (usersInfo.Object.UserEmail.Equals(Auth.currentUser))
                {
                    //set the user information to the user static object
                    user = usersInfo.Object;
                }
            }
        }

        //method to set the user score
        public async void setUserScore(int Newexp)
        {
            //get the user experience points
            int currentExp = Convert.ToInt32(UserInfo.user.UserProgress);

            //set the user new experience points
            int newTotalExp = currentExp + Newexp;

            //set the user new experience points in the user object
            UserInfo.user.UserProgress = newTotalExp + "";

            //check if the experience is greater than 500 and less than 1000 
            if (newTotalExp > 500 && newTotalExp < 1000)
            {
                UserInfo.user.UserLevel = "2";
            }
            //check if the experience is greater than 1000 and less than 1500 
            else if (newTotalExp > 1000 && newTotalExp < 1500)
            {
                UserInfo.user.UserLevel = "3";
            }
            //check if the experience is greater than 1500 and less than 2000 
            else if (newTotalExp > 1500 && newTotalExp < 2000)
            {
                UserInfo.user.UserLevel = "4";
            }
            //check if the experience is greater than 2000 and less than 2500 
            else if (newTotalExp > 2000 && newTotalExp < 2500)
            {
                UserInfo.user.UserLevel = "5";
            }

            //set the user updated information in firebase
            //---------------------------------------------------------
            var allUsers = firebaseClient.Child("Users");

            await allUsers.Child(UserInfo.user.UserID).PutAsync<Models.User>(UserInfo.user);
            //---------------------------------------------------------
        }
    }
}
