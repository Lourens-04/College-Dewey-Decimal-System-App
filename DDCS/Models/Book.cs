using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Models
{
    class Book
    {
        //instantiating the variables for books
        //-----------------------------------------
        private string bookID;
        private string bookTitle;
        //-----------------------------------------

        //constructor to set books details
        //----------------------------------------------------------------------------
        public Book(string bookID, string bookTitle)
        {
            this.bookID = bookID;
            this.bookTitle = bookTitle;
        }
        //----------------------------------------------------------------------------

        //Getters and Setters to get and set books details
        //----------------------------------------------------------------------------
        public string BookID { get => bookID; set => bookID = value; }
        public string BookTitle { get => bookTitle; set => bookTitle = value; }
        //----------------------------------------------------------------------------
    }
}
