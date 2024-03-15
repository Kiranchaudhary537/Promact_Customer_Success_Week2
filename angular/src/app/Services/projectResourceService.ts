import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiEndPoint } from './apiendpoint';
@Injectable({
  providedIn: 'root',
})
export class ProjectResourceService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = apiEndPoint();
  }

  getAllProjects(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}project-resources`);
  }

  createProject(projectData: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}project-resources`, projectData);
  }

  getProjectById(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}project-resources/{id}/by-id`);
  }

  updateProject(id: string, projectData: any): Observable<any> {
    console.log(id);
    return this.http.put<any>(`${this.baseUrl}project-resources/${id}`, projectData);
  }

  deleteProject(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}project-resources/${id}/`);
  }
}
