using DataStructuresHW_Group16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataStructuresHW_Group16.Controllers
{
    public class StackController : Controller
    {
        static Stack<string> myStack = new Stack<string>();
        //temp stack
        private Stack<string> myTempStack = new Stack<string>();

        // GET: Stack
        public ActionResult Index()
        {
            //What appears automatically
            ViewBag.MyStack = "There are no items in Stack";

            return View();
        }

        public ActionResult AddOne()
        {
            //add one to stack
            myStack.Push("New Entry " + (myStack.Count + 1));

            int iStackCount = myStack.Count;

            //let user know it was done successfully
            ViewBag.MyStack = "An item was added to Stack. There are currently " + iStackCount + " item(s) in the stack.";

            return View("Index");
        }

        public ActionResult AddList()
        {
            //clear stack
            myStack.Clear();

            //load up stack
            for (int iCount = 1; iCount < 2001; iCount++)
            {
                myStack.Push("New Entry " + iCount);
            }

            //let user know it was done successfully
            ViewBag.MyStack = "A list of 2000 was added to the Stack";

            return View("Index");
        }

        public ActionResult Display()
        {
            ViewBag.MyStack = "<ul>";

            //loops for each item in stack. puts in bulleted list
            foreach (var item in myStack)
            {
                ViewBag.MyStack += "<li>" + item + "</li>";
            }

            ViewBag.MyStack += "</ul>";

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
            bool bPresent = false;//variable to see if input was found

            //checks if user has input
            if (sInput == "")
            {
                ViewBag.MyStack = "Please enter a Search";
            }
            else
            {
                //loops through each item in stack
                while (myStack.Count != 0)
                {
                    //check to see if first element is the input
                    if (sInput == myStack.Peek())
                    {
                        //if yes, delete it
                        myStack.Pop();
                        ViewBag.MyStack = sInput + " was deleted from the stack.";
                        bPresent = true;
                    }
                    else
                    {
                        //if not, transfer to temp structure
                        myTempStack.Push(myStack.Pop());
                    }
                }

                //reloads up normal structure
                while (myTempStack.Count != 0)
                {
                    myStack.Push(myTempStack.Pop());
                }

                //output if not found
                if (bPresent == false)
                {
                    ViewBag.MyStack = sInput + " was not found in the stack.";
                }

            }
            return View("Index");
        }

        public ActionResult Clear()
        {
            //clear stack
            myStack.Clear();

            //let user know it was done successfully
            ViewBag.MyStack = "There are no items in the Stack";

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
            if (myStack.Contains(sInput))
            {
                ViewBag.MyStack = sInput + " was found in ";
            }
            else
            {
                ViewBag.MyStack = sInput + " was not found. The search took ";
            }
            sw.Stop();

            TimeSpan ts = sw.Elapsed;

            ViewBag.MyStack += ts + " fractions of a second.";

            return View("Index");
        }

        public ActionResult Return()
        {
            //return back to main page
            return View("../Home/Index");
        }
    }
}