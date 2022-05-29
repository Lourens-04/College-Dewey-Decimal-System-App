using DDCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDCS.Classes
{
    class IdentifyGame
    {
        //instantiating static Dictionary list of call numbers and book descriptions
        public static IDictionary<int, Book> books = new Dictionary<int, Book>();

        //Load all the books call numbers with their discriptions to be used in the Identify game
        public void LoadDictionary()
        {
            books.Clear();

            //Adding all the books to the dictonary
            //------------------------------------------------------
            books.Add(0, new Book("000", "Generalities"));
            books.Add(1, new Book("100", "Philosophy & psychology"));
            books.Add(2, new Book("200", "Religion"));
            books.Add(3, new Book("300", "Social sciences"));
            books.Add(4, new Book("400", "Language"));
            books.Add(5, new Book("500", "Natural sciences & mathematics"));
            books.Add(6, new Book("600", "Technology (Applied sciences)"));
            books.Add(7, new Book("700", "The arts"));
            books.Add(8, new Book("800", "Literature & rhetoric"));
            books.Add(9, new Book("900", "Geography & history"));
            //------------------------------------------------------
        }
    }
}
