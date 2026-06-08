export interface SchoolClass {
  id: number;
  name: string;
  courseId: number;
  teacherId: number;
  semester: string;
  startDate: string;
  endDate: string;
  isActive?: boolean;
  course?: { id: number; name?: string; code?: string };
  teacher?: { id: number; fullName: string };
}

export interface CreateClassRequest {
  name?: string;
  courseId: number;
  teacherId: number;
  semester?: string;
  startDate: string;
  endDate: string;
}

export interface StudentClassEnrollment {
  id: number;
  studentId: number;
  classId: number;
  enrollmentDate: string;
  student?: { id: number; fullName: string; email: string };
  class?: SchoolClass;
}

export interface EnrollStudentRequest {
  studentId: number;
  classId: number;
}
