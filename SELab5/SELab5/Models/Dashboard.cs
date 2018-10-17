using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELab5.Models
{
    public class Dashboard
    {
        public int countPerson { get; set; }
        public List<Person> birthdayPerson { get; set; }
        public List<Person> recentlyUpdatedPerson { get; set; }
        public Dashboard()
        {
            birthdayPerson = new List<Person>();
            recentlyUpdatedPerson = new List<Person>();
        }
    }
}