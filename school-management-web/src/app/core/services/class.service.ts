import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateClassRequest, SchoolClass, StudentClassEnrollment } from '../models/class.model';

@Injectable({ providedIn: 'root' })
export class ClassService {
  private readonly http = inject(HttpClient);
  private readonly teacherUrl = `${environment.apiUrl}/teacher/classes`;
  private readonly studentUrl = `${environment.apiUrl}/student/classes`;
  private readonly adminUrl = `${environment.apiUrl}/admin/classes`;
  getTeacherClasses(): Observable<SchoolClass[]> {
    return this.http.get<SchoolClass[]>(this.teacherUrl);
  }
   getAdminClasses(): Observable<SchoolClass[]> {
    return this.http.get<SchoolClass[]>(this.adminUrl);
  }
  create(data: CreateClassRequest): Observable<SchoolClass> {
    return this.http.post<SchoolClass>(this.teacherUrl, data);
  }

  getStudentClasses(): Observable<StudentClassEnrollment[]> {
    return this.http.get<StudentClassEnrollment[]>(this.studentUrl);
  }
}
