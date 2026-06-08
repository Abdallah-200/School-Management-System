export interface Submission {
  id: number;
  assignmentId: number;
  studentId: number;
  submittedDate: string;
  fileUrl: string;
  grade?: number;
  gradedByTeacherId?: number;
  remarks?: string;
  assignment?: { id: number; title: string };
  student?: { id: number; fullName: string };
}

export interface CreateSubmissionRequest {
  assignmentId: number;
  fileUrl: string;
}

export interface GradeSubmissionRequest {
  submissionId: number;
  grade: number;
  remarks: string;
}
