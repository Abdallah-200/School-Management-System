import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { AssignmentService } from '../../../core/services/assignment.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Assignment } from '../../../core/models/assignment.model';

@Component({
  selector: 'app-student-assignments',
  imports: [DatePipe, TranslatePipe],
  templateUrl: './student-assignments.component.html',
  styleUrl: './student-assignments.component.scss'
})
export class StudentAssignmentsComponent implements OnInit {
  private readonly assignmentService = inject(AssignmentService);
  private readonly translate = inject(TranslateService);

  readonly assignments = signal<Assignment[]>([]);
  readonly loading = signal(true);
  readonly uploading = signal<number | null>(null);
  readonly error = signal('');
  readonly success = signal('');

  ngOnInit(): void {
    this.loadAssignments();
  }

  loadAssignments(): void {
    this.loading.set(true);
    this.assignmentService.getStudentAssignments().subscribe({
      next: data => {
        this.assignments.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('student.assignments.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  onFileSelected(assignmentId: number, event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;

    this.uploading.set(assignmentId);
    this.error.set('');
    this.success.set('');

    this.assignmentService.submitFile(assignmentId, file).subscribe({
      next: () => {
        this.uploading.set(null);
        this.success.set(this.translate.get('student.assignments.success'));
        input.value = '';
      },
      error: err => {
        this.uploading.set(null);
        this.error.set(err.error?.title || this.translate.get('student.assignments.failed'));
      }
    });
  }
}
