using Metacortex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Metacortex.helper
{
    public class DovizDataHelper
    {



        public List<Doviz> getDovizDatas(string doviz, string startDate, string endDate)
        {
            var dayList = GetDatesBetween(startDate, endDate);
            List<Doviz> dovizler = new List<Doviz>(); // Aşağıda oluşturduğum public class ile bir List oluşturuyoruz.

            XmlDocument xml = new XmlDocument(); // yeni bir XML dökümü oluşturuyoruz.
                                                
            /*
                    foreach döngüsü ile verilen tarihler arasındaki bütün verileri alıyoruz , eğer resmi tatil ve haftasonu günlerinden bilgi almak
                istenirse hata alınıucaktır o yüzden try catch içinde o durumu giderdim
            */
            foreach (var day in dayList) // seçilen aralıktaki bütün günlerdeki veriler getirilir
            {
                try
                {
                    //www.tcmb.gov.tr/kurlar/201909/01092019.xml  => tam url bu şekilde
                    xml.Load("https://www.tcmb.gov.tr/kurlar/" + day.ToString("yyyyMM") + "/" + day.ToString("ddMMyyyy") + ".xml"); // bağlantı kuruyoruz.

                    var Tarih_Date_Nodes = xml.SelectSingleNode("//Tarih_Date"); // Count değerini olmak için ana boğumu seçiyoruz.
                    var CurrencyNodes = Tarih_Date_Nodes.SelectNodes("//Currency"); // ana boğum altındaki kur boğumunu seçiyoruz.
                    int CurrencyLength = CurrencyNodes.Count; // toplam kur boğumu sayısını elde ediyor ve for döngüsünde kullanıyoruz.
                    for (int i = 0; i < CurrencyLength; i++) // for u çalıştırıyoruz.
                    {
                        var cn = CurrencyNodes[i]; // kur boğumunu alıyoruz.
                                                   // Listeye kur bilgirini ekliyoruz.
                        if (doviz == cn.ChildNodes[1].InnerXml) // sadece seçilen veriler getirilir
                        { // eğer seçilen döviz ismi aynı ise ekleme yap
                            dovizler.Add(new Doviz
                            {

                                Kod = cn.Attributes["Kod"].Value,
                                CrossOrder = cn.Attributes["CrossOrder"].Value,
                                CurrencyCode = cn.Attributes["CurrencyCode"].Value,
                                Unit = cn.ChildNodes[0].InnerXml,
                                Isim = cn.ChildNodes[1].InnerXml,
                                CurrencyName = cn.ChildNodes[2].InnerXml,
                                ForexBuying = cn.ChildNodes[3].InnerXml,
                                ForexSelling = cn.ChildNodes[4].InnerXml,
                                BanknoteBuying = cn.ChildNodes[5].InnerXml,
                                BanknoteSelling = cn.ChildNodes[6].InnerXml,
                                CrossRateOther = cn.ChildNodes[8].InnerXml,
                                CrossRateUSD = cn.ChildNodes[7].InnerXml,
                                Date = day.ToShortDateString()
                            });
                        }

                    }

                }
                catch (Exception ex)
                {
                    // eğer resmi tatil ve haftasonu güğnleri,nden veri alınmaya çalışılırsa buraya düşecektir
                }

            }

            return dovizler; // dözvizler listesini döndür
        }



        public List<string> getDovizNameList()
        {
            List<String> nameList = new List<string>(); //isim listesi oluşturulur

            XmlDocument xml = new XmlDocument(); // yeni bir XML dökümü oluşturuyoruz.
            xml.Load("http://www.tcmb.gov.tr/kurlar/today.xml"); // bağlantı kuruyoruz.
            var Tarih_Date_Nodes = xml.SelectSingleNode("//Tarih_Date"); // Count değerini olmak için ana boğumu seçiyoruz.
            var CurrencyNodes = Tarih_Date_Nodes.SelectNodes("//Currency"); // ana boğum altındaki kur boğumunu seçiyoruz.
            int CurrencyLength = CurrencyNodes.Count; // toplam kur boğumu sayısını elde ediyor ve for döngüsünde kullanıyoruz.

            for (int i = 0; i < CurrencyLength; i++) // for u çalıştırıyoruz.
            {
                var cn = CurrencyNodes[i]; // kur boğumunu alıyoruz.
                nameList.Add(cn.ChildNodes[1].InnerXml);
            }

            return nameList;
        }


        // verilen iki tarih arasındaki bütün günleri getiren fonksiyon
        private DateTime[] GetDatesBetween(string startDate, string endDate)
        {

            // string -> dateTime değişimi
            DateTime _startDate = Convert.ToDateTime(startDate);
            DateTime _endDate = Convert.ToDateTime(endDate);

            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = _startDate; date <= _endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates.ToArray();
        }
    }
}