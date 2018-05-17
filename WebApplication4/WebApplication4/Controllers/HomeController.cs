using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<SentenceModel> list = Logic.ConvertOut();
            if (list == null)
            {
                return View();
            }
            else
            {
                return View(list);
            }
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string searchWord)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string word = searchWord;
                    StreamReader reader = new StreamReader(file.InputStream);
                    string textFile = reader.ReadToEnd();
                    Logic.ConvertToJson(textFile, word);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            if (null != Logic.ConvertOut())
            {
                return View(Logic.ConvertOut());
            }
            else
            {
                return View();
            }
        }
    }
}