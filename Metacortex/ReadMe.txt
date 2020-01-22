Projede istediğiniz bütün özelliklere dikkat ederek yaptım 
Kodlamada ise çok fazla yorum satırı kullandım tekrar tekrar burada kodları açıklama yapmayacağım

Nasıl Çalışır ?

1) Öncelikle en üstte yer alan Dropdown alanından istenilen para birimi seçilir
2) Daha Sonra Başlangıç Tarihi ve Bitiş Tarihi Seçilir
3) Getir Butonuna Basılır
4) Ekrana Seçilen para biriminin kur değerleri gösterilir
5) Para birimi değiştirlip grafik güncellenmek istenirse tekrardan "Göster" butonuna basılmalıdır
6) uygulama 2 projeden oluşmaktadır; 1) Wep Api : uygulamanın verileri bu apiden çekilir , 2)Mvc : uygulamanın arayüzü olup sizin kullanacağız arayüzdür.

Notlar: 

->Tarih Kontrolleri yapıldı (Tarih mutlaka seçilmeli , bitiş Tarihi başlangıç Tarihinden küçük olmamalı)
->Cacheleme sistemi ile tekrar tekrar request atılması engellenip daha önceki Json Verileri getirildi
->Chartjs grafiğinde ise 5 adet dataset gösterildi ve , gösterilmek istenilmeyen dataset için üzerine dokunup sade bir grafik elde edebilirsiniz
->Veri Tabanı İçin Metacortex adlı projenin içinde script dosyası yer almaktadır. Mssql Server içinde execute etmeniz veya script dosyasına bakarak veri tabanını kendiniz de oluşturabilirsiniz
, ve veri tabanına sağlıklı bir şekilde bağlanabilmek için proje içindeki "connectionString"i kendi bağlantınıza göre update etmeniz gerekicektir



