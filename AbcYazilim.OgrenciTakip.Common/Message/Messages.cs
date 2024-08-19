using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.Common.Message
{
    public class Messages
    {
       

        //bir function oluşturduk 
        public static void HataMesaji(string hataMesaji)
        {
            XtraMessageBox.Show(hataMesaji, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); //error burada error iconu gelmesini sağlıyor 
        }

        public static void UayriMesaji(string uayariMesaji)
        {
            XtraMessageBox.Show(uayariMesaji, "Uyar", MessageBoxButtons.OK, MessageBoxIcon.Information); //Information burada Information iconu gelmesini sağlıyor 
        }


        public static DialogResult EvetSeciliEvetHayir(string mesaj, string baslik)
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);  //MessageBoxDefaultButton.Button1 seçerek yes seçeneğinin seçili olarak gelmesini belirttik
        }
        public static DialogResult HayirSeciliEvetHayir(string mesaj, string baslik)
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);  //MessageBoxDefaultButton.Button2 seçerek no seçeneğinin seçili olarak gelmesini belirttik
        }
        public static DialogResult EvetSeciliEvetHayirIptal(string mesaj, string baslik)
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult SilMesaj(string kartAdi)
        {
            return HayirSeciliEvetHayir($"Seçtiğiniz {kartAdi} Silinecektir. Onaylıyor musunuz?", "Silme Onayı");
        }

        public static DialogResult KapanisMesaj()
        {
            return EvetSeciliEvetHayirIptal("Yapılan Değişiklikler Kaydedilsin mi?", "Çıkış Onay");
        }

        public static DialogResult KayitMesaj()
        {
            return EvetSeciliEvetHayir("Yapılan Değişiklikler Kaydedilsin mi?", "Kayıt Onay");
        }
        public static void KartSecmemeUyariMesaji()
        {
            UayriMesaji("Lütfen Bir Kart Seçiniz.");
        }
        public static void MukerrerKayitHataMesaji(string alanAdi)
        {
            HataMesaji($"Girmiş Olduğunuz {alanAdi} Daha Önce Kullanılmıştır.");
        }

        public static void HataliVeriMesaji(string alanAdi)
        {
            HataMesaji($"{alanAdi} Alanına Geçerli Bir Değer Girmelisiniz.");
        }

        public static DialogResult TabloExportMesaji(string dosyaFormati)
        {
            return EvetSeciliEvetHayir($"İlgili Tablo, {dosyaFormati} Olarak DışarıAktarılacaktır. Aktarım Onay?", "Onaylıyor Musunuz?");
        }

    }
}
