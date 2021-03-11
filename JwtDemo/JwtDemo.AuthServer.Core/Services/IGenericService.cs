using JwtDemo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.Core.Services
{
    public interface IGenericService<TEntity, TDto> where TEntity : class, new() where TDto : class, new()
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDto>> AddAsync(TDto entity);
        Task<Response<NoDataDto>> RemoveAsync(int id);
        Task<Response<NoDataDto>> UpdateAsync(TDto entity, int id);
    }
}
