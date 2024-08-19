using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.BaseForms;
using System;
using System.Windows.Forms;

namespace AbcYazilim.OgrenciTakip.Ul.Win.Show
{
    public class ShowListForms<TForm> where TForm: BaseListForm
    {
        public static void ShowListForm(KartTuru kartTuru)
        {
            //yetki kontrolü gelecek 

            var frm = (TForm)Activator.CreateInstance(typeof(TForm));
            frm.MdiParent = Form1.ActiveForm;

            frm.Yukle();
            frm.Show(); 
        }

        public static void ShowListForm(KartTuru kartTuru, params object[] prm)
        {
            //yetki kontrolü gelecek 

            var frm = (TForm)Activator.CreateInstance(typeof(TForm),prm);
            frm.MdiParent = Form1.ActiveForm;

            frm.Yukle();
            frm.Show();
        }
        public static BaseEntity ShowDialogListForm(KartTuru kartTuru,long? seciliGelecekId,params object[] prm)
        {
            //yetki kontrolleri olacak 

            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm), prm))
            {
                frm.SeciliGelecekId = seciliGelecekId;
                frm.Yukle();
                frm.ShowDialog();

                return frm.DialogResult == DialogResult.OK ? frm.SelectedEntity : null;
            }
        }
    }
}
