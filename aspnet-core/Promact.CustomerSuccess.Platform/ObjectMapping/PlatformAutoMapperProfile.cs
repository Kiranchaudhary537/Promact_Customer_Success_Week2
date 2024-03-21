
using AutoMapper;
using Promact.CustomerSuccess.Platform.Entities;
using Promact.CustomerSuccess.Platform.Services.Dtos;

namespace Promact.CustomerSuccess.Platform.ObjectMapping;

public class PlatformAutoMapperProfile : Profile
{
    public PlatformAutoMapperProfile()
    {
        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();
        CreateMap<Project, ProjectDto>().ReverseMap();

        CreateMap<ClientFeedback, ClientFeedbackDto>();
        CreateMap<CreateClientFeedbackDto, ClientFeedback>();
        CreateMap<UpdateClientFeedbackDto, ClientFeedback>();

        CreateMap<CreateProjectResourceDto, ProjectResource>();
        CreateMap<UpdateProjectResourceDto, ProjectResource>();
        CreateMap<ProjectResource, ProjectResourceDto>().ReverseMap();

        CreateMap<CreateProjectUpdateDto, ProjectUpdate>();
        CreateMap<UpdateProjectUpdateDto, ProjectUpdate>();
        CreateMap<ProjectUpdate, ProjectUpdateDto>().ReverseMap();

        CreateMap<CreateMeetingMinuteDto, MeetingMinute>();
        CreateMap<UpdateMeetingMinuteDto, MeetingMinute>();
        CreateMap<MeetingMinute, MeetingMinuteDto>().ReverseMap();

        CreateMap<CreateApprovedTeamDto, ApprovedTeam>();
        CreateMap<UpdateApprovedTeamDto, ApprovedTeam>();
        CreateMap<ApprovedTeam, ApprovedTeamDto>().ReverseMap();

        CreateMap<CreatePhaseDto, Phase>();
        CreateMap<UpdatePhaseDto, Phase>();
        CreateMap<Phase, PhaseDto>().ReverseMap();

        CreateMap<CreateAuditHistoryDto, AuditHistory>();
        CreateMap<UpdateAuditHistoryDto, AuditHistory>();
        CreateMap<AuditHistory, AuditHistoryDto>().ReverseMap();

        CreateMap<CreateProjectBudgetDto, ProjectBudget>();
        CreateMap<UpdateProjectBudgetDto, ProjectBudget>();
        CreateMap<ProjectBudget, ProjectBudgetDto>().ReverseMap();

        CreateMap<CreateEscalationMatrixDto, EscalationMatrix>();
        CreateMap<UpdateEscalationMatrixDto, EscalationMatrix>();
        CreateMap<EscalationMatrix, EscalationMatrixDto>().ReverseMap();

        CreateMap<CreateRiskProfileDto, RiskProfile>();
        CreateMap<UpdateRiskProfileDto, RiskProfile>();
        CreateMap<RiskProfile, RiskProfileDto>().ReverseMap();

        CreateMap<CreateSprintDto, Sprint>();
        CreateMap<UpdateSprintDto, Sprint>();
        CreateMap<Sprint, SprintDto>().ReverseMap();

        CreateMap<CreateStakeholderDto, Stakeholder>();
        CreateMap<UpdateStakeholderDto, Stakeholder>();
        CreateMap<Stakeholder, StakeholderDto>().ReverseMap();

        CreateMap<CreateRiskProfileDto, RiskProfile>();
        CreateMap<UpdateRiskProfileDto, RiskProfile>();
        CreateMap<RiskProfile, RiskProfileDto>().ReverseMap();

        CreateMap<CreateVersionHistoryDto, VersionHistory>();
        CreateMap<UpdateVersionHistoryDto, VersionHistory>();
        CreateMap<VersionHistory, VersionHistoryDto>().ReverseMap();

        CreateMap<CreateRemediationStepDto, RemediationStep>();
        CreateMap<UpdateRemediationStepDto, RemediationStep>();
        CreateMap<RemediationStep, RemediationStepDto>().ReverseMap();

        CreateMap<CreateOverviewDto, Overview>();
        CreateMap<UpdateOverviewDto, Overview>();
        CreateMap<Overview, OverviewDto>().ReverseMap();

        CreateMap<CreateStakeAndScopeDto, StakeAndScope>();
        CreateMap<UpdateStakeAndScopeDto, StakeAndScope>();
        CreateMap<StakeAndScope, StakeAndScopeDto>().ReverseMap();

        CreateMap<CreateUsersDto, Users>();
        CreateMap<UpdateUsersDto, Users>();
        CreateMap<Users, UsersDto>().ReverseMap();

        CreateMap<CreateUserProjectDto, UserProject>();
        CreateMap<UpdateUserProjectDto, UserProject>();
        CreateMap<UserProject, UserProjectDto>().ReverseMap();
    }
}
