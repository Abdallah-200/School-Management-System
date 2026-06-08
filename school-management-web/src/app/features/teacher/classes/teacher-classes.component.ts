import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClassService } from '../../../core/services/class.service';
import { CourseService } from '../../../core/services/course.service';
import { AuthService } from '../../../core/auth/auth.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { SchoolClass } from '../../../core/models/class.model';
import { Course } from '../../../core/models/course.model';

@Component({
  selector: 'app-teacher-classes',
  imports: [ReactiveFormsModule, DatePipe, TranslatePipe],
  templateUrl: './teacher-classes.component.html',
  styleUrl: './teacher-classes.component.scss'
})
export class TeacherClassesComponent implements OnInit {
  private readonly classService = inject(ClassService);
  private readonly courseService = inject(CourseService);
  private readonly auth = inject(AuthService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly classes = signal<SchoolClass[]>([]);
  readonly courses = signal<Course[]>([]);
  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly showForm = signal(false);

  readonly form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    courseId: [0, Validators.required],
    semester: [this.translate.get('teacher.classes.defaultSemester')],
    startDate: ['', Validators.required],
    endDate: ['', Validators.required]
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.classService.getTeacherClasses().subscribe({
      next: data => {
        this.classes.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('teacher.classes.loadFailed'));
        this.loading.set(false);
      }
    });

    this.courseService.getAll().subscribe({
      next: data => this.courses.set(data)
    });
  }

  toggleForm(): void {
    this.showForm.update(v => !v);
    this.form.reset({ semester: this.translate.get('teacher.classes.defaultSemester') });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const teacherId = this.auth.userId();
    if (!teacherId) return;

    this.saving.set(true);
    const raw = this.form.getRawValue();

    this.classService.create({
      ...raw,
      teacherId
    }).subscribe({
      next: () => {
        this.saving.set(false);
        this.showForm.set(false);
        this.loadData();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('teacher.classes.createFailed'));
      }
    });
  }
}
