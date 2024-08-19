using AbcYazilim.OgrenciTakip.Model.Attributes;
using AbcYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcYazilim.OgrenciTakip.Model.Entities.Base
{
    public class BaseEntity:IBaseEntity
    {
        [Column(Order =0),Key, DatabaseGenerated(DatabaseGeneratedOption.None)]  //datatable da creat ederken en üst sıraya koy buradakileri demek  // DatabaseGenerated(DatabaseGeneratedOption.None diyerek id sini otomatik artan olmasın biz vereceğiz demek 
        public long Id { get; set; }  //long olmasının nedeni burdaki Id yi biz oluşturup database e göndereceğiz .
                                      //NaseHareketEntity de ise int verdik çünkü database tarafınfan otamatik olarak üretilecek bu yüzden int kullanıldı.

        [Column(Order = 1),Required,StringLength(20),Kod("Kod","txtKod"),ZorunluAlan("Kod", "txtKod")]  //Required diyerek artık null olamaz oluyor /  StringLength(20) max 20 karakter olacak
        public virtual string Kod { get; set; }    //`BaseEntity` sınıfındaki `Kod` özelliği, miras alınan sınıflarda değiştirilebilmesi için `virtual` yapılmıştır. `Okul` sınıfı bu özelliği kendi özelinde kullanmak için `override` ederek özelleştirmiştir.
    }
}
