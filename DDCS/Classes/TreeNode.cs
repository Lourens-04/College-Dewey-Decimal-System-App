using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Classes
{
    class TreeNode<T>
    {
        //Setting the node variables to be added into the tree data structure 
        //----------------------------------------------------------------------
        public T Data { get; set; }
        public T Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        //----------------------------------------------------------------------
    }
}
