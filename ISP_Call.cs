using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_DataAccess.Repository.IRepository
{
    public interface ISP_Call:IDisposable
    {
        //Create,Update,Delete
        void Execute(string procedureName, DynamicParameters param = null);

        //Find
        T Single<T>(string procedureName, DynamicParameters param = null);
        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        //Display
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        //Multiple query result
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);
    }
}
