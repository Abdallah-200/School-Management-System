export interface Assignment {
  id: number;
  classId: number;
  title: string;
  description: string;
  dueDate: string;
  createdDate?: string;
  createdByTeacherId: number;
  class?: { id: number; name: string };
}

export interface CreateAssignmentRequest {
  classId: number;
  title: string;
  description: string;
  dueDate: string;
  createdByTeacherId?: number;
}
