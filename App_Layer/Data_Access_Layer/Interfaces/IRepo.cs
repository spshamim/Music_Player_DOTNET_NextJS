using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IRepo<CLASS, ID, RET>
    {
        RET Create(CLASS obj);
        List<CLASS> Get();
        CLASS Get(ID id);
        RET Update(CLASS obj);
        bool Delete(ID id);
        List<CLASS> GetByString(string name);
    }
}
