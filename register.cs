using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataGrid
{
    public class register
    {
        //private field
        private string _company;
        private string _description;
        private int _type;

        

        //public field
        public string Company { get { return _company; } }
        public string Description { get { return _description; } }
        public int Type { get { return _type; } }
      

        //constructor
        public register(string _company,string _description ,int type) 
        {
        
     
        }

        public void add()
        {

        }
    }
}
