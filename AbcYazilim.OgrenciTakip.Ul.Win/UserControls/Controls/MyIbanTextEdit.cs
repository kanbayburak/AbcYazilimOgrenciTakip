using DevExpress.XtraEditors.Mask;
using System.ComponentModel;

namespace AbcYazilim.OgrenciTakip.Ul.Win.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyIbanTextEdit: MyTextEdit
    {
        public MyIbanTextEdit()
        {
            Properties.Mask.MaskType = MaskType.Regular;
            Properties.Mask.EditMask = @"TR\d?\d? \d?\d?\d?\d? \d?\d?\d?\d? \d?\d?\d?\d? \d?\d?\d?\d? \d?\d?";
            //ıban yazılmadan önce hepsine 0 yazıyor bunun oluşmaması için 
            Properties.Mask.AutoComplete = AutoCompleteType.None;
            StatusBarAciklama = "İban No Giriniz.";
        }
    }
}
