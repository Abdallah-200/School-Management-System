import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';
import { UserRole } from '../../core/models/auth.model';
import { TranslatePipe } from '../../core/i18n/translate.pipe';
import { TranslateService } from '../../core/i18n/translate.service';
import { LanguageSwitcherComponent } from '../../shared/components/language-switcher/language-switcher.component';

interface NavItem {
  labelKey: string;
  path: string;
  icon: string;
}

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterLink, RouterLinkActive, TranslatePipe, LanguageSwitcherComponent],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent {
  private readonly auth = inject(AuthService);
  private readonly translate = inject(TranslateService);

  readonly role = this.auth.role;

  readonly navItems: Record<UserRole, NavItem[]> = {
    Admin: [
      { labelKey: 'nav.dashboard', path: '/admin', icon: '📊' },
      { labelKey: 'nav.users', path: '/admin/users', icon: '👥' },
      { labelKey: 'nav.departments', path: '/admin/departments', icon: '🏛️' },
      { labelKey: 'nav.courses', path: '/admin/courses', icon: '📚' },
      { labelKey: 'nav.attendance', path: '/admin/attendance', icon: '✅' },
      { labelKey: 'nav.notifications', path: '/admin/notifications', icon: '🔔' }
    ],
    Teacher: [
      { labelKey: 'nav.dashboard', path: '/teacher', icon: '📊' },
      { labelKey: 'nav.classes', path: '/teacher/classes', icon: '🏫' },
      { labelKey: 'nav.enrollments', path: '/teacher/enrollments', icon: '📝' },
      { labelKey: 'nav.assignments', path: '/teacher/assignments', icon: '📋' },
      { labelKey: 'nav.submissions', path: '/teacher/submissions', icon: '📤' }
    ],
    Student: [
      { labelKey: 'nav.dashboard', path: '/student', icon: '📊' },
      { labelKey: 'nav.myClasses', path: '/student/classes', icon: '🏫' },
      { labelKey: 'nav.assignments', path: '/student/assignments', icon: '📋' },
      { labelKey: 'nav.attendance', path: '/student/attendance', icon: '✅' },
      { labelKey: 'nav.grades', path: '/student/grades', icon: '🎓' }
    ]
  };

  get currentNav(): NavItem[] {
    const role = this.role();
    return role ? this.navItems[role] : [];
  }

  roleLabel(): string {
    return this.translate.roleKey(this.role());
  }

  logout(): void {
    this.auth.logout();
  }
}
