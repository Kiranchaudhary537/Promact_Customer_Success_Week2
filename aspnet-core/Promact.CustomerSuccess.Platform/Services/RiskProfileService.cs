using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Service;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Promact.CustomerSuccess.Platform.Services
{
    public class RiskProfileService : ApplicationService,
        IRiskProfileService
    {
        IRepository<RiskProfile, Guid> _repository;
        public RiskProfileService(IRepository<RiskProfile, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<RiskProfile> CreateAsync(CreateRiskProfileDto input)
        {
            try
            {
                var entity = ObjectMapper.Map<CreateRiskProfileDto, RiskProfile>(input);
                await _repository.InsertAsync(entity, autoSave: true);
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
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

        public async Task<List<RiskProfile>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        public async Task<RiskProfile> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }

        public async Task<RiskProfile> UpdateAsync(Guid id, UpdateRiskProfileDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
            return entity;
        }
    }
}
