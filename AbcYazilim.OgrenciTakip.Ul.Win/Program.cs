//using AbcYazilim.OgrenciTakip.Ul.Win.GenelForms;
//using System;
//using System.Windows.Forms;

//namespace AbcYazilim.OgrenciTakip.Ul.Win
//{
//    static class Program
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new AnaForm());
//        }
//    }
//}

using System;
using System.Windows.Forms;
using System.Data.Entity.Migrations;
using AbcYazilim.OgrenciTakip.Data.Contexts;
using AbcYazilim.OgrenciTakip.Data.OgrenciTakipMagration;
using AbcYazilim.OgrenciTakip.Ul.Win.GenelForms;

namespace AbcYazilim.OgrenciTakip.Ul.Win
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //// Veritabanını sıfırlama ve migration işlemi
            //using (var context = new OgrenciTakipContext())
            //{
            //    context.Database.Delete(); // Veritabanını siler
            //    Console.WriteLine("Veritabanı başarıyla silindi.");

            //    // Migration işlemlerini uygular
            //    var migrator = new DbMigrator(new Configuration());
            //    migrator.Update(); // Migration'ları veritabanına uygular
            //    Console.WriteLine("Veritabanı başarıyla güncellendi.");
            //}

            // WinForms uygulamasını başlatma
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnaForm());
        }
    }
}

