import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ClassService } from '../../../core/services/class.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { StudentClassEnrollment } from '../../../core/models/class.model';

@Component({
  selector: 'app-student-classes',
  imports: [DatePipe, TranslatePipe],
  templateUrl: './student-classes.component.html',
  styleUrl: './student-classes.component.scss'
})
export class StudentClassesComponent implements OnInit {
  private readonly classService = inject(ClassService);
  private readonly translate = inject(TranslateService);

  readonly enrollments = signal<StudentClassEnrollment[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  ngOnInit(): void {
    this.classService.getStudentClasses().subscribe({
      next: data => {
        this.enrollments.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('student.classes.loadFailed'));
        this.loading.set(false);
      }
    });
  }
}
