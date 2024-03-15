using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Service
{
    public class PhaseService : ApplicationService,
        IPhaseService
    {
        IRepository<Phase, Guid> _repository;
        public PhaseService(IRepository<Phase, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Phase> CreateAsync(CreatePhaseDto input)
        {
            var entity = ObjectMapper.Map<CreatePhaseDto, Phase>(input);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public  async Task<List<Phase>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public Task<Phase> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdatePhaseDto input)
        {
            throw new NotImplementedException();
        }
    }
}
