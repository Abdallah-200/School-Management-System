import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { AttendanceService } from '../../../core/services/attendance.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Attendance, AttendanceStatus } from '../../../core/models/attendance.model';

@Component({
  selector: 'app-student-attendance',
  imports: [DatePipe, TranslatePipe],
  templateUrl: './student-attendance.component.html',
  styleUrl: './student-attendance.component.scss'
})
export class StudentAttendanceComponent implements OnInit {
  private readonly attendanceService = inject(AttendanceService);
  private readonly translate = inject(TranslateService);

  readonly attendance = signal<Attendance[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  ngOnInit(): void {
    this.attendanceService.getStudentAttendance().subscribe({
      next: data => {
        this.attendance.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('student.attendance.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  statusLabel(status: AttendanceStatus): string {
    return this.translate.attendanceKey(status);
  }

  statusClass(status: AttendanceStatus): string {
    const classes: Record<AttendanceStatus, string> = {
      Present: 'badge-present',
      Absent: 'badge-absent',
      Late: 'badge-late'
    };
    return classes[status];
  }
}
