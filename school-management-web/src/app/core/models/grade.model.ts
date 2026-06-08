export interface Grade {
  id: number;
  studentId: number;
  classId: number;
  assignmentId: number;
  score: number;
  createdAt: string;
  assignment?: { id: number; title: string };
  class?: { id: number; name: string };
}
