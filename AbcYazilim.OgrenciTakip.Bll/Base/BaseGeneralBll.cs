using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Data.Contexts;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.Bll.Base
{
    public class BaseGeneralBll<T>:BaseBll<T,OgrenciTakipContext> where T: BaseEntity
    {
        //2.8.video
        #region Variables

        private KartTuru _kartTuru; 

        #endregion
        public BaseGeneralBll(KartTuru kartTuru) { _kartTuru = kartTuru; }

        public BaseGeneralBll(Control ctrl, KartTuru kartTuru) : base(ctrl) { _kartTuru = kartTuru; }

        public virtual BaseEntity Single(Expression<Func<T, bool>> filter)
        {
            //Enittylerin propertylerine ulaşmak için süslü parantez ile yapacağız
            return BaseSingle(filter, x => x);
        }

        //******UYARI: BaseListin üzerine gelince IQueryable türünden bir değer dödürdüğünü anlıyoruz. IQueryable sadece string bir sorgu ifadesi döndürür. çalıştırdığımız vakit hata verecektir çünkü database e gitme olayı olmuyor. database e git çalış diyebilmemiz için adımalr 1-using System.Linq; ekle,  2-.OrderBy(x => x.Kod).ToList()  eklemesini yap*******
        public virtual IEnumerable<BaseEntity> List(Expression<Func<T, bool>> filter)
        {
            return BaseList(filter, x => x).OrderBy(x => x.Kod).ToList();
        }
        public bool Insert(BaseEntity entity)  //kaydedebilmesi için enetity ihtiyacımız var
        {
            return BaseInsert(entity, x => x.Kod == entity.Kod);  // VALİDATİON İŞLEMLERİ İÇİN KULLANILACAK, İŞLEM YAPACAĞIMIZ ZAMAN BU KODU AÇIKLAYACAĞIZ 
        }

        public bool Insert(BaseEntity entity, Expression<Func<T, bool>> filter)
        {
            return BaseInsert(entity, filter);  //editformdan veya listformdan gelemnle filter yapacağız
        }

        public bool UpDate(BaseEntity oldEntity, BaseEntity currentEntity)
        {
            return BaseUpdate(oldEntity, currentEntity, x => x.Kod == currentEntity.Kod);  // VALİDATİON İŞLEMLERİ İÇİN KULLANILACAK, İŞLEM YAPACAĞIMIZ ZAMAN BU KODU AÇIKLAYACAĞIZ 
        }

        public bool UpDate(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<T, bool>> filter)
        {
            return BaseUpdate(oldEntity, currentEntity, filter);  //baseeditformdan veya listformdan gelemnle filter yapacağız
        }

        public bool Delete(BaseEntity entity)
        {
            return BaseDelete(entity, _kartTuru);
        }

        public string YeniKodVer()
        {
            return BaseYeniKodVer(_kartTuru, x => x.Kod); //bu şekilde git kod alanının en büyük değerini getir, yoksa eğer kendisi üretip gelecek
        }
        public string YeniKodVer(Expression<Func<T, bool>> filter)
        {
            return BaseYeniKodVer(_kartTuru, x => x.Kod, filter);
        }

    }
}
