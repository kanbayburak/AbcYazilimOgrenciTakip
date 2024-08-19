using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Entities.Base.Interfaces;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.BaseForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Show.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.OgrenciTakip.Ul.Win.Show
{
    public class ShowEditForms<TForm>: IBaseFormShow where TForm : BaseEditForm
    {
        public long ShowDialogEditForm(KartTuru kartTuru, long id) //,params object[] prm
        {
            //yetki kontrolü gelecek 

            using (var frm= (TForm)Activator.CreateInstance(typeof(TForm)))
            {

                //buradaki IslemTuru, Id, Yukle BaseEditForm dan geldi
                frm.BaseIslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                frm.Id = id;
                frm.Yukle();
                frm.ShowDialog();
                return frm.RefresYapilacak ? frm.Id : 0;  //formda değişiklik yapılacaksa true oldu o zaman bu formun id sibni geri gönder, değilse 0 olarak gönder diyor
            }
        }

        public static long ShowDialogEditForm(KartTuru kartTuru, long id ,params object[] prm)
        {
            //yetki kontrolü gelecek 

            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm),prm))
            {

                //buradaki IslemTuru, Id, Yukle BaseEditForm dan geldi
                frm.BaseIslemTuru = id > 0 ? IslemTuru.EntityUpdate : IslemTuru.EntityInsert;
                frm.Id = id;
                frm.Yukle();
                frm.ShowDialog();
                return frm.RefresYapilacak ? frm.Id : 0;  //formda değişiklik yapılacaksa true oldu o zaman bu formun id sibni geri gönder, değilse 0 olarak gönder diyor
            }
        }
        public static T ShowDialogEditForm<T>(params object[] prm) where T: IBaseEntity
        {
            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm), prm))
            {
                frm.Yukle();
                frm.ShowDialog();
                return (T)frm.ReturnEntity();
            }
        }
    }
}
