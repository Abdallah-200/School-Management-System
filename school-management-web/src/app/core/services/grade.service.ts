import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Grade } from '../models/grade.model';

@Injectable({ providedIn: 'root' })
export class GradeService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/student/grades`;

  getMyGrades(): Observable<Grade[]> {
    return this.http.get<Grade[]>(this.baseUrl);
  }
}
