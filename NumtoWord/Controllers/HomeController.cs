using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NumtoWord.Models;
using NumtoWord.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NumtoWord.Controllers
{
    public class HomeController : Controller
    {
        INumToWordService _numtowordservice;

        public HomeController(INumToWordService numtowordservice)
        {
           _numtowordservice=numtowordservice;
    }

        public ActionResult Index()
        {
            return View();
        }
       
        private string NumtoWd(string number)
        {
            if (number.Length == 0) {
                number = "0.00";
            }

            var NumtoWord = "";
            var numberdecimal = String.Format("{0:0.00}", decimal.Parse(number));
            var numberchars = numberdecimal.ToCharArray();

            int realint = numberchars.Length - 3;

            for (int j = 0; j < numberchars.Length; j++)
            {
                var one = numberchars.Length - 1;
                var two = numberchars.Length - 2;

                if (j > realint)
                { //for digit only
                    if (j == one)
                    {
                        NumtoWord += ""; //nothing shown
                    }
                    else
                    {
                        var twonum = int.Parse(numberchars[two].ToString());
                        var onenum = int.Parse(numberchars[one].ToString());
                        var realnum = 0;
                        if (twonum > 0 && onenum == 0)
                        {
                            realnum = twonum;
                            NumtoWord += getStrfromDic(realnum);
                        }
                        else
                        {
                            var to = numberchars[two].ToString() + numberchars[one].ToString();
                            if (int.Parse(to) < 20)
                            {
                                NumtoWord += getStrfromDic(int.Parse(to));
                            }
                            else
                            {
                                NumtoWord += getStrfromDic(int.Parse(numberchars[two].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[one].ToString()));
                            }
                        }
                    }

                }
                else
                {
                    //start big monster here
                    //based on the practiceal checque amount, pick up hundred billion as limit, it is enough.
                    //1, find out number position
                    var hundredbillion = numberchars.Length - 15;
                    var tenbillion = numberchars.Length - 14;
                    var onebillion = numberchars.Length - 13;

                    var hundredmillion = numberchars.Length - 12;
                    var tenmillion = numberchars.Length - 11;
                    var onemillion = numberchars.Length - 10;

                    var hundredthousand = numberchars.Length - 9;
                    var tenthousand = numberchars.Length - 8;
                    var thousand = numberchars.Length - 7;

                    var hundred = numberchars.Length - 6;
                    var tenth = numberchars.Length - 5;
                    var oneth = numberchars.Length - 4;

                    if (j == hundredbillion)
                    {
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredbillion].ToString())) + " hundred";
                        //hundred position (1st)

                        var cc = numberchars[tenbillion].ToString() + numberchars[onebillion].ToString();
                        if (int.Parse(cc) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(cc)) + " billion";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenbillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onebillion].ToString())) + " billion";
                        }
                        //ten and one position for billion ( 2nd 3rd)



                        //hundred million position (1st)
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredmillion].ToString())) + " hundred";

                        var c0 = numberchars[tenmillion].ToString() + numberchars[onemillion].ToString();
                        if (int.Parse(c0) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c0)) + " million";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenmillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onemillion].ToString())) + " million";
                        }
                        //hundred million position (2nd 3rd)



                        //hundred thousand position (1st)
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }
                        //hundred thousand position (2nd, 3rd) 



                        //hundred position (1st)
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString() + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //ten and one dollar position (2nd, 3rd)


                    }

                    else if (hundredbillion == -1 && j == tenbillion)
                    {

                        //ten and one (2nd, 3rd)
                        var cc = numberchars[tenbillion].ToString() + numberchars[onebillion].ToString();

                        if (int.Parse(cc) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(cc)) + " billion";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenbillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onebillion].ToString())) + " billion";
                        }

                        //hundred million (1st)
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredmillion].ToString())) + " hundred";

                        var c0 = numberchars[tenmillion].ToString() + numberchars[onemillion].ToString();
                        if (int.Parse(c0) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c0)) + " million";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenmillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onemillion].ToString())) + " million";
                        }
                        //hundred million 2nd 3rd

                        //hundred thousand 1st
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }
                        //hundred thousand 2nd 3rd

                        //hundred
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString() + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar

                    }

                    else if (tenbillion == -1 && j == onebillion)
                    {
                        NumtoWord += getStrfromDic(int.Parse(numberchars[onebillion].ToString())) + " billion";
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredmillion].ToString())) + " hundred";
                        var c0 = numberchars[tenmillion].ToString() + numberchars[onemillion].ToString();
                        if (int.Parse(c0) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c0)) + " million";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenmillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onemillion].ToString())) + " million";
                        }

                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }


                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar



                    }


                    //worked
                    else if (onebillion == -1 && j == hundredmillion)
                    {
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredmillion].ToString())) + " hundred";
                        var c0 = numberchars[tenmillion].ToString() + numberchars[onemillion].ToString();
                        if (int.Parse(c0) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c0)) + " million";
                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenmillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onemillion].ToString())) + " million";
                        }

                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }


                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar



                    }

                    else if (hundredmillion == -1 && j == tenmillion)
                    {

                        var c0 = numberchars[tenmillion].ToString() + numberchars[onemillion].ToString();

                        if (int.Parse(c0) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c0)) + " million";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenmillion].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[onemillion].ToString())) + " million";
                        }

                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }


                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar




                    }

                    else if (tenmillion == -1 && j == onemillion)
                    {
                        NumtoWord += getStrfromDic(int.Parse(numberchars[onemillion].ToString())) + " million";

                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }


                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar



                    }


                    else if (onemillion == -1 && j == hundredthousand)
                    {

                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundredthousand].ToString())) + " hundred";

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10) + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }


                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar



                    }

                    else if (hundredthousand == -1 && j == tenthousand)
                    {

                        var c = numberchars[tenthousand].ToString() + numberchars[thousand].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " thousand";

                        }
                        else
                        {
                            NumtoWord += getStrfromDic(int.Parse(numberchars[tenthousand].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        }

                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c1 = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c1) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c1)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar




                    }



                    else if (tenthousand == -1 && j == thousand)
                    {

                        NumtoWord += getStrfromDic(int.Parse(numberchars[thousand].ToString())) + " thousand";
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar


                    }

                    else if (thousand == -1 && j == hundred)
                    {
                        NumtoWord += getStrfromDic(int.Parse(numberchars[hundred].ToString())) + " hundred";

                        var c = numberchars[tenth].ToString() + numberchars[oneth].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        } //dollar

                    }

                    else if (hundred == -1 && j == tenth)
                    {

                        var c = numberchars[tenth].ToString()
                            + numberchars[oneth].ToString();

                        if (int.Parse(c) < 20)
                        {
                            NumtoWord += getStrfromDic(int.Parse(c)) + " dollar";
                        }
                        else
                        {
                            NumtoWord +=
                                getStrfromDic(int.Parse(numberchars[tenth].ToString()) * 10)
                                + getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                        }
                    }

                    else if (tenth == -1 && j == oneth)
                    {
                        NumtoWord +=
                            getStrfromDic(int.Parse(numberchars[oneth].ToString())) + " dollar";
                    }

                }

            }

            NumtoWord += " cent"; //need to refactor later
            return NumtoWord;

        }

        public static string getStrfromDic(int key)
        {

            var dic = new Dictionary<int, string>();

            dic.Add(0, " zero");
            dic.Add(1, " one");
            dic.Add(2, " two");
            dic.Add(3, " three");
            dic.Add(4, " four");
            dic.Add(5, " five");
            dic.Add(6, " six");
            dic.Add(7, " seven");
            dic.Add(8, " eight");
            dic.Add(9, " nine");
            dic.Add(10, " ten");
            dic.Add(11, " eleven");
            dic.Add(12, " twelve");
            dic.Add(13, " thirteen");
            dic.Add(14, " fourteen");
            dic.Add(15, " fifteen");
            dic.Add(16, " sixteen");
            dic.Add(17, " seventeen");
            dic.Add(18, " eighteen");
            dic.Add(19, " nineteen");
            dic.Add(20, " twenty");
            dic.Add(30, " thirty");
            dic.Add(40, " forty");
            dic.Add(50, " fifty");
            dic.Add(60, " sixty");
            dic.Add(70, " seventy");
            dic.Add(80, " eighty");
            dic.Add(90, " ninety");
            dic.Add(100, " hundred");

            return dic[key];

        }
 
        [HttpPost]
        public ActionResult Index(NumToWord NumToWord)
        {
          ViewBag.result = NumtoWd(NumToWord.number).ToUpper();

            return View();
 
        }

        public ActionResult GetAll() {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllData() {

            var all = _numtowordservice.GetAll();

            return Json(all, JsonRequestBehavior.AllowGet);

         }

        public ActionResult AddOne() {

            ViewBag.ShouldClose = false;

            return View();
        }

        [HttpPost]
        public ActionResult Addone(NumToWord numtoword) {

            if (ModelState.IsValid) { 

               _numtowordservice.Add(numtoword);

            }

            ViewBag.ShouldClose = true;

            return View();

        }

        [HttpPost]
        public ActionResult AjaxAddone(string numtoword)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();

            NumToWord data = js.Deserialize<NumToWord>(numtoword);

            if (ModelState.IsValid) {

            _numtowordservice.Add(data);

            }

            ViewBag.ShouldClose = true;

            return RedirectToAction("Index");

        }



    }

 


}