import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AssignmentService } from '../../../core/services/assignment.service';
import { ClassService } from '../../../core/services/class.service';
import { TranslatePipe } from '../../../core/i18n/translate.pipe';
import { TranslateService } from '../../../core/i18n/translate.service';
import { Assignment } from '../../../core/models/assignment.model';
import { SchoolClass } from '../../../core/models/class.model';

@Component({
  selector: 'app-teacher-assignments',
  imports: [ReactiveFormsModule, DatePipe, TranslatePipe],
  templateUrl: './teacher-assignments.component.html',
  styleUrl: './teacher-assignments.component.scss'
})
export class TeacherAssignmentsComponent implements OnInit {
  private readonly assignmentService = inject(AssignmentService);
  private readonly classService = inject(ClassService);
  private readonly fb = inject(FormBuilder);
  private readonly translate = inject(TranslateService);

  readonly assignments = signal<Assignment[]>([]);
  readonly classes = signal<SchoolClass[]>([]);
  readonly loading = signal(false);
  readonly saving = signal(false);
  readonly error = signal('');
  readonly showForm = signal(false);
  readonly selectedClassId = signal(0);

  readonly form = this.fb.nonNullable.group({
    classId: [0, Validators.required],
    title: ['', Validators.required],
    description: ['', Validators.required],
    dueDate: ['', Validators.required]
  });

  ngOnInit(): void {
    this.classService.getTeacherClasses().subscribe({
      next: data => this.classes.set(data)
    });
  }

  loadAssignments(): void {
    const classId = this.selectedClassId();
    if (!classId) return;

    this.loading.set(true);
    this.assignmentService.getByClass(classId).subscribe({
      next: data => {
        this.assignments.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(this.translate.get('teacher.assignments.loadFailed'));
        this.loading.set(false);
      }
    });
  }

  onClassChange(classId: number): void {
    this.selectedClassId.set(classId);
    this.form.patchValue({ classId });
    this.loadAssignments();
  }

  toggleForm(): void {
    this.showForm.update(v => !v);
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.saving.set(true);
    this.assignmentService.create(this.form.getRawValue()).subscribe({
      next: () => {
        this.saving.set(false);
        this.showForm.set(false);
        this.loadAssignments();
      },
      error: err => {
        this.saving.set(false);
        this.error.set(err.error?.title || this.translate.get('teacher.assignments.createFailed'));
      }
    });
  }
}
