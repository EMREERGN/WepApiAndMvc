using ClosedXML.Excel;
using Metacortex.helper;
using Metacortex.Models;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Xml;

namespace Metacortex.Controllers
{
    public class HomeController : Controller
    {
        // veri tabanı bağlantısı
        wepApiMvcDbEntities db = new wepApiMvcDbEntities();
        // Wep Api Bağlantısı kurulur
        HttpClient wepApiClient = new HttpClient();
        static Uri uri;
       

        DovizDataHelper dovizHelper = new DovizDataHelper();
        JsonResult lastJsonDatas; // Excell ile indirilecek son Json Verileri nesnesi

        public ActionResult Index()
        {

            ViewBag.NameList = dovizHelper.getDovizNameList(); // dropdown menü için döviz isimleri listesi döndürülür
            return View();
        }



        public JsonResult GetDovizDatas(string doviz, string startDate, string endDate)
        {

            // istenilen verileri veri tabanına kaydeden methot
            saveDataOnDb(doviz, startDate, endDate);


            // request için string düzenlemeleri
            string _doviz = doviz.Replace(" ", "_");
            string _startDate = startDate.Replace(".", "_");
            string _endDate = endDate.Replace(".", "_");

            // daha önce cachlenmiş mi, kontrol edilir
            JsonResult cachedJsonValues = Session[doviz + startDate + endDate] as JsonResult; // Eğer aynı request daha önceden atılmış ise direk response olarak döndürülür

            // eğer ilk defa sorgu atılmış ise
            if (cachedJsonValues == null)
            {

                // seçilen bilgilere göre api'ya istek atılır
                wepApiClient.BaseAddress = new Uri("https://localhost:44381/api/values/?doviz=" + _doviz + "&startDate=" + _startDate + "&endDate=" + _endDate);
                wepApiClient.DefaultRequestHeaders.Clear();
                wepApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                uri = wepApiClient.BaseAddress;

                HttpResponseMessage response = wepApiClient.GetAsync(wepApiClient.BaseAddress).Result;
                // Eğer sonuç başarılı ise Json VErileri grafiğe gönderilir
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var account = JsonConvert.DeserializeObject<List<Doviz>>(result);
                    Session[doviz + startDate + endDate] = Json(account); // Sessionda aynı sorgu atılır ise daha sonra direk çekmek için saklanır

                    lastJsonDatas = Json(account); //Güncel Json Dosyaları global olarak tutulur

                    return lastJsonDatas;
                }
                else
                    return Json(new { });

            }

            // Eğer veriler Cach'li ise
            else // daha önce sorgu atılmış ise yani cache'lenmiş ise
            {
                lastJsonDatas = Session[doviz + startDate + endDate] as JsonResult;  //Güncel Json Dosyaları global olarak tutulur
                return lastJsonDatas;
            }

        }

        private void saveDataOnDb(string doviz, string startDate, string endDate)
        {
            DovizTable dovizTable = new DovizTable();
            
            dovizTable.Doviz = doviz;
            dovizTable.StartDate = startDate;
            dovizTable.EndDate = endDate;
            dovizTable.QueryDate = DateTime.Now; // Veri analizi için sorgunun çekildiği tarih veri tabanına kaydedilir

            if (ModelState.IsValid)
            {
                db.DovizTable.Add(dovizTable);
                try
                {
                    db.SaveChangesAsync();
                }catch(Exception ex)
                {
                    var deneme = ex.Message.ToString();

                }

            }


        }
    }
}