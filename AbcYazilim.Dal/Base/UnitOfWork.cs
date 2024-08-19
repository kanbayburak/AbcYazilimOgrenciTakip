using AbcYazilim.Dal.InterFaces;
using AbcYazilim.OgrenciTakip.Common.Message;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace AbcYazilim.Dal.Base
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {

        #region Variables
        private readonly DbContext _context; 
        #endregion
        
        //constrcution oluştur. save işlemini yapabilmemiz için. Bu context i tyerel değişkenlerde kullanabilmemiz için _context oluşturmamız lazımdı üstte oluşturduk
        public UnitOfWork(DbContext context)
        {
            if (context == null) return;
            {
                _context = context;
            }
        }
        //bu sayede UnitOfWorks den Repository e ulaşabileceğiz.
        public IRepository<T> Rep => new Repository<T>(_context);

        //hataları ayıklayabilmemiz için save blogunun arasında try chach kullanmalıyız 
        public bool Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                var sqlEx = (SqlException)ex.InnerException?.InnerException;
                if (sqlEx == null)
                {
                    //common katmamnından message ı çekmemiz lazım //Bunu referanslar kısmından cı-ommon kısmını seçtik o şekilde implemante ettik
                    Messages.HataMesaji(ex.Message);
                    return false;
                }
                switch (sqlEx.Number)
                {
                    case 208:
                        Messages.HataMesaji("İşlem Yapmak İstediğiniz Tablo Veri Tabanında BUlunamadı.");
                        break;
                    case 547:
                        Messages.HataMesaji("Seçilen Kartın İşlme Görmüş Hareketleri var Kart Silinemez.");
                        break;
                    case 2601:
                    case 2627:
                        Messages.HataMesaji("Girmiş Olduğunuz Id Daha Önce Kullanılmıştır.");
                        break;
                    case 4060:
                        Messages.HataMesaji("İŞlem Yapmak İstediğiniz VeriTabanı Sunucuda Bulunamadı.");
                        break;
                    case 18456:
                        Messages.HataMesaji("Server'a Bağlanılmak istenen Kullanıcı Adı ve Şifre Hatalıdır.");
                        break;
                    default:
                        Messages.HataMesaji(sqlEx.Message);
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.HataMesaji(ex.Message);
                return false;

            }
            return true;
        }

        #region Dispose
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }


                _disposedValue = true;
            }
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

            #endregion
        }
    }
}
