using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Models
{
    class CallNumbers
    {
        //instantiating the variables for books
        //-----------------------------------------
        private string callNum;
        private string callNumDesc;
        //-----------------------------------------

        //constructor to set books details
        //----------------------------------------------------------------------------
        public CallNumbers(string callNum, string callNumDesc)
        {
            this.callNum = callNum;
            this.callNumDesc = callNumDesc;
        }
        //----------------------------------------------------------------------------

        //Getters and Setters to get and set books details
        //----------------------------------------------------------------------------
        public string CallNum { get => callNum; set => callNum = value; }
        public string CallNumDesc { get => callNumDesc; set => callNumDesc = value; }
        //----------------------------------------------------------------------------
    }
}
