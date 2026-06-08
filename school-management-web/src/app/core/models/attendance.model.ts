export type AttendanceStatus = 'Present' | 'Absent' | 'Late';

export interface Attendance {
  id: number;
  classId: number;
  studentId: number;
  date: string;
  status: AttendanceStatus;
  markedByTeacherId: number;
  createdDate?: string;
  student?: { id: number; fullName: string };
  class?: { id: number; name: string };
}

export interface CreateAttendanceRequest {
  classId: number;
  studentId: number;
  date: string;
  status: AttendanceStatus;
}
