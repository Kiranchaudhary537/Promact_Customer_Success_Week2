
export interface EscalationMatrixModel {
  id?: string;
  level: string;
  escalationType: string;
  projectId: string;
}

export interface RemediationStepModel {
  id?: string;
  description: string;
  riskProfileId: string;
}

export interface RiskProfileModel {
  id?: string;
  projectId: string;
  riskType: string;
  severity: string;
  impact: string;
}

export interface SprintModel {
  id?: string;
  projectId: string;
  phaseId: string;
  startDate: string;
  endDate: string;
  status: string;
  comments: string;
  goals: string;
  sprintNumber: number;
}

export interface StakeholderModel {
  id?: string;
  projectId: string;
  title: string;
  name: string;
  contactEmail: string;
}

export interface VersionHistoryModel {
  id?: string;
  version: string;
  change: string;
  changeReason: string;
  createdBy: string;
  revisionDate: string;
  approvalDate: string;
  approvedBy: string;
  projectId: string;
}


export interface AuditHistoryModel {
  id?: string;
  dateOfAudit: string;
  reviewedBy: string;
  status: string;
  section: string;
  commentQueries: string;
  actionItem: string;
  projectId: string;
}
