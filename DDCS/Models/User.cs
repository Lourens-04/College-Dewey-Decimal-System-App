using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Models
{
    class User
    {
        //instantiating user information variables 
        //-----------------------------------------
        private string userID;
        private string userEmail;
        private string userFirstName;
        private string userLastName;
        private string userLevel;
        private string userProgress;
        //-----------------------------------------

        //constructor to take in user infromation
        //----------------------------------------------------------------------------
        public User(string userID, string userEmail, string userFirstName, string userLastName, string userLevel, string userProgress)
        {
            this.userID = userID;
            this.userEmail = userEmail;
            this.userFirstName = userFirstName;
            this.userLastName = userLastName;
            this.userProgress = userProgress;
            this.userLevel = userLevel;
        }
        //----------------------------------------------------------------------------

        //Getters and Setters for user Information
        //----------------------------------------------------------------------------
        public string UserID { get => userID; set => userID = value; }
        public string UserEmail { get => userEmail; set => userEmail = value; }
        public string UserFirstName { get => userFirstName; set => userFirstName = value; }
        public string UserLastName { get => userLastName; set => userLastName = value; }
        public string UserLevel { get => userLevel; set => userLevel = value; }
        public string UserProgress { get => userProgress; set => userProgress = value; }
        //----------------------------------------------------------------------------
    }
}
