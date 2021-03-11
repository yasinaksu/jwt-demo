using JwtDemo.AuthServer.Core.Repositories;
using JwtDemo.AuthServer.Core.Services;
using JwtDemo.AuthServer.Core.UnitOfWork;
using JwtDemo.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class, new() where TDto : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;
        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var addedEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.AddAsync(addedEntity);
            await _unitOfWork.CommitAsync();
            var addedDto = ObjectMapper.Mapper.Map<TDto>(addedEntity);
            return Response<TDto>.Success(addedDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = ObjectMapper.Mapper.Map<List<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<TDto>.Fail("Entity not found", 404, true);
            }
            var dto = ObjectMapper.Mapper.Map<TDto>(entity);
            return Response<TDto>.Success(dto, 200);
        }

        public async Task<Response<NoDataDto>> RemoveAsync(int id)
        {
            var deletedEntity = await _repository.GetByIdAsync(id);
            if (deletedEntity == null)
            {
                return Response<NoDataDto>.Fail("Entity not found", 404, true);
            }
            _repository.Delete(deletedEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> UpdateAsync(TDto entity, int id)
        {
            var modifiedEntity = await _repository.GetByIdAsync(id);
            if (modifiedEntity == null)
            {
                return Response<NoDataDto>.Fail("Entity not found", 404, true);
            }
            modifiedEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _repository.Update(modifiedEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _repository.Where(predicate).ToListAsync();
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entities);
            return Response<IEnumerable<TDto>>.Success(dtos, 200);
        }
    }
}
