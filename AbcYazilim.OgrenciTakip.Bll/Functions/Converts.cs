using AbcYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;

using System;
using System.Linq;

namespace AbcYazilim.OgrenciTakip.Bll.Functions
{
    public static class Converts
    {
        //Expention metod tanımlayabilmemiz için 3 tane kural var 1-classların static olması lazım 2- functionların static olması lazım 3-vermiş olduğumuz ilk değişkenin this ile tanımlanması gerekiyor 
        //bir kaynak bir tane hedefimiz olması lazım 
        public static TTarget EntityConvert<TTarget>(this IBaseEntity source)
        {
            if (source == null) return default(TTarget);
            var hedef = Activator.CreateInstance<TTarget>(); //hedeftargettan bir tane instance üretmiş olduk

            //hem hedef hemde kaynak propertylerine ulaşmak için 
            var kaynakProp = source.GetType().GetProperties();  //reflection yoluyla kaynak entitylerinin propertylerine ulaşmış olduk
            var hedefProp = typeof(TTarget).GetProperties();  //reflection yoluyla hedef entitylerinin propertylerine ulaşmış olduk

            //yukardakileri(kaynak, hedef) karşılaştırarak hedef entitylerini oluşturacağız
            foreach (var kp in kaynakProp)
            {
                var value = kp.GetValue(source);
                var hp = hedefProp.FirstOrDefault(x => x.Name == kp.Name);  //x => x.Name == kp.Name burada gelen bu kaynakProp ın ismini al hedefpropların arasından ara, eğer bunu bulabilirsen hp ye at bulamıyıorsan null gelmiş olacak.
                if (hp!=null)
                {
                    hp.SetValue(hedef, ReferenceEquals(value, "") ? null : value);
                }
            }
            return hedef;
        }
    }
}
