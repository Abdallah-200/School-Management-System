import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../core/services/user.service';
import { User, CreateUserRequest } from '../../../core/models/user.model';
import { UserRole } from '../../../core/models/auth.model';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';

@Component({
  selector: 'app-users',
  imports: [ReactiveFormsModule, TranslatePipe],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
  private readonly userService = inject(UserService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly users = signal<User[]>([]);
  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly showForm = signal(false);

  readonly roles: UserRole[] = ['Admin', 'Teacher', 'Student'];

  readonly form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    role: ['Student' as UserRole, Validators.required]
  });

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading.set(true);
    this.userService.getAll().subscribe({
      next: data => {
        this.users.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('admin.users.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  toggleForm(): void {
    this.showForm.update(v => !v);
    this.form.reset({ role: 'Student' });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    const data = this.form.getRawValue() as CreateUserRequest;

    this.userService.create(data).subscribe({
      next: () => {
        this.saving.set(false);
        this.showForm.set(false);
        this.loadUsers();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('admin.users.createFailed'));
      }
    });
  }

  deleteUser(id: number): void {
    if (!confirm(this.translate.get('common.confirmDeleteUser'))) return;

    this.userService.delete(id).subscribe({
      next: () => this.loadUsers(),
      error: () => this.error.set(this.translate.get('admin.users.deleteFailed'))
    });
  }
}
