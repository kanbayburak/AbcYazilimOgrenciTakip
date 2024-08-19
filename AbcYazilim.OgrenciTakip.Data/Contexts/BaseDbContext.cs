using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace AbcYazilim.OgrenciTakip.Data.Contexts
{
    //Not: Genereik tipte olması lazım ki her contexte rahatlıkla implamente edelim
    //yapılışı: <TContect,TConfiguration> ekledik ve uygulamasını gerçekleştirdik

    public class BaseDbContext<TContext,TConfiguration>: DbContext where TContext: DbContext where TConfiguration: DbMigrationsConfiguration<TContext>, new()  //1)buradaki TContext context OgrenciTakipContext i implamente ediyor //2)birde yapacağımız magration işlemlerindeki configurationlar için takip edeceğimiz TConfiguration  //3) where TContext: DbContext where TConfiguration: DbMigrationsConfiguration<TContext>, new() bunlarda TContext ve TConfiguration neyi tmesil ediyor onu açıklıyoruz.
    {
        private static string _nameorConnectionString=typeof(TContext).Name;


        //connectionStringimizin Name ini göndereceğiz, kendisini değil. Çünkü yönetim modülünde sürekli connectionStringi değiştireceğiz, her firma ayrı ayrı kendine özel connectionStringi olması gerektiği için 
        public BaseDbContext():base(_nameorConnectionString) { }

        //KISACASI: TABLE LARI GÜNCELLE CONTRUCTERIDIR. Özetle: bu configuration yani bu constructer ile gwlinirse, Database.SetInitializer.... database ine bağlanacak Entity deki modellerimize bakacak, ve database deki table lar ile karşılaştırma işlemini yapacak değişiklik varsa Migration yoluyla değişiklikleri sağlar
        public BaseDbContext(string connectionString) : base(connectionString)
        {
            //mesela entity bölümündeki okul kısmına yeni bir property ekliyelim. işte bu eklediğimiz yeni alanın otomatik olarak bulup migration yoluyla base e aktarılma işlemini sağlar bu kod
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TConfiguration>());
            _nameorConnectionString = connectionString;
        }
    }
}


//BU BASE İ YAPMAMIZIN NEDENİ DİĞER CONTEXTLERDE KULLANMAK İÇİN SÜREKLİ AYNI KODU YAZMAMAK İÇİN


