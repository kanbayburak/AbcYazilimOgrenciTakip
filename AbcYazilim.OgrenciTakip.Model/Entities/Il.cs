using AbcYazilim.OgrenciTakip.Model.Attributes;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcYazilim.OgrenciTakip.Model.Entities
{
    public class Il : BaseEntityDurum
    {
        [Index("IX_Kod", IsUnique = true)]
        public override string Kod { get; set; } //`BaseEntity` sınıfındaki `Kod` özelliği, miras alınan sınıflarda değiştirilebilmesi için `virtual` yapılmıştır. `Il` sınıfı bu özelliği kendi özelinde kullanmak için `override` ederek özelleştirmiştir.

        [Required,StringLength(50),ZorunluAlan("İl Adi","txtIlAdi")]
        public string IlAdi { get; set; }
        [StringLength(500)]
        public string Aciklama { get; set; }

        //public ICollection<Ilce> Ilce {get; set; } //İle bağlı ilçeleri ICollection ile ulaşabiliriz 
    }
}
