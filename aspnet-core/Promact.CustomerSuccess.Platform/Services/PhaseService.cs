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

        public async Task<string> DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id, autoSave: true);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed" + ex.ToString();
            }

        }

        public async Task<List<Phase>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<Phase> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task<Phase> UpdateAsync(Guid id, UpdatePhaseDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
            return entity;
        }
    }
}
