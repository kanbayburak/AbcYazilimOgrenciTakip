using AbcYazilim.OgrenciTakip.Common.Enums;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AbcYazilim.Dal.InterFaces
{

    //T nin aslında bir anlamı yok farklı bir şeyde yazılabilirdi. Anlamı ise herhangi bir tipde bir Entity gelecek olduğunu belirtmiş oluyoruz   
    //IDisposable ile implemente ettik bunun nedeni bu interface i implemente eden class lar işlmeleri bittikten sonra hafızadan siliyor işlemine tabi tutmuş oluyoruz
    public interface IRepository<T>:IDisposable where T: class//T nin tipini tanımlıyoruz (where T: class > demek istiyorki sana gelen T ler sadece class olacak, bu classlarda enetitylerimizdir.)
    {
        //arayüzümüzü oluşturuyoruz
        //amacımız şu Insert, CRUD işlemlerini tek bir class da toplayıp tüm enetitylerimiz buraya çağırıp işlemleri gerçekleştiriyoruz.
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);  //birden çok enetity gönderme durumunda ınsert işlemlerini yap

        void Update(T entity); //T enetity göderiyorum bunu güncelle
        void Update(T entity, IEnumerable<string> fields); //liste türünde bir enetity T gönderebilirim, bu enetity i değilde bu entityde değişen alanlarını update et demiş olduk 

        void Update(IEnumerable<T> entities); //T türünden entity gönderebilirim, bunları update edeceksin 

        void Delete(T entity); //T enetity göderiyorum bunu sil
        void Delete(IEnumerable<T> entities); //liste türünde bir enetity T gönderebilirim, bu göndermiş olduğum entityleri de sil

        //bir değer dönecek ama bu değerin türünü bilmiyoruz.T tipinde sorgular alacağız ve bu sorgular sonucunda bu yüzden datatransferobject tipinde veriler dönecek. Tipini bilmediğimiz için IResult yazdık
        //TReslt şu demek ben sana sorgulama anında tipini vereceğim 
        //2. yazan Expression T tipinde değer alıyor, bu gelen T tipi filterlar arasında dolaşıyor ihtiyacımız olanları seçiyor TResult olarak geri dödürüyor 
        TResult Find<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>> selector);

        //sorgu yapılacak IQueryable ile
        //T türünü barından bir filter göndereceğiz, bu filtre ile sen git gerekli sorgulamaları yap, eğer sorgu sonucunda true dönüyorsa kayıt vardır demektir ve bunları geriye gönder.
        //yine TResult türünde olacak bu da sorgu anında belli olacak 
        IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector);
        int Count(Expression<Func<T, bool>> filter=null);
        string YeniKodVer(KartTuru kartTuru, Expression<Func<T, string>> filte, Expression<Func<T, bool>> where = null);
    }
}
