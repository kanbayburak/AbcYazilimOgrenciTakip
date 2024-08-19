﻿using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Common.Enums
{
    public enum HizmetTipi:byte
    {
        [Description("Eğitim")]
        Egitim=1,
        [Description("Yemek")]
        Yemek =2,
        [Description("Servis")]
        Servis =3,
        [Description("Pansiyon")]
        Pansiyon = 4,
        [Description("Diger")]
        Diger =5,
    }
}
