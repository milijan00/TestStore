using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Usecases
{
    public interface IQuery<TRequest, TResponse> : IUsecase
    {
        TResponse Execute(TRequest data);
    }
    public interface IQuery<TResponse> : IUsecase
    {
        TResponse Execute();
    }
}
