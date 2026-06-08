import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  CreateSubmissionRequest,
  GradeSubmissionRequest,
  Submission
} from '../models/submission.model';

@Injectable({ providedIn: 'root' })
export class SubmissionService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/submissions`;

  submit(data: CreateSubmissionRequest): Observable<Submission> {
    return this.http.post<Submission>(`${this.baseUrl}/submit`, data);
  }

  grade(data: GradeSubmissionRequest): Observable<Submission> {
    return this.http.post<Submission>(`${this.baseUrl}/grade`, data);
  }

  getByAssignment(assignmentId: number): Observable<Submission[]> {
    return this.http.get<Submission[]>(`${this.baseUrl}/assignment/${assignmentId}`);
  }

  getByStudent(studentId: number): Observable<Submission[]> {
    return this.http.get<Submission[]>(`${this.baseUrl}/student/${studentId}`);
  }
}
