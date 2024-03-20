import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { apiEndPoint } from './apiendpoint';
@Injectable({
  providedIn: 'root'
})
export class UserProjectService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = apiEndPoint();
  }

  getAllItem(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}user-project`);
  }

  createItem(projectData: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}user-project`, projectData);
  }

  getItemById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}user-project/${id}/by-id`);
  }

  updateItem(id: number, projectData: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}user-project/${id}`, projectData);
  }

  deleteItem(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}user-project/${id}/async-by-id`);
  }
}
