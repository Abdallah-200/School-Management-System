import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseService } from '../../../core/services/course.service';
import { DepartmentService } from '../../../core/services/department.service';
import { Course } from '../../../core/models/course.model';
import { Department } from '../../../core/models/department.model';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';

@Component({
  selector: 'app-courses',
  imports: [ReactiveFormsModule, TranslatePipe],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.scss'
})
export class CoursesComponent implements OnInit {
  private readonly courseService = inject(CourseService);
  private readonly departmentService = inject(DepartmentService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly courses = signal<Course[]>([]);
  readonly departments = signal<Department[]>([]);
  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly showForm = signal(false);

  readonly form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    code: ['', Validators.required],
    description: [''],
    departmentId: [0, Validators.required],
    credits: [3, [Validators.required, Validators.min(1)]]
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.courseService.getAll().subscribe({
      next: data => {
        this.courses.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('admin.courses.loadFailed'));
        this.loading.set(false);
      }
    });

    this.departmentService.getAll().subscribe({
      next: data => this.departments.set(data)
    });
  }

  toggleForm(): void {
    this.showForm.update(v => !v);
    this.form.reset({ credits: 3 });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.courseService.create(this.form.getRawValue()).subscribe({
      next: () => {
        this.saving.set(false);
        this.showForm.set(false);
        this.loadData();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('admin.courses.createFailed'));
      }
    });
  }

  deleteCourse(id: number): void {
    if (!confirm(this.translate.get('common.confirmDeleteCourse'))) return;

    this.courseService.delete(id).subscribe({
      next: () => this.loadData(),
      error: () => this.error.set(this.translate.get('admin.courses.deleteFailed'))
    });
  }

  getDepartmentName(id: number): string {
    return this.departments().find(d => d.id === id)?.name || this.translate.get('common.none');
  }
}
