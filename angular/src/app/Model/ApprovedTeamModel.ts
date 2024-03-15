export interface ApprovedTeam {
  id?: string;
  numberOfResources: number;
  role: string;
  availabilityPercentage: number;
  duration: number;
  phaseId: string;
  projectId: string;
}
