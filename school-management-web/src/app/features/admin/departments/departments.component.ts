import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepartmentService } from '../../../core/services/department.service';
import { UserService } from '../../../core/services/user.service';
import { Department } from '../../../core/models/department.model';
import { User } from '../../../core/models/user.model';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';

@Component({
  selector: 'app-departments',
  imports: [ReactiveFormsModule, TranslatePipe],
  templateUrl: './departments.component.html',
  styleUrl: './departments.component.scss'
})
export class DepartmentsComponent implements OnInit {
  private readonly departmentService = inject(DepartmentService);
  private readonly userService = inject(UserService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly departments = signal<Department[]>([]);
  readonly teachers = signal<User[]>([]);
  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly showForm = signal(false);

  readonly form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    description: [''],
    headOfDepartmentId: [0, Validators.required]
  });

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.departmentService.getAll().subscribe({
      next: data => {
        this.departments.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('admin.departments.loadFailed'));
        this.loading.set(false);
      }
    });

    this.userService.getAll().subscribe({
      next: users => this.teachers.set(users.filter(u => u.role === 'Teacher' || u.role === 'Admin'))
    });
  }

  toggleForm(): void {
    this.showForm.update(v => !v);
    this.form.reset();
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.departmentService.create(this.form.getRawValue()).subscribe({
      next: () => {
        this.saving.set(false);
        this.showForm.set(false);
        this.loadData();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('admin.departments.createFailed'));
      }
    });
  }

  deleteDepartment(id: number): void {
    if (!confirm(this.translate.get('common.confirmDeleteDepartment'))) return;

    this.departmentService.delete(id).subscribe({
      next: () => this.loadData(),
      error: () => this.error.set(this.translate.get('admin.departments.deleteFailed'))
    });
  }
}
