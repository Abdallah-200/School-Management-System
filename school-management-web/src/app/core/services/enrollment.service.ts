import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EnrollStudentRequest, StudentClassEnrollment } from '../models/class.model';

@Injectable({ providedIn: 'root' })
export class EnrollmentService {
  private readonly http = inject(HttpClient);
  private readonly teacherUrl = `${environment.apiUrl}/teacher/student-classes`;
  private readonly adminUrl = `${environment.apiUrl}/admin/student-classes`;

  enroll(data: EnrollStudentRequest): Observable<StudentClassEnrollment> {
    return this.http.post<StudentClassEnrollment>(this.teacherUrl, data);
  }

  getByStudent(studentId: number): Observable<StudentClassEnrollment[]> {
    return this.http.get<StudentClassEnrollment[]>(`${this.teacherUrl}/student/${studentId}`);
  }

  getByClass(classId: number, scope: 'teacher' | 'admin' = 'teacher'): Observable<StudentClassEnrollment[]> {
    const base = scope === 'admin' ? this.adminUrl : this.teacherUrl;
    return this.http.get<StudentClassEnrollment[]>(`${base}/class/${classId}`);
  }
}
