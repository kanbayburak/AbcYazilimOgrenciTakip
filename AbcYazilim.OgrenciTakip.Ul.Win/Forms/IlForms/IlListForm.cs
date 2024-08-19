using AbcYazilim.OgrenciTakip.Bll.General;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Model.Entities;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.BaseForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.IlceForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Functions;
using AbcYazilim.OgrenciTakip.Ul.Win.Show;
using DevExpress.XtraBars;

namespace AbcYazilim.OgrenciTakip.Ul.Win.Forms.IlForms
{
    public partial class IlListForm : BaseListForm
    {
        public IlListForm()
        {
            InitializeComponent();
            Bll = new IlBll();
            btnBagliKartlar.Caption = "İlçe Kartlari";
        }
        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.Il;
            FormShow = new ShowEditForms<IlEditForm>();
            Navigator = longNavigator.Navigator;
            


            if (IsMdiChild)
                ShowItems = new BarItem[] {btnBagliKartlar};
        }
        protected override void Listele()
        {
            Tablo.GridControl.DataSource = ((IlBll)Bll).List(FilterFunctions.Filter<Il>(AktifKartlariGoster));
        }

        protected override void BagliKartAC()
        {
            var Entity = Tablo.GetRow<Il>();
            if (Entity == null) return;
            ShowListForms<IlceListForm>.ShowListForm(KartTuru.Ilce, Entity.Id, Entity.IlAdi);
        }
    }
}