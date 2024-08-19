using AbcYazilim.Dal.InterFaces;
using AbcYazilim.OgrenciTakip.Bll.Functions;
using AbcYazilim.OgrenciTakip.Bll.InterFaces;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Common.Functions;
using AbcYazilim.OgrenciTakip.Common.Message;
using AbcYazilim.OgrenciTakip.Model.Attributes;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;



namespace AbcYazilim.OgrenciTakip.Bll.Base
{

    //<T, CTontext> dememizin nedeni birden fazla context olacağından buna generic yapı denir
    //Normal veri girişi yaptığımız formlar için olduğıundan dolayı BaseEntity kullandık, başka zaman basehareketentity yide başka zaman yapacağız
    public class BaseBll<T, TContext>:IBaseBll where T: BaseEntity where TContext: DbContext //buraya gelecek olacak entitylerimiz model katmanındaki BaseEntity den olacağı için implamente ettik 
    {

        #region Variables
        private readonly Control _ctrl;
        private IUnitOfWork<T> _uow;  //repostory imize  buradan ynai UnitOfWork  den erişiyor olacağız 
        #endregion

        private bool Validation(IslemTuru islemTuru, BaseEntity oldEntity,BaseEntity currentEntity,Expression<Func<T,bool>>filter)
        {
            var errorControl = GetValidationErrorControl();

            if (errorControl == null) return true;
            _ctrl.Controls[errorControl].Focus();
            return false;

            string GetValidationErrorControl()
            {
                string MukerrerKod()
                {
                    foreach (var property in typeof(T).GetPropertyAttributesFromType<Kod>())
                    {
                        if (property.Attribute == null) continue;

                        if ((islemTuru == IslemTuru.EntityInsert || oldEntity.Kod == currentEntity.Kod) && islemTuru == IslemTuru.EntityUpdate) continue;

                        if (_uow.Rep.Count(filter) < 1) continue;

                        Messages.MukerrerKayitHataMesaji(property.Attribute.Description);
                        return property.Attribute.ControlName;
                    }
                    return null;
                }
                string HataliGiris()
                {
                    foreach (var property in typeof(T).GetPropertyAttributesFromType<ZorunluAlan>())
                    {
                        if (property.Attribute == null) continue;
                        var value = property.Property.GetValue(currentEntity);

                        if (property.Property.PropertyType == typeof(long))
                            if ((long)value == 0) value = null;

                        if (!string.IsNullOrEmpty(value?.ToString())) continue;
                        Messages.HataliVeriMesaji(property.Attribute.Description);
                        return property.Attribute.ControlName;
                    }
                    return null;
                }
                return HataliGiris() ?? MukerrerKod();
            }
        }

        //1. ctorumuz
        protected BaseBll() { } //protected dememizin nedeni sadece implamente eden classlar ulaşmasını istiyoruz 

        //2. ctorumuz
        protected BaseBll(Control ctrl)
        {
            _ctrl = ctrl;
        }

        protected TResult BaseSingle<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            GeneralFunctions.CreatUnirOfWork<T, TContext>(ref _uow); //sisteme giriş yaparken sürekli ConnectionString değişecek
            return _uow.Rep.Find(filter, selector);
        }
        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T, bool>>filter, Expression<Func<T, TResult>> selector)
        {
            GeneralFunctions.CreatUnirOfWork<T, TContext>(ref _uow);
            return _uow.Rep.Select(filter, selector);
        }

        protected bool BaseInsert(BaseEntity entity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreatUnirOfWork<T, TContext>(ref _uow);

            //Validation kod blokları
            if (!Validation(IslemTuru.EntityInsert, null, entity, filter)) return false;
                _uow.Rep.Insert(entity.EntityConvert<T>());

            //burada gelen entity i Convert yapıp o şekilde repository e göndereceğiz
            //_uow.Rep.Insert(entity.EntityConvert<T>());
            return _uow.Save();
        }

        //2 tane entity alacak bir eski diğeri yeni halinin
        protected bool BaseUpdate(BaseEntity oldEntity, BaseEntity currentEntity,Expression<Func<T,bool>> filter)
        {
            GeneralFunctions.CreatUnirOfWork<T, TContext>(ref _uow);

            //validation işlmeleri
            if (!Validation(IslemTuru.EntityUpdate, oldEntity, currentEntity, filter)) return false;
            

            //sadece değişen propertylere update olacak 
            var degisenAlanlar = oldEntity.DegisenAlanlariGetir(currentEntity);
            //değişen alanlara sahipsek
            if (degisenAlanlar.Count == 0) return true;

            _uow.Rep.Update(currentEntity.EntityConvert<T>(), degisenAlanlar);
            return _uow.Save();
        }

        protected bool BaseDelete(BaseEntity entity, KartTuru kartTuru,bool mesajVer= true)
        {
            GeneralFunctions.CreatUnirOfWork<T, TContext>(ref _uow);
            if (mesajVer)
            {
                if (Messages.SilMesaj(kartTuru.ToName()) != DialogResult.Yes) return false;
            }
            _uow.Rep.Delete(entity.EntityConvert<T>());
            return _uow.Save();
        }

        protected string BaseYeniKodVer(KartTuru kartTuru,Expression<Func<T, string>>filter, Expression<Func<T, bool>> where = null)
        {
            GeneralFunctions.CreatUnirOfWork<T, TContext>(ref _uow);
            return _uow.Rep.YeniKodVer(kartTuru, filter, where);
        }
        #region Dispose


        //Bu kod, _ctrl ve _uow nesnelerinin Dispose metodunu çağırarak, bu nesnelerin sahip olduğu kaynakları güvenli bir şekilde serbest bırakmayı amaçlar ve null kontrolü yaparak olası hataları önler.
        public void Dispose()
        {
            _ctrl?.Dispose();
            _uow?.Dispose();
        }
        #endregion
    }
}
