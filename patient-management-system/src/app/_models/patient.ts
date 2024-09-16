import { Appointment } from "./appointment";

export interface Patient {
    id: number;
    name: string;
    appointments: Appointment[];
  }