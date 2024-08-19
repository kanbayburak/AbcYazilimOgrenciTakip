using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.Model.Attributes
{
    public class ZorunluAlan: Attribute
    {
        public string Description { get; }
        public string ControlName { get; }


        /// <summary>
        /// Validation İşlemleri Sırasında Zorunlu Olan Alanları İşaretlemek İçin Kullanılacak.
        /// </summary>
        /// <param name="description"> Uyarı Mesajında Gösterilecek Açıklama </param>
        /// <param name="controlName"> Uyarı Mesajı Sonrası Focuslanacak Control Adı </param>
        public ZorunluAlan(string description, string controlName)
        {
            Description = description;
            ControlName = controlName;
        }
    }
}
