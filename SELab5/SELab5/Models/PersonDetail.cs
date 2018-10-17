using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELab5.Models
{
    public class PersonDetail
    {
        public Person person { get; set; }
        public List<Contact> listContact { get; set; }
        public PersonDetail()
        {
            listContact = new List<Contact>();
        }
    }
}