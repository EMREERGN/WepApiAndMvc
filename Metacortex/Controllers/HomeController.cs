using Metacortex.helper;
using Metacortex.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml;

namespace Metacortex.Controllers
{
    public class HomeController : Controller
    {
        DovizDataHelper dovizHelper = new DovizDataHelper();

        public ActionResult Index()
        {

            ViewBag.NameList = dovizHelper.getDovizNameList(); // dropdown menü için döviz isimleri listesi döndürülür
            return View();
        }


       
        public JsonResult GetDovizDatas(string doviz, string startDate, string endDate)
        {
          
            // daha önce cachlenmiş mi, kontrol edilir
            JsonResult cachedJsonValues = Session[doviz + startDate + endDate] as JsonResult;

            // eğer ilk defa sorgu atılmış ise
            if (cachedJsonValues == null) {
                List<Doviz> dovizler = new List<Doviz>(); // Doviz Listeyi nesnesi oluşturulur
                dovizler = dovizHelper.getDovizDatas(doviz,startDate,endDate); // doviz bilgileri listesi getirilir

                // cache'leme işlemi yapılır
                Session[doviz + startDate + endDate] = Json(dovizler);
                return Json(dovizler); // return Value For Json

            }

            // Eğer veriler Cach'li ise
            else // daha önce sorgu atılmış ise yani cache'lenmiş ise
            {
                return Session[doviz + startDate + endDate] as JsonResult;
            }

        }






       
    }
}