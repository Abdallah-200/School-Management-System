import { UserRole } from './auth.model';

export interface CreateNotificationRequest {
  title: string;
  message: string;
  recipientRole: UserRole;
  recipientId?: number;
}
