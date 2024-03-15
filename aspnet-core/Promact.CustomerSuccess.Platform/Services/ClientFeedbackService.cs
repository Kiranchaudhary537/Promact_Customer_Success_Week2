
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Promact.CustomerSuccess.Platform
{
    [Authorize]
    public class ClientFeedbackService : ApplicationService, IClientFeedbackService
    {
        private readonly IRepository<ClientFeedback, Guid> _repository;
        private readonly IObjectMapper _objectMapper;

        public ClientFeedbackService(
            IRepository<ClientFeedback, Guid> repository,
            IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
        }


        [Authorize("Client Feedback Read")]
        public async Task<List<ClientFeedback>> GetAllAsync()
        {
            var entities = await _repository.GetListAsync();
            return entities;
        }

        [Authorize("Client Feedback Read")]
        public async Task<ClientFeedback> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return entity;
        }
        
        [Authorize("Client Feedback Create")]
        public async Task<ClientFeedback> CreateAsync(CreateClientFeedbackDto input)
        {
            var entity = _objectMapper.Map<CreateClientFeedbackDto, ClientFeedback>(input);
            await _repository.InsertAsync(entity, autoSave: true);
            return entity;
  
        }

        [Authorize("Client Feedback Update")]
        public async Task<ClientFeedback> UpdateAsync(Guid id, UpdateClientFeedbackDto input)
        {
            var entity = await _repository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            await _repository.UpdateAsync(entity, autoSave: true);
            return entity;

        }

        [Authorize("Client Feedback Delete")]
        public async Task<String> DeleteAsync(Guid id)
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
    }
}
