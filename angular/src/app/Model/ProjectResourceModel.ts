// project-allocation.model.ts
import { Project } from './ProjectModel';

export interface projectResourceModel{
  id?: string;
  name:string,
  projectId: string;
  project?: Project;
  allocationPercentage: number;
  start: string;
  end: string;
  role: string;
  comment?:string,
}
