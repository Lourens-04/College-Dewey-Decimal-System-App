using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DDCS.Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace DDCS
{
    class Auth
    {
        //setting refrences to connect to the database
        private FirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyD8TfutILktY_82LFO3PKbDqeEuiuHszoU"));

        //setting refrences to connect to the database
        private FirebaseClient firebaseClient = new FirebaseClient("https://ddcs-e164a.firebaseio.com/");

        //currentUserName
        public static string currentUser = "";

        //Boolean that will say if the user is valid
        public static bool userIsValid = false;

        //instantiating the user info object 
        UserInfo userInfo = new UserInfo();

        public async Task LogIn(string email, string password)
        {
            try
            {
                //Check firebase if the user exists, and return the user if they exist
                var user = await auth.SignInWithEmailAndPasswordAsync(email, password);

                //set the current user
                currentUser = user.User.Email;

                //Get user Progress
                await userInfo.getCurrentUserProgress();

                userIsValid = true;
            }
            catch (Exception)
            {
                userIsValid = false;
            }
        }

        //Sign user up method
        public async Task SignUp(string firstName, string lastName, string email, string password)
        {
            try
            {
                //Check firebase if the user exists, and return the user if they exist
                var user = await auth.CreateUserWithEmailAndPasswordAsync(email, password);

                //set the current user
                currentUser = user.User.Email;

                //create user
                Models.User newUser = new Models.User("", "", "", "", "", "");

                //set an empty container in firebase for new user
                var allUsers = await firebaseClient
                    .Child("Users")
                    .PostAsync<Models.User>(newUser);

                //Get user firebase uploaded key
                string userID = allUsers.Key;

                //get the actual user information 
                newUser = new Models.User(userID, email, firstName, lastName, "1", "0");

                //set the user Information
                //----------------------------------------------------------
                var userInFirebase = firebaseClient.Child("Users");

                await userInFirebase.Child(userID).PutAsync<Models.User>(newUser);
                //----------------------------------------------------------

                //Get user Progress
                await userInfo.getCurrentUserProgress();

                //Check if the user is valid
                userIsValid = true;
            }
            catch (Exception)
            {
                //user is not valid
                userIsValid = false;
            }
        }
    }
}
