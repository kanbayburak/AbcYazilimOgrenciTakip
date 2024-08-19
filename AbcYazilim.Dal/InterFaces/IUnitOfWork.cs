using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.Dal.InterFaces
{

    //okul entity si üzerinden çeşitli update, delete, insert işlemleri yapıldı ve sonrasında Repository e gönderiyoruz ancak Repository e göndermemiz demek bunu database e gönderiyoruz anlamına geliyor. çünkü herhangi bir save işlemi yapıp database e göndermiyor. 
    //bunları tek tek repositorye gönderip save işlemi yapabiliriz ama biz böyle yapmayacağız toplu olarak işlmeleri yapıp save işlemini Unitofwork yapacak. 
    public interface IUnitOfWork<T>: IDisposable where T: class // demek istiyorki sana gelen T ler sadece class olacak, bu classlarda enetitylerimizdir.
    {
        //repostryden gelen talepleri database e gönderme işlemi
        IRepository<T> Rep { get; } // repostory dekie tüm functionlara ulaşma şekli
        bool Save(); //kaydedildiyse true değilse false

    }
}


