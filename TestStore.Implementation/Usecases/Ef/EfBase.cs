using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Implementation.DataAccess;

namespace TestStore.Implementation.Usecases.Ef
{
    public class EfBase
    {
        public TestStoreDbContext Context { get; set; }
        public EfBase(TestStoreDbContext context) => this.Context = context;
    }
}
