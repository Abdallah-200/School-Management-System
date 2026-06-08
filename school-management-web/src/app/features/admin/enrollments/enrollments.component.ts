import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { EnrollmentService } from '../../../core/services/enrollment.service';
import { UserService } from '../../../core/services/user.service';
import { ClassService } from '../../../core/services/class.service';
import { StudentClassEnrollment } from '../../../core/models/class.model';
import { User } from '../../../core/models/user.model';
import { SchoolClass } from '../../../core/models/class.model';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';

@Component({
  selector: 'app-enrollments',
  imports: [ReactiveFormsModule, DatePipe, TranslatePipe],
  templateUrl: './enrollments.component.html',
  styleUrl: './enrollments.component.scss'
})
export class EnrollmentsComponent implements OnInit {
  private readonly enrollmentService = inject(EnrollmentService);
  private readonly userService = inject(UserService);
  private readonly classService = inject(ClassService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly enrollments = signal<StudentClassEnrollment[]>([]);
  readonly students = signal<User[]>([]);
  readonly classes = signal<SchoolClass[]>([]);
  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly success = signal('');

  readonly form = this.fb.nonNullable.group({
    studentId: [0, Validators.required],
    classId: [0, Validators.required]
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.userService.getStudentsForTeacher().subscribe({
      next: users => this.students.set(users)
    });

    this.classService.getTeacherClasses().subscribe({
      next: data => this.classes.set(data),
      error: () => this.error.set(this.translate.get('admin.enrollments.loadFailed'))
    });
  }

  searchByClass(): void {
    const classId = this.form.controls.classId.value;
    if (!classId) return;

    this.loading.set(true);
    this.enrollmentService.getByClass(classId).subscribe({
      next: data => {
        this.enrollments.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('admin.enrollments.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.error.set('');
    this.success.set('');

    this.enrollmentService.enroll(this.form.getRawValue()).subscribe({
      next: () => {
        this.saving.set(false);
        this.success.set(this.translate.get('admin.enrollments.success'));
        this.searchByClass();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('admin.enrollments.failed'));
      }
    });
  }
}
