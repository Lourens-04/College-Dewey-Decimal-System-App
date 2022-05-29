using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Models
{
    class UserDataGridTable
    {
        //instantiating for the diffrent user answer details
        //-----------------------------------------
        private string questionNum;
        private string answer;
        //-----------------------------------------

        //constructor for the user answer details
        //----------------------------------------------------------------------------
        public UserDataGridTable(string questionNum, string answer)
        {
            this.questionNum = questionNum;
            this.answer = answer;
        }
        //----------------------------------------------------------------------------

        //Getters and Setters for the user answer details
        //----------------------------------------------------------------------------
        public string QuestionNum { get => questionNum; set => questionNum = value; }
        public string Answer { get => answer; set => answer = value;}
        //----------------------------------------------------------------------------
    }
}
