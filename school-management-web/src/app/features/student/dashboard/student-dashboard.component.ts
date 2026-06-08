import { Component } from '@angular/core';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';

@Component({
  selector: 'app-student-dashboard',
  imports: [TranslatePipe],
  template: `
    <div class="page-header">
      <h2>{{ 'student.dashboard.title' | translate }}</h2>
      <p>{{ 'student.dashboard.subtitle' | translate }}</p>
    </div>
    <div class="card">
      <h3>{{ 'student.dashboard.welcome' | translate }}</h3>
      <p>{{ 'student.dashboard.hint' | translate }}</p>
    </div>
  `,
  styleUrl: './student-dashboard.component.scss'
})
export class StudentDashboardComponent {}
