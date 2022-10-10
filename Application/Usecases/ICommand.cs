using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Usecases
{
    public interface ICommand<TRequest> : IUsecase
    {
        void Execute(TRequest data);
    }
}
