using Data_Access_Layer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class Repo
    {
        protected DContext db;
        internal Repo()
        {
            db = new DContext();
        }
    }
}
