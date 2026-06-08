export interface Course {
  id: number;
  name?: string;
  code?: string;
  description?: string;
  departmentId: number;
  credits: number;
}

export interface CreateCourseRequest {
  name?: string;
  code?: string;
  description?: string;
  departmentId: number;
  credits: number;
}

export interface UpdateCourseRequest {
  name?: string;
  code?: string;
}
