import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth/auth.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink, TranslatePipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  private readonly fb = inject(FormBuilder);
  private readonly auth = inject(AuthService);
  private readonly translate = inject(TranslateService);

  readonly loading = signal(false);
  readonly error = signal('');

  readonly form = this.fb.nonNullable.group({
    fullName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    // confirmPassword: ['', Validators.required]
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { ...data } = this.form.getRawValue();
   

    this.loading.set(true);
    this.error.set('');

    this.auth.register(data).subscribe({
      next: () => {
        this.loading.set(false);
        this.auth.redirectByRole();
      },
      error: err => {
        this.loading.set(false);
        const msg =
          err.error?.title ||
          err.error?.message ||
          (err.status === 0 ? this.translate.get('auth.networkError') : null) ||
          this.translate.get('auth.registerFailed');
        this.error.set(msg);
      }
    });
  }
}
