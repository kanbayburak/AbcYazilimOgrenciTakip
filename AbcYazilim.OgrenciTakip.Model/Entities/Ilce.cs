using AbcYazilim.OgrenciTakip.Model.Attributes;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcYazilim.OgrenciTakip.Model.Entities
{
    public class Ilce: BaseEntityDurum
    {
        [Index("IX_Kod", IsUnique = false)] //false olmasının nedeni aynı kodları girebilmemize izin versin, bunun nedeni aynı ile ait ilçelerin kodları olabilir falan filan tam anlamadım 
        public override string Kod { get; set; } //`BaseEntity` sınıfındaki `Kod` özelliği, miras alınan sınıflarda değiştirilebilmesi için `virtual` yapılmıştır. `Il` sınıfı bu özelliği kendi özelinde kullanmak için `override` ederek özelleştirmiştir.
        [Required,StringLength(50), ZorunluAlan("İlce Adi", "txtIlceAdi")]
        public string IlceAdi { get; set; }
        public long IlId { get; set; }
        [StringLength(500)]
        public string Aciklama { get; set; }

        //ilçeEntity si İlEntitysi ile ilişkili o yüzden aşağıdaki property i ekliyoruz
        //İlçenin ile bağlı olmasını ilişkilendirdik.
        public Il Il { get; set; }
    }
}
