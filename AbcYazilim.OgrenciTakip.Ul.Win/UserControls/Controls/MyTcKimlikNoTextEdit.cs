using DevExpress.Utils;
using System.ComponentModel;
using DevExpress.XtraEditors.Mask;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyTcKimlikNoTextEdit: MyTextEdit 
    {
        public MyTcKimlikNoTextEdit()
        {
            Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //kendi mask ımızı kendimiz tasarlayacağız bu yüzden regular dedik
            Properties.Mask.MaskType = MaskType.Regular;
            Properties.Mask.EditMask = @"\d?\d?\d? \d?\d?\d? \d?\d?\d? \d?\d? ";

            Properties.Mask.AutoComplete = AutoCompleteType.None;
            //MyTextEditten implament olarak geidiği için bu şekilde yazdık
            StatusBarAciklama = "TC Kimlik No Giriniz.";
        }
    }
}
