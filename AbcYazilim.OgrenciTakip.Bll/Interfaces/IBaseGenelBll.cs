﻿using AbcYazilim.OgrenciTakip.Model.Entities.Base;

namespace AbcYazilim.OgrenciTakip.Bll.Interfaces
{
    public interface IBaseGenelBll
    {
        bool Insert(BaseEntity entity);
        bool UpDate(BaseEntity oldEntity, BaseEntity currentEntity);
        string YeniKodVer();
    }
}
