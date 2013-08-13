using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFoundation.Common.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createria"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        T FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createria"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null); 
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
    }
}
