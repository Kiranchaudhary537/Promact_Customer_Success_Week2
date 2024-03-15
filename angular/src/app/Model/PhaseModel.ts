import { ApprovedTeam } from "./ApprovedTeamModel";

export interface Phase {
  id?: string;
  title: string;
  startDate: string;
  completionDate?: string;
  approvalDate?: string;
  status: PhaseStatus;
  comments: string;
  projectId: string;
  approvedTeams?: ApprovedTeam[];
}

export enum PhaseStatus {
  Delayed = 'Delayed',
  OnTime = 'OnTime',
  SignRevised = 'SignRevised'
}
