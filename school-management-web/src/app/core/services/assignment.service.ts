import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Assignment, CreateAssignmentRequest } from '../models/assignment.model';

@Injectable({ providedIn: 'root' })
export class AssignmentService {
  private readonly http = inject(HttpClient);
  private readonly teacherUrl = `${environment.apiUrl}/teacher/assignments`;
  private readonly studentUrl = `${environment.apiUrl}/student/assignments`;

  getByClass(classId: number): Observable<Assignment[]> {
    return this.http.get<Assignment[]>(`${this.teacherUrl}/class/${classId}`);
  }

  create(data: CreateAssignmentRequest): Observable<Assignment> {
    return this.http.post<Assignment>(this.teacherUrl, data);
  }

  getStudentAssignments(): Observable<Assignment[]> {
    return this.http.get<Assignment[]>(this.studentUrl);
  }

  submitFile(assignmentId: number, file: File): Observable<unknown> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(`${this.studentUrl}/${assignmentId}/submit`, formData);
  }
}
