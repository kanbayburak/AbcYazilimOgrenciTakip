using AbcYazilim.OgrenciTakip.Data.OgrenciTakipMagration;
using AbcYazilim.OgrenciTakip.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AbcYazilim.OgrenciTakip.Data.Contexts
{

    //OgrenciTakipContext bir nevi bizim database imizdir. yani i�erisine entiylerimizi ekleyece�iz ve connection bilgilerimizide i�erisinde bar�nd�r�yor 
    public class OgrenciTakipContext : BaseDbContext<OgrenciTakipContext,Configuration>// TConext = OgrenciTakipContext ve TConfiguration =   Configuration   a kar��l�k geliyor.
    {
       
        public OgrenciTakipContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // entitylerdeki mesela Okul u �o�ul yap�yor bunun �n�ne ge�mek i�in 
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); // mesela Entity deki �l silindi�i vakit o ile ba�l� il�eleri de sil k�sm�n� devre d��� b�rak�yoruz
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); 
        }

        //olu�turdu�umuz her entity i burada tan�mlayaca��z
        //database lerdeki gibi table olarak farz edebiliriz 
        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Okul> Okul { get; set; }
        public DbSet<Filtre> Filtre { get; set; }
        public DbSet<AileBilgi> AileBilgi { get; set; }
        public DbSet<IptalNedeni> IptalNedeni { get; set; }
        public DbSet<YabanciDil> YabanciDil { get; set; }
        public DbSet<Tesvik> Tesvik { get; set; }
        public DbSet<Kontenjan> Kontenjan { get; set; }
        public DbSet<Rehber> Rehber { get; set; }
        public DbSet<SinifGrup> SinifGrup { get; set; }
        public DbSet<Meslek> Meslek { get; set; }
        public DbSet<Yakinlik> Yakinlik { get; set; }
        public DbSet<Isyeri> Isyeri { get; set; }
        public DbSet<Gorev> Gorev { get; set; }
        public DbSet<IndirimTuru> IndirimTuru { get; set; }
        public DbSet<Evrak> Evrak { get; set; }
        public DbSet<Sube> Sube { get; set; }
        public DbSet<Donem> Donem { get; set; }
        public DbSet<Promosyon> Promosyon { get; set; }
        public DbSet<Servis> Servis { get; set; }
        public DbSet<HizmetTuru> HizmetTuru { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
    }
} 