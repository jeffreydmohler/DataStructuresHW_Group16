using DataStructuresHW_Group16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataStructuresHW_Group16.Controllers
{
    public class QueueController : Controller
    {
        static Queue<string> myQueue = new Queue<string>();
        //temp queue
        private Queue<string> myTempQueue = new Queue<string>();

        // GET: Queue
        public ActionResult Index()
        {
            //What appears automatically
            ViewBag.MyQueue = "There are no items in Queue";

            return View();
        }

        public ActionResult AddOne()
        {
            //add one to queue
            myQueue.Enqueue("New Entry " + (myQueue.Count + 1));

            int iQueueCount = myQueue.Count;
            
            //let user know it was done successfully
            ViewBag.MyQueue = "An item was added to Queue. There are currently " + iQueueCount + " item(s) in the queue.";

            return View("Index");
        }
        
        public ActionResult AddList()
        {
            //clear queue
            myQueue.Clear();

            //load up queue
            for (int iCount = 1; iCount < 2001; iCount++)
            {
                myQueue.Enqueue("New Entry " + iCount);
            }

            //let user know it was done successfully
            ViewBag.MyQueue = "A list of 2000 was added to Queue";

            return View("Index");
        }

        public ActionResult Display()
        {
            ViewBag.MyQueue = "<ul>";

            //loops for each item in queue. puts in bulleted list
            foreach (var item in myQueue)
            {
                ViewBag.MyQueue += "<li>" + item + "</li>";
            }

            ViewBag.MyQueue += "</ul>";

            return View("Index");
        }

        //get method to instantiate userEntry object
        public ActionResult Delete()
        {
            return View(new UserEntry());
        }

        [HttpPost] //post method 
        public ActionResult Delete(UserEntry delete)
        {
            string sInput = delete.userInput;
            bool bPresent = false; //variable to see if input was found

            //checks if user has input
            if (sInput == "")
            {
                ViewBag.MyQueue = "Please enter a Search";
            }
            else
            {
                //loops through each item in queue
                while (myQueue.Count != 0)
                {
                    //check to see if first element is the input
                    if (sInput == myQueue.Peek())
                    {
                        //if yes, delete it
                        myQueue.Dequeue();
                        ViewBag.MyQueue = sInput + " was deleted from the queue.";
                        bPresent = true;
                    }
                    else
                    {
                        //if not, transfer to temp structure
                        myTempQueue.Enqueue(myQueue.Dequeue());
                    }
                }

                //reloads up normal structure
                while (myTempQueue.Count != 0)
                {
                    myQueue.Enqueue(myTempQueue.Dequeue());
                }

                //output if not found
                if (bPresent == false)
                {
                    ViewBag.MyQueue = sInput + " was not found in the queue.";
                }
            }

            return View("Index");
        }

        public ActionResult Clear()
        {
            //clear queue
            myQueue.Clear();

            //let user know it was done successfully
            ViewBag.MyQueue = "There are no items in Queue";

            return View("Index");
        }

        //get method - instantiate object
        public ActionResult Search()
        {
            return View(new UserEntry());
        }

        [HttpPost] //post method
        public ActionResult Search(UserEntry search)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            //start stopwatch 
            sw.Start();

            string sInput = search.sUserInput;

            //checks if input is in structure
            if (myQueue.Contains(sInput))
            {
                ViewBag.MyQueue = sInput + " was found in ";
            }
            else
            {
                ViewBag.MyQueue = sInput + " was not found. The search took ";
            }

            sw.Stop();

            TimeSpan ts = sw.Elapsed;

            ViewBag.MyQueue += ts + " fractions of a second.";

            return View("Index");
        }

        public ActionResult Return()
        {
            //return back to main page
            return View("../Home/Index");
        }
        
    }
}