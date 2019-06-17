import { Member } from './member';

export interface EventDetail extends Event {
  category: string;
  members: Member[];
}
