import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AttendanceService } from '../../../core/services/attendance.service';
import { ClassService } from '../../../core/services/class.service';
import { EnrollmentService } from '../../../core/services/enrollment.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Attendance, AttendanceStatus } from '../../../core/models/attendance.model';
import { SchoolClass } from '../../../core/models/class.model';

@Component({
  selector: 'app-teacher-attendance',
  imports: [ReactiveFormsModule, DatePipe, TranslatePipe],
  templateUrl: './teacher-attendance.component.html',
  styleUrl: './teacher-attendance.component.scss'
})
export class TeacherAttendanceComponent implements OnInit {
  private readonly attendanceService = inject(AttendanceService);
  private readonly classService = inject(ClassService);
  private readonly enrollmentService = inject(EnrollmentService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly attendance = signal<Attendance[]>([]);
  readonly classes = signal<SchoolClass[]>([]);
  readonly students = signal<{ id: number; name: string }[]>([]);
  readonly loading = signal(false);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly success = signal('');
  readonly selectedClassId = signal(0);

  readonly statuses: AttendanceStatus[] = ['Present', 'Absent', 'Late'];

  readonly form = this.fb.nonNullable.group({
    classId: [0, Validators.required],
    studentId: [0, Validators.required],
    date: [new Date().toISOString().split('T')[0], Validators.required],
    status: ['Present' as AttendanceStatus, Validators.required]
  });

  ngOnInit(): void {
    this.classService.getAdminClasses().subscribe({
      next: data => this.classes.set(data),
      error: () => this.error.set(this.translate.get('teacher.attendance.loadFailed'))
    });
  }

  onClassChange(classId: number): void {
    this.selectedClassId.set(classId);
    this.form.patchValue({ classId });

    if (classId) {
      this.enrollmentService.getByClass(classId, 'admin').subscribe({
        next: data => {
          this.students.set(
            data.map(e => ({
              id: e.studentId,
              name: e.student?.fullName || this.translate.get('teacher.attendance.studentFallback', { id: e.studentId })
            }))
          );
        }
      });
      this.loadAttendance();
    }
  }

  loadAttendance(): void {
    const classId = this.selectedClassId();
    if (!classId) return;

    this.loading.set(true);
    this.attendanceService.getByClass(classId).subscribe({
      next: data => {
        this.attendance.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('teacher.attendance.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.error.set('');
    this.success.set('');

    this.attendanceService.mark(this.form.getRawValue()).subscribe({
      next: () => {
        this.saving.set(false);
        this.success.set(this.translate.get('teacher.attendance.success'));
        this.loadAttendance();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('teacher.attendance.failed'));
      }
    });
  }

  statusLabel(status: AttendanceStatus): string {
    return this.translate.attendanceKey(status);
  }
}
