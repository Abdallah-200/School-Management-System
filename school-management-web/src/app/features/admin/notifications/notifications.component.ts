import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NotificationService } from '../../../core/services/notification.service';
import { UserRole } from '../../../core/models/auth.model';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';

@Component({
  selector: 'app-notifications',
  imports: [ReactiveFormsModule, TranslatePipe],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent {
  private readonly notificationService = inject(NotificationService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly saving = signal(false);
  readonly error = signal('');
  readonly success = signal('');
  readonly roles: UserRole[] = ['Admin', 'Teacher', 'Student'];

  readonly form = this.fb.nonNullable.group({
    title: ['', Validators.required],
    message: ['', Validators.required],
    recipientRole: ['Student' as UserRole, Validators.required],
    recipientId: [null as number | null]
  });

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.error.set('');
    this.success.set('');

    const raw = this.form.getRawValue();
    const data = {
      title: raw.title,
      message: raw.message,
      recipientRole: raw.recipientRole,
      recipientId: raw.recipientId || undefined
    };

    this.notificationService.create(data).subscribe({
      next: () => {
        this.saving.set(false);
        this.success.set(this.translate.get('admin.notifications.success'));
        this.form.reset({ recipientRole: 'Student' });
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('admin.notifications.failed'));
      }
    });
  }
}
