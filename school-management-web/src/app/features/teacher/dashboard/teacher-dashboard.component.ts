import { Component } from '@angular/core';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';

@Component({
  selector: 'app-teacher-dashboard',
  imports: [TranslatePipe],
  template: `
    <div class="page-header">
      <h2>{{ 'teacher.dashboard.title' | translate }}</h2>
      <p>{{ 'teacher.dashboard.subtitle' | translate }}</p>
    </div>
    <div class="card">
      <h3>{{ 'teacher.dashboard.welcome' | translate }}</h3>
      <p>{{ 'teacher.dashboard.hint' | translate }}</p>
    </div>
  `,
  styleUrl: './teacher-dashboard.component.scss'
})
export class TeacherDashboardComponent {}
