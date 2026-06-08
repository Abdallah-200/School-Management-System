import { Component } from '@angular/core';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';

@Component({
  selector: 'app-admin-dashboard',
  imports: [TranslatePipe],
  template: `
    <div class="page-header">
      <h2>{{ 'admin.dashboard.title' | translate }}</h2>
      <p>{{ 'admin.dashboard.subtitle' | translate }}</p>
    </div>
    <div class="stats-grid">
      <div class="stat-card"><div class="stat-icon">👥</div><div class="stat-value">{{ 'common.none' | translate }}</div><div class="stat-label">{{ 'nav.users' | translate }}</div></div>
      <div class="stat-card"><div class="stat-icon">🏛️</div><div class="stat-value">{{ 'common.none' | translate }}</div><div class="stat-label">{{ 'nav.departments' | translate }}</div></div>
      <div class="stat-card"><div class="stat-icon">📚</div><div class="stat-value">{{ 'common.none' | translate }}</div><div class="stat-label">{{ 'nav.courses' | translate }}</div></div>
      <div class="stat-card"><div class="stat-icon">🔔</div><div class="stat-value">{{ 'common.none' | translate }}</div><div class="stat-label">{{ 'nav.notifications' | translate }}</div></div>
    </div>
    <div class="card">
      <h3>{{ 'admin.dashboard.welcome' | translate }}</h3>
      <p>{{ 'admin.dashboard.hint' | translate }}</p>
    </div>
  `,
  styleUrl: './admin-dashboard.component.scss'
})
export class AdminDashboardComponent {}
