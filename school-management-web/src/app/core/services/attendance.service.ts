import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Attendance, CreateAttendanceRequest } from '../models/attendance.model';

@Injectable({ providedIn: 'root' })
export class AttendanceService {
  private readonly http = inject(HttpClient);
  private readonly adminUrl = `${environment.apiUrl}/admin/attendance`;
  private readonly teacherUrl = `${environment.apiUrl}/teacher/attendance`;
  private readonly studentUrl = `${environment.apiUrl}/student/attendance`;

  mark(data: CreateAttendanceRequest): Observable<Attendance> {
    return this.http.post<Attendance>(this.adminUrl, data);
  }

  getByClass(classId: number): Observable<Attendance[]> {
    return this.http.get<Attendance[]>(`${this.adminUrl}/class/${classId}`);
  }

  getByStudent(studentId: number): Observable<Attendance[]> {
    return this.http.get<Attendance[]>(`${this.adminUrl}/student/${studentId}`);
  }

  getStudentAttendance(): Observable<Attendance[]> {
    return this.http.get<Attendance[]>(this.studentUrl);
  }
}
