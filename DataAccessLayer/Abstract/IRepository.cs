using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        void Insert(T p);
        void Update(T p);
        void Delete(T p);

        //Şartlı arama için bir ifade tanımladık T'den yine referans alarak bool türünde olduğunu ve adının filter olduğunu da belirtik.
        List<T> List(Expression<Func<T, bool>> filter);
    }
}
