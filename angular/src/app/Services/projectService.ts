import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiEndPoint } from './apiendpoint';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = apiEndPoint();
  }

  getAllProjects(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}project/project`);
  }

  createProject(projectData: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}project/project`, projectData);
  }

  getProjectById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}project/${id}/project-by-id`);
  }

  updateProject(id: number, projectData: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}project/${id}/project`, projectData);
  }

  deleteProject(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}project/${id}/project`);
  }
}
