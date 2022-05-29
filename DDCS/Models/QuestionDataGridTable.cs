using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Models
{
    class QuestionDataGridTable
    {
        //instantiating for the diffrent questions details 
        //-----------------------------------------
        private string questionID;
        private string bookID;
        private string answerID;
        private string bookTitle;
        //-----------------------------------------

        //constructor for the questions details
        //----------------------------------------------------------------------------
        public QuestionDataGridTable(string questionID, string bookID, string answerID, string bookTitle)
        {
            this.questionID = questionID;
            this.bookID = bookID;
            this.answerID = answerID;
            this.bookTitle = bookTitle;
        }
        //----------------------------------------------------------------------------

        //Getters and Setters for the questions details
        //----------------------------------------------------------------------------
        public string QuestionID { get => questionID; set => questionID = value; }
        public string BookID { get => bookID; set => bookID = value; }
        public string AnswerID { get => answerID; set => answerID = value; }
        public string BookTitle { get => bookTitle; set => bookTitle = value; }
        //----------------------------------------------------------------------------
    }
}
