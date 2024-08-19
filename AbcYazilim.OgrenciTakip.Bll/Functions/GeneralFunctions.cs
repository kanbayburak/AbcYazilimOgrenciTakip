using AbcYazilim.Dal.Base;
using AbcYazilim.Dal.InterFaces;
using AbcYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.Bll.Functions
{
    public static class GeneralFunctions
    {
        //DegisenAlanlariGetir function ını tanımladık
        public static List<string>DegisenAlanlariGetir<T>(this T oldEntity, T currentEntity)
        {
            List<string> alanlar = new List<string>();

            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                var oldValue = prop.GetValue(oldEntity) ?? string.Empty;  //??, prop.GetValue(oldEntity) ile gelen değer null ise o zaman null değilde string.Empty olarak bu old değeri al 
                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                //dikkat! bu verinin byte dizisi olup olmadığına dikkat edeceğiz.bunun için,
                //çok anlamadım ama yapılan işlem birisi byte diğerinin türü farklı, bu durumda karşılaştırma yapamayız bunun için aşağıdaki işlenmi yaptık 
                if (prop.PropertyType==typeof(byte[])) 
                {
                    if (string.IsNullOrEmpty(oldValue.ToString()))
                    {
                        oldValue = new byte[] { 0 };
                    }
                    if(string.IsNullOrEmpty(currentEntity.ToString()))
                    {
                        currentValue = new byte[] { 0 };
                    }
                    //2 si byte ise şimdi bu 2 alanı karşılaştıracağız
                    if (((byte[])oldValue).Length!=((byte[])currentValue).Length)
                    {
                        alanlar.Add(prop.Name);
                    }
                    else if (!currentValue.Equals(oldValue))
                    {
                        alanlar.Add(prop.Name);
                    }
                }
            }
            return alanlar;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["OgrenciTakipContext"].ConnectionString;
        }

        //ANLAMADIM 
        private static TContext CreateContext<TContext>() where TContext: DbContext
        {
            return (TContext)Activator.CreateInstance(typeof(TContext), GetConnectionString());
        }

        public static void CreatUnirOfWork<T, TContext>(ref IUnitOfWork<T> uow) where T: class,IBaseEntity where TContext:DbContext 
        {
            uow?.Dispose();
            uow = new UnitOfWork<T>(CreateContext<TContext>());
        }
    }
}
