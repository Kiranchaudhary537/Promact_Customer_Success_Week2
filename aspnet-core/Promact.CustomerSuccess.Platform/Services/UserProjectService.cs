using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class UserProjectService : ApplicationService,
        IUserProjectService
    {
        private readonly IRepository<UserProject, Guid> _repository;
        public UserProjectService(IRepository<UserProject, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<UserProject> CreateAsync(CreateUserProjectDto input)
        {
            var entity = ObjectMapper.Map<CreateUserProjectDto, UserProject>(input);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        public async Task DeleteAsyncById(Guid id)
        {
            await _repository.DeleteAsync(id, autoSave: true);
        }

        public async Task<List<UserProject>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<UserProject> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task UpdateAsync(Guid id, UpdateUserProjectDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
        }
    }
}
