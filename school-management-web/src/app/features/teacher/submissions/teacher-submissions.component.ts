import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { SubmissionService } from '../../../core/services/submission.service';
import { AssignmentService } from '../../../core/services/assignment.service';
import { ClassService } from '../../../core/services/class.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Submission } from '../../../core/models/submission.model';
import { Assignment } from '../../../core/models/assignment.model';
import { SchoolClass } from '../../../core/models/class.model';

@Component({
  selector: 'app-teacher-submissions',
  imports: [ReactiveFormsModule, DatePipe, TranslatePipe],
  templateUrl: './teacher-submissions.component.html',
  styleUrl: './teacher-submissions.component.scss'
})
export class TeacherSubmissionsComponent implements OnInit {
  private readonly submissionService = inject(SubmissionService);
  private readonly assignmentService = inject(AssignmentService);
  private readonly classService = inject(ClassService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly submissions = signal<Submission[]>([]);
  readonly assignments = signal<Assignment[]>([]);
  readonly classes = signal<SchoolClass[]>([]);
  readonly loading = signal(false);
  readonly grading = signal(false);
  readonly error = signal('');
  readonly success = signal('');
  readonly selectedAssignmentId = signal(0);
  readonly gradingSubmissionId = signal<number | null>(null);

  readonly gradeForm = this.fb.nonNullable.group({
    grade: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
    remarks: ['']
  });

  ngOnInit(): void {
    this.classService.getTeacherClasses().subscribe({
      next: data => this.classes.set(data)
    });
  }

  onClassChange(classId: number): void {
    if (!classId) return;
    this.assignmentService.getByClass(classId).subscribe({
      next: data => this.assignments.set(data)
    });
  }

  loadSubmissions(assignmentId: number): void {
    this.selectedAssignmentId.set(assignmentId);
    if (!assignmentId) return;

    this.loading.set(true);
    this.submissionService.getByAssignment(assignmentId).subscribe({
      next: data => {
        this.submissions.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('teacher.submissions.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  startGrading(submissionId: number, currentGrade?: number): void {
    this.gradingSubmissionId.set(submissionId);
    this.gradeForm.reset({ grade: currentGrade ?? 0, remarks: '' });
  }

  cancelGrading(): void {
    this.gradingSubmissionId.set(null);
  }

  submitGrade(): void {
    const submissionId = this.gradingSubmissionId();
    if (!submissionId || this.gradeForm.invalid) return;

    this.grading.set(true);
    const { grade, remarks } = this.gradeForm.getRawValue();

    this.submissionService.grade({ submissionId, grade, remarks }).subscribe({
      next: () => {
        this.grading.set(false);
        this.gradingSubmissionId.set(null);
        this.success.set(this.translate.get('teacher.submissions.success'));
        this.loadSubmissions(this.selectedAssignmentId());
      },
      error: err => {
        this.grading.set(false);
        this.error.set(err.error?.title || this.translate.get('teacher.submissions.failed'));
      }
    });
  }
}
