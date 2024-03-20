using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Promact.CustomerSuccess.Platform.Services.Service;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class UserService : ApplicationService,
        IUserService
    {
        private readonly IRepository<Users, Guid> _repository;
        public UserService(IRepository<Users, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Users> CreateAsync(CreateUsersDto input)
        {
           
            var entity = ObjectMapper.Map<CreateUsersDto, Users>(input);
            
            //entity.ProjectId.Add(input.ProjectId);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        public async Task DeleteAsyncById(Guid id)
        {
            await _repository.DeleteAsync(id, autoSave: true);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<Users> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task UpdateAsync(Guid id, UpdateUsersDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
        }
    }
}
