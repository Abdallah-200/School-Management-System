import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { GradeService } from '../../../core/services/grade.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Grade } from '../../../core/models/grade.model';

@Component({
  selector: 'app-student-grades',
  imports: [DatePipe, TranslatePipe],
  templateUrl: './student-grades.component.html',
  styleUrl: './student-grades.component.scss'
})
export class StudentGradesComponent implements OnInit {
  private readonly gradeService = inject(GradeService);
  private readonly translate = inject(TranslateService);

  readonly grades = signal<Grade[]>([]);
  readonly loading = signal(true);
  readonly error = signal('');

  ngOnInit(): void {
    this.gradeService.getMyGrades().subscribe({
      next: data => {
        if (Array.isArray(data)) {
          this.grades.set(data);
        }
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('student.grades.loadFailed'));
        this.loading.set(false);
      }
    });
  }
}
