using Metacortex.helper;
using Metacortex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace WepApi.Controllers
{
    public class ValuesController : ApiController
    {

        DovizDataHelper dovizHelper = new DovizDataHelper();
        
        
        // GET api/values
        public IHttpActionResult Get()
        {
            // test amaçlı yazılan get isteiğ
            string doviz = "ABD DOLARI";
            string startDate = "01.11.2019";
            string endDate = "17.11.2019";


            List<Doviz> dovizler = new List<Doviz>(); // Doviz Listeyi nesnesi oluşturulur
            dovizler = dovizHelper.getDovizDatas(doviz, startDate, endDate); // doviz bilgileri listesi getirilir

            return Json(dovizler); // return Value For Json
        }


        // GET api/values/5
        
        public IHttpActionResult Get(string doviz,string startDate,string endDate)
        {
            // String düzenlemeleri
            // url üzerinde gelen string düzen
            string _doviz = doviz.Replace("_", " "); // _ olan işareti boluk olarak değiştir
            string _startDate = startDate.Replace("_", ".");
            string _endDate = endDate.Replace("_", ".");

            List<Doviz> dovizler = new List<Doviz>(); // Doviz Listeyi nesnesi oluşturulur
            dovizler = dovizHelper.getDovizDatas(_doviz, _startDate, _endDate); // doviz bilgileri listesi getirilir

            return Json(dovizler); //Json Olarak sonuçları döndürür
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
