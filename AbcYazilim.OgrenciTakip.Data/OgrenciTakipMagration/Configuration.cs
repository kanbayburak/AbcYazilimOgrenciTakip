using AbcYazilim.OgrenciTakip.Data.Contexts;
using System.Data.Entity.Migrations;

namespace AbcYazilim.OgrenciTakip.Data.OgrenciTakipMagration
{
    public class Configuration: DbMigrationsConfiguration<OgrenciTakipContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; //migration işlmelerini otamatik yap demiş olduk
            AutomaticMigrationDataLossAllowed = true;  //migration işlmelerinde veri kayıpları olma durumunda kayıp olsuna izin veriyoruz
        }
    }
}
