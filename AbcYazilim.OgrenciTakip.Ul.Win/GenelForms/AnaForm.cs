using AbcYazilim.OgrenciTakip.Ul.Win.Forms.OkulForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Show;
using DevExpress.XtraBars;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.IlForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.AileBilgiForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.IptalNedeniForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.YabanciDilForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.TesvikForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.KontenjanForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.RehberForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.SinifGrupForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.MeslekForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.YakinlikFolder;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.IsyeriForms;

using AbcYazilim.OgrenciTakip.Ul.Win.Forms.GorevForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.IndirimTuruForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.EvrakForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.PromosyonForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.ServisForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.HizmetTuruForms;
using System;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.HizmetForms;


namespace AbcYazilim.OgrenciTakip.Ul.Win.GenelForms
{
    public partial class AnaForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static long DonemId=1;
        public static string DonemAdi = "Dönem Bilgisi Bekleniyor...";
        public static long SubeId=1;
        public static string SubeAdi = "Şube Bilgisi Bekleniyor...";

        public static DateTime EgitimBaslamaTarihi = new DateTime(2017,09,15);
        public static DateTime DonemBitisTarihi = new DateTime(2018, 06, 30);
        public static bool GunTarihininOncesineHizmetBaslamaTarihiGirilebilir = true;
        public static bool GunTarihininSonrasinaHizmetBaslamaTarihiGirilebilir = true;

        public AnaForm()
        {
            InitializeComponent();
            EventsLoad();
        }

        private void EventsLoad()
        {
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarButtonItem btn:
                        btn.ItemClick += Butonlar_ItemClick;
                        break;
                }
            }
        }



        //burada uygulamayı ilk çalıştırdığımızda gözüken butonlar
        private void Butonlar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item == btnOkulKartlari)
                ShowListForms<OkulListForm>.ShowListForm(KartTuru.Okul);
            else if (e.Item == btnIlKartlari)
                ShowListForms<IlListForm>.ShowListForm(KartTuru.Il);
            else if (e.Item == btnAileBilgiKartklari)
                ShowListForms<AileBilgiListForm>.ShowListForm(KartTuru.AileBilgi);
            else if (e.Item == btnIptalNedeniKartlari)
                ShowListForms<IptalNedeniListForm>.ShowListForm(KartTuru.IptalNedeni);
            else if (e.Item == btnYabanciDilKartlari)
                ShowListForms<YabanciDilListForm>.ShowListForm(KartTuru.YabanciDil);
            else if (e.Item == btnTesvikKartlari)
                ShowListForms<TesvikListForm>.ShowListForm(KartTuru.Tesvik);
            else if (e.Item == btnKontenjanKartlari)
                ShowListForms<KontenjanListForm>.ShowListForm(KartTuru.Kontenjan);
            else if (e.Item == btnRehberKartlari)
                ShowListForms<RehberListForm>.ShowListForm(KartTuru.Rehber);
            else if (e.Item == btnSinifGrupKartlari)
                ShowListForms<SinifGrupListForm>.ShowListForm(KartTuru.SinifGrup);
            else if (e.Item == btnMeslekKartlari)
                ShowListForms<MeslekListForm>.ShowListForm(KartTuru.Meslek);
            else if (e.Item == btnYakinlikKartlari)
                ShowListForms<YakinlikListForm>.ShowListForm(KartTuru.Yakinlik);
            else if (e.Item == btnIsyeriKartlari)
                ShowListForms<IsyeriListForm>.ShowListForm(KartTuru.Isyeri);
            else if (e.Item == btnGorevKartlari)
                ShowListForms<GorevListForm>.ShowListForm(KartTuru.Gorev);
            else if (e.Item == btnIndirimTuruKartlari)
                ShowListForms<IndirirmTuruListForm>.ShowListForm(KartTuru.IndirimTuru);
            else if (e.Item == btnEvrakKartlari)
                ShowListForms<EvrakListForm>.ShowListForm(KartTuru.Evrak);
            else if (e.Item == btnPromosyonKartlari)
                ShowListForms<PromosyonListForm>.ShowListForm(KartTuru.Promosyon);
            else if (e.Item == btnServisYeriKartlari)
                ShowListForms<ServisListForm>.ShowListForm(KartTuru.Servis);
            else if (e.Item == btnHizmetTuruKartlari)
                ShowListForms<HizmetTuruListForm>.ShowListForm(KartTuru.HizmetTuru);
            else if (e.Item == btnHizmetKartlari)
                ShowListForms<HizmetListForm>.ShowListForm(KartTuru.Hizmet);
        }
    }
}

//Özet Bu kodda, AnaForm adlı bir form oluşturuluyor ve form yüklendiğinde EventsLoad metodu çağrılarak, ribbonControl üzerindeki tüm BarButtonItem öğelerine Butonlar_ItemClick olay işleyicisi ekleniyor.Butonlar_ItemClick metodu, tıklanan öğe btnOkulKartlari ise, OkulKartlari formunu açıyor.