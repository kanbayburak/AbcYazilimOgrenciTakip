using AbcYazilim.OgrenciTakip.Bll.General;
using AbcYazilim.OgrenciTakip.Common.Enums;
using AbcYazilim.OgrenciTakip.Ul.Win.Forms.BaseForms;
using AbcYazilim.OgrenciTakip.Ul.Win.GenelForms;
using AbcYazilim.OgrenciTakip.Ul.Win.Show;

namespace AbcYazilim.OgrenciTakip.Ul.Win.Forms.EvrakForms
{
    public partial class EvrakListForm : BaseListForm
    {
        public EvrakListForm()
        {
            InitializeComponent();

            Bll = new EvrakBll();
        }

        protected override void DegiskenleriDoldur()
        {
            Tablo = tablo;
            BaseKartTuru = KartTuru.Evrak;
            FormShow = new ShowEditForms<EvrakEditForm>();
            Navigator = longNavigator.Navigator;
        }
        protected override void Listele()
        {
            Tablo.GridControl.DataSource = ((EvrakBll)Bll).List(x=>x.Durum == AktifKartlariGoster && x.SubeId == AnaForm.SubeId && x.DonemId == AnaForm.DonemId);
        }
    }
}