using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SELab5.Models;
using Microsoft.AspNet.Identity;

namespace SELab5.Controllers
{
    public class PersonController : Controller
    {
        
        PhoneBookDbEntities _db;

        public PersonController()
        {
            _db = new PhoneBookDbEntities();
           

        }
        // GET: Person
        public ActionResult Index()
        {
            string login = User.Identity.GetUserId();

            List<Person> personList = new List<Person>();
            personList=_db.People.Where(person => person.AddedBy == login).ToList();
            Dashboard dashboard = new Dashboard();
            dashboard.countPerson = personList.Count;



           
            DateTime nxtDate = DateTime.Now.AddDays(10);
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            int nxtTenthday = DateTime.Now.AddDays(10).Day;
            foreach (Person selected in personList)
            {
                DateTime DOB = DateTime.Parse(selected.DateOfBirth.ToString());
                if (DOB.Month == currentMonth && DOB.Day >= currentDay && DOB.Day <= nxtTenthday)
                {
                    dashboard.birthdayPerson.Add(selected);

                }
            }
            dashboard.birthdayPerson = personList;
            return View(dashboard);
         //   dashboard.birthdayPerson=personList.Where(person=>Convert.ToDateTime(person.DateOfBirth.ToString()).Month==currentMonth && Convert.ToDateTime(person.DateOfBirth.ToString()).Day>=currentDay)

        }
      

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            Person person = new Person();
            List<Contact> lstContact = new List<Contact>();
            PersonDetail personDetail = new PersonDetail();
            person = _db.People.Find(id);
            lstContact = _db.Contacts.Where(contact => contact.PersonId == id).ToList();
            personDetail.person = person;
            personDetail.listContact = lstContact;
            return View(personDetail);
        }

       
        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(Person getPerson)
        {
            try
            {
                string login = User.Identity.GetUserId();

                // TODO: Add insert logic here
                Person StorePerson = new Person();
                StorePerson.FirstName = getPerson.FirstName;
                StorePerson.MiddleName = getPerson.MiddleName;
                StorePerson.LastName = getPerson.LastName;
                StorePerson.DateOfBirth = getPerson.DateOfBirth;
                StorePerson.FaceBookAccountId = getPerson.FaceBookAccountId;
                StorePerson.TwitterId = getPerson.TwitterId;
                StorePerson.LinkedInId = getPerson.LinkedInId;
                StorePerson.HomeAddress = getPerson.HomeAddress;
                StorePerson.AddedOn = DateTime.Now;
                StorePerson.UpdateOn = DateTime.Now;
                StorePerson.AddedBy = login;
                StorePerson.EmailId = getPerson.EmailId;
                StorePerson.HomeCity = getPerson.HomeCity;

                _db.People.Add(StorePerson);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            Person editPerson = _db.People.Find(id);

            return View(editPerson);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person getPerson)
        {
            try
            {
                // TODO: Add update logic here
                Person updatePerson = _db.People.Find(id);
                updatePerson.FirstName = getPerson.FirstName;
                updatePerson.MiddleName = getPerson.MiddleName;
                updatePerson.LastName = getPerson.LastName;
                updatePerson.DateOfBirth = getPerson.DateOfBirth;
                updatePerson.FaceBookAccountId = getPerson.FaceBookAccountId;
                updatePerson.TwitterId = getPerson.TwitterId;
                updatePerson.LinkedInId = getPerson.LinkedInId;
                updatePerson.HomeAddress = getPerson.HomeAddress;

                updatePerson.UpdateOn = DateTime.Now;

                updatePerson.EmailId = getPerson.EmailId;
                updatePerson.HomeCity = getPerson.HomeCity;

              
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            Person showTo = _db.People.Find(id);
            return View(showTo);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Person getperson)
        {
            try
            {
                // TODO: Add delete logic here
             Person deletePerson = _db.People.Find(id);
                List<Contact> deletePersonContacts = _db.Contacts.Where(contact => contact.PersonId == id).ToList();
                _db.Contacts.RemoveRange(deletePersonContacts);
                _db.SaveChanges();
                _db.People.Remove(deletePerson);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Contact(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Contact(int id, Contact personContact)
        {
          
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    Contact addContact = new Contact();
                    addContact.ContactNumber = personContact.ContactNumber;
                    addContact.Type = personContact.Type;
                    addContact.PersonId = id;
                    _db.Contacts.Add(addContact);
                    _db.SaveChanges();
                }
                    return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }
    }
}
