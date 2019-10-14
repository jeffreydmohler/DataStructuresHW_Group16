using DataStructuresHW_Group16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataStructuresHW_Group16.Controllers
{
    public class DictionaryController : Controller
    {
        static Dictionary<string, int> myDictionary = new Dictionary<string, int>();
        //temp dict
        private Dictionary<string, int> myTempDictionary = new Dictionary<string, int>();

        // GET: Dictionary
        public ActionResult Index()
        {
            //What appears automatically
            ViewBag.MyDictionary = "There are no items in Dictionary";

            return View();
        }

        public ActionResult AddOne()
        {
            myDictionary.Add("New Entry " + (myDictionary.Count + 1), (myDictionary.Count + 1));

            int iDictCount = myDictionary.Count;

            //let user know it was done successfully
            ViewBag.MyDictionary = "An item was added to Dictionary. There are currently " + iDictCount + " item(s) in the dictionary.";

            return View("Index");
        }
        
        public ActionResult AddList()
        {
            //clear Dictionary
            myDictionary.Clear();

            //load up Dictionary
            for (int iCount = 1; iCount < 2001; iCount++)
            {
                myDictionary.Add("New Entry " + (myDictionary.Count + 1), (myDictionary.Count + 1));
            }

            //let user know it was done successfully
            ViewBag.MyDictionary = "A list of 2000 was added to the Dictionary";

            return View("Index");
        }

        public ActionResult Display()
        {
            ViewBag.MyDictionary = "<ul>";

            //loops for each item in Dictionary. puts in bulleted list
            foreach (KeyValuePair<string, int> item in myDictionary)
            {
                ViewBag.MyDictionary += "<li>" + item + "</li>";
            }

            ViewBag.MyDictionary += "</ul>";

            return View("Index");
        }

        //get method to instantiate userEntry object
        public ActionResult Delete()
        {
            return View(new UserEntry());
        }

        [HttpPost]//post method 
        public ActionResult Delete(UserEntry delete)
        {

            string sInput = delete.userInput;

            //checks if user has input
            if (sInput == null)
            {
                ViewBag.MyDictionary = "Please enter a Search";
            }
            else
            {
                //checks if input in dict
                if (myDictionary.ContainsKey(sInput))
                {
                    //deletes input
                    myDictionary.Remove(sInput);
                    ViewBag.MyDictionary = sInput + " was deleted from the dictionary.";
                }
                else
                {
                    ViewBag.MyDictionary = sInput + " was not found in the dictionary.";
                }
            }

            return View("Index");
        }

        public ActionResult Clear()
        {
            //clear dict
            myDictionary.Clear();

            //let user know it was done successfully
            ViewBag.MyDictionary = "There are no items in the Dictionary";

            return View("Index");
        }

        //get method - instantiate object
        public ActionResult Search()
        {
            return View(new UserEntry());
        }

        [HttpPost]//post method
        public ActionResult Search(UserEntry search)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            
            //start stopwatch
            sw.Start();

            string sInput = search.sUserInput;

            //checks if input is in structure
            if (myDictionary.ContainsKey(sInput))
            {
                ViewBag.MyDictionary = sInput + " was found in ";
            }
            else
            {
                ViewBag.MyDictionary = sInput + " was not found. The search took ";
            }
            sw.Stop();

            TimeSpan ts = sw.Elapsed;

            ViewBag.MyDictionary += ts + " fractions of a second.";

            return View("Index");
        }

        public ActionResult Return()
        {
            //return back to main page
            return View("../Home/Index");
        }
        
    }
}