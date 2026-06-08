import { Routes } from '@angular/router';
import { authGuard } from './core/auth/auth.guard';
import { roleGuard } from './core/auth/role.guard';
import { AuthLayoutComponent } from './layout/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { AdminDashboardComponent } from './features/admin/dashboard/admin-dashboard.component';
import { UsersComponent } from './features/admin/users/users.component';
import { DepartmentsComponent } from './features/admin/departments/departments.component';
import { CoursesComponent } from './features/admin/courses/courses.component';
import { EnrollmentsComponent } from './features/admin/enrollments/enrollments.component';
import { NotificationsComponent } from './features/admin/notifications/notifications.component';
import { TeacherDashboardComponent } from './features/teacher/dashboard/teacher-dashboard.component';
import { TeacherClassesComponent } from './features/teacher/classes/teacher-classes.component';
import { TeacherAssignmentsComponent } from './features/teacher/assignments/teacher-assignments.component';
import { TeacherAttendanceComponent } from './features/teacher/attendance/teacher-attendance.component';
import { TeacherSubmissionsComponent } from './features/teacher/submissions/teacher-submissions.component';
import { StudentDashboardComponent } from './features/student/dashboard/student-dashboard.component';
import { StudentClassesComponent } from './features/student/classes/student-classes.component';
import { StudentAssignmentsComponent } from './features/student/assignments/student-assignments.component';
import { StudentAttendanceComponent } from './features/student/attendance/student-attendance.component';
import { StudentGradesComponent } from './features/student/grades/student-grades.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ]
  },
  {
    path: 'admin',
    component: MainLayoutComponent,
    canActivate: [authGuard, roleGuard('Admin')],
    children: [
      { path: '', component: AdminDashboardComponent },
      { path: 'users', component: UsersComponent },
      { path: 'departments', component: DepartmentsComponent },
      { path: 'courses', component: CoursesComponent },
      { path: 'attendance', component: TeacherAttendanceComponent },
      { path: 'notifications', component: NotificationsComponent }
    ]
  },
  {
    path: 'teacher',
    component: MainLayoutComponent,
    canActivate: [authGuard, roleGuard('Teacher')],
    children: [
      { path: '', component: TeacherDashboardComponent },
      { path: 'classes', component: TeacherClassesComponent },
      { path: 'enrollments', component: EnrollmentsComponent },
      { path: 'assignments', component: TeacherAssignmentsComponent },
      { path: 'submissions', component: TeacherSubmissionsComponent }
    ]
  },
  {
    path: 'student',
    component: MainLayoutComponent,
    canActivate: [authGuard, roleGuard('Student')],
    children: [
      { path: '', component: StudentDashboardComponent },
      { path: 'classes', component: StudentClassesComponent },
      { path: 'assignments', component: StudentAssignmentsComponent },
      { path: 'attendance', component: StudentAttendanceComponent },
      { path: 'grades', component: StudentGradesComponent }
    ]
  },
  { path: '**', redirectTo: 'login' }
];
