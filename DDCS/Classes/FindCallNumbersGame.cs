using DDCS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace DDCS.Classes
{
    class FindCallNumbersGame
    {
        //instantiating the tree data structure to hold all the data in the CN text file
        public static Tree<CallNumbers> tree = new Tree<CallNumbers>();

        //setting variable index to 0
        int index_Control_lvl1 = 0;

        //setting variable index to 0
        int index_Control_lvl2 = 0;

        public void LoadTree()
        {
            //Check if the tree data structure equels null
            if (tree.Root == null)
            {
                //Add the top node into the tree
                tree.Root = new TreeNode<CallNumbers>();
            }

            //Reding in the data from text file called CN
            //--------------------------------------------------------------------------------
            StreamReader stream = new StreamReader("CN.txt");

            while (stream.Peek() >= 0)
            {
                //String that will hold the current line in the text file
                string callNumDesc;

                //set the callNumDesc to the line returend from the text file
                callNumDesc = stream.ReadLine();

                //try the folling code
                try
                {
                    //check to see if the variable equels to null
                    if (callNumDesc != null)
                    {
                        //spliting the call number and discription 
                        string[] split = callNumDesc.Split('*');

                        //seting a variable with the call number
                        string callNum = split[0].Trim();

                        //seting a variable with the call number description
                        string desc = split[1].Trim();

                        //check if the last two digits in the call number ends with a 0
                        if (callNum.Substring(1,2).Equals("00"))
                        {
                            //setting index_Control_lvl2 variable yo 0
                            index_Control_lvl2 = 0;

                            //Add level one Child
                            AddLevelOneChildren(callNum, desc);

                            //incrementing index_Control_lvl1 variable
                            index_Control_lvl1++;

                            //Add level two Child
                            AddLevelTwoChildren(callNum, desc);

                            //incrementing index_Control_lvl2 variable
                            index_Control_lvl2++;
                        }
                        //check if the last digit in the call number ends with a 0
                        else if(callNum.Substring(2).Equals("0"))
                        {
                            //Add level two Child
                            AddLevelTwoChildren(callNum, desc);

                            //incrementing index_Control_lvl2 variable
                            index_Control_lvl2++;
                        }
                        else
                        {
                            //Add level three Child
                            AddLevelThreeChildren(callNum, desc);
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    //Do nothing
                }
            }
            //Close the stream from the text file
            stream.Close();
        }

        private void AddLevelThreeChildren(string callNum, string callNumDesc)
        {
            //Check if the Children for level 3 in the tree equels to null 
            if (tree.Root.Children[(index_Control_lvl1 - 1)].Children[(index_Control_lvl2 - 1)].Children == null)
            {
                //Setting the node into the children list in the tree data structure 
                tree.Root.Children[(index_Control_lvl1 - 1)].Children[(index_Control_lvl2 - 1)].Children = new List<TreeNode<CallNumbers>>()
                {
                    new TreeNode<CallNumbers>() { Data = new CallNumbers(callNum, callNumDesc), Parent = tree.Root.Children[(index_Control_lvl1- 1)].Children[(index_Control_lvl2 - 1)].Data }
                };
            }
            else
            {
                //Adding to to already created list in the children list in the tree data structure 
                tree.Root.Children[(index_Control_lvl1 - 1)].Children[(index_Control_lvl2 - 1)].Children.Add(new TreeNode<CallNumbers>() { Data = new CallNumbers(callNum, callNumDesc), Parent = tree.Root.Children[(index_Control_lvl1 - 1)].Children[(index_Control_lvl2 - 1)].Data });
            }
        }

        private void AddLevelTwoChildren(string callNum, string callNumDesc)
        {
            //Check if the Children for level 2 in the tree equels to null 
            if (tree.Root.Children[(index_Control_lvl1 - 1)].Children == null)
            {
                //Setting the node into the children list in the tree data structure 
                tree.Root.Children[(index_Control_lvl1- 1)].Children = new List<TreeNode<CallNumbers>>()
                {
                    new TreeNode<CallNumbers>() { Data = new CallNumbers(callNum, callNumDesc), Parent = tree.Root.Children[(index_Control_lvl1 - 1)].Data }
                };
            }
            else
            {
                //Adding to to already created list in the children list in the tree data structure 
                tree.Root.Children[(index_Control_lvl1 - 1)].Children.Add(new TreeNode<CallNumbers>() { Data = new CallNumbers(callNum, callNumDesc), Parent = tree.Root.Children[(index_Control_lvl1 - 1)].Data });
            }
        }

        private void AddLevelOneChildren(string callNum, string callNumDesc)
        {
            //Check if the Children for level 1 in the tree equels to null 
            if (tree.Root.Children == null)
            {
                //Setting the node into the children list in the tree data structure 
                tree.Root.Children = new List<TreeNode<CallNumbers>>
                {
                    new TreeNode<CallNumbers>(){ Data = new CallNumbers(callNum, callNumDesc), Parent = tree.Root.Data}
                };
            }
            else
            {
                //Adding to to already created list in the children list in the tree data structure 
                tree.Root.Children.Add(new TreeNode<CallNumbers>() { Data = new CallNumbers(callNum, callNumDesc), Parent = tree.Root.Data });
            }
        }
    }
}
