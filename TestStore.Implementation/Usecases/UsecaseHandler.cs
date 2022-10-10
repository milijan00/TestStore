using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Application.Usecases;

namespace TestStore.Implementation.Usecases
{
    public class UsecaseHandler
    {
        
        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                command.Execute(data);
            }catch(Exception ex)
            {
                throw;
            }
        }

        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                return query.Execute(data);
            }catch(Exception ex)
            {
                throw;
            }
        }
        public TResponse HandleQuery<TResponse>(IQuery<TResponse> query)
        {
            try
            {
                return query.Execute();
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
