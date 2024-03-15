// project.model.ts

export interface Project {
  id?:string,
  name: string;
  projectManager:string,
  member:number,
  status:string,
  creationTime:string
}
