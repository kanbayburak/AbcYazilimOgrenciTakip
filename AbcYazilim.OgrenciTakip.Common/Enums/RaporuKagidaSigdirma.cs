using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Common.Enums
{
    public enum RaporuKagidaSigdirma: byte
    {
        [Description("Sütunları Daraltarak Sığdır")]
        SutunlariDaraltarakSigdir =1,
        [Description("YazıBoyutunu Küçülterek Sığdır")]
        YaziBoyutunuKuculterekSigdir =2,
        [Description("İşlem Yapma")]
        IslemYapma =3,
    }
}
