using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Usecases
{
    public interface IUsecase
    {
        public int Id { get;  }
        public string Name { get; }
        public bool AdminOnly { get; }
    }
}
