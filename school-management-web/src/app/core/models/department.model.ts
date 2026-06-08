export interface Department {
  id: number;
  name?: string;
  description?: string;
  headOfDepartmentId?: number;
  headOfDepartmentName?: string;
}

export interface CreateDepartmentRequest {
  name: string;
  description?: string;
  headOfDepartmentId: number;
}

export interface UpdateDepartmentRequest {
  name?: string;
  description?: string;
}
