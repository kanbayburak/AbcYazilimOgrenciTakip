using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace AbcYazilim.OgrenciTakip.Common.Functions
{
    public static class EnumFunctions
    {

        private static T GetAttribute<T>(this Enum value) where T: Attribute                    //GetAttribute function tanımladık ve Enum tipinde bir değer alıyor

        {
            if (value == null) return null;                                                     //daha sonra null olma durumunu değerlendiriyoruz

            var memberInfo = value.GetType().GetMember(value.ToString());                       //attributelara ulaşmak için memberInfo tipinde bir tane değişlkene ihtiyacımız var ve buradaki value lar KartTuru ndeki enumlar(okul=1 .... vs) yani value dan biz type ına ulaşıyoruz. type türü ise KartTuru dür. memeberInfo[0] sayesinde ise okul un attribute ini (definationını) aldık

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }

        public static string ToName(this Enum value)                                             // üsten aldığuımız attribute ın value sunu alacağız 
        {
            if (value == null) return null;
            var attribute = value.GetAttribute<DescriptionAttribute>();                          //bu gelen valuenun attribute leri arasında dolaş ve Description tipinde olan attribute geri gönder
            return attribute == null ? value.ToString() : attribute.Description;                 //eğer gelen bu attribute ın herhangi bir description ı yoksa o zaman normal kartın adını gönder, eğer varsa bu attribute in description ını gönder
        }

        public static ICollection GetEnumDescriptionList<T>()
            {
            return typeof(T).GetMembers()
                .SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), true)
                .Cast<DescriptionAttribute>())
                .Select(x => x.Description).ToList();
            }                   
        public static T GetEnum<T>(this string description)
        {
            var enumNames = Enum.GetNames(typeof(T));

            foreach (var e in enumNames.Select(x=>Enum.Parse(typeof(T),x)).Where(y=>description==ToName((Enum)y)))
                return (T)e;

            return default(T);
        }
    }
}
