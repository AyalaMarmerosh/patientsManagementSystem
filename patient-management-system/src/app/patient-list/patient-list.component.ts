import { Patient } from './../_models/patient';
import { Component, OnInit } from '@angular/core';
import { Appointment } from '../_models/appointment';
import { PatientService } from '../_services/patient.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgModel } from '@angular/forms';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';




@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule, FormsModule, MatPaginatorModule],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.css'
})
export class PatientListComponent implements OnInit  {

  patients: Patient[] = [];
  searchQuery = '';
  pageSize = 10;
  totalPatients: number =0;
  pageIndex: number = 0;

  constructor(private patientService: PatientService) { }

  ngOnInit() {
    this.loadPatients();
  }

  loadPatients() {
    if (this.searchQuery) {
      this.patientService.searchPatients(this.searchQuery).subscribe((data) => {
        this.patients = data;
      });
    } else {
      this.patientService.getPatients(this.pageIndex + 1, this.pageSize).subscribe((data) => {
        this.patients = data.patients;
        this.totalPatients = data.totalPatients;
      });
    }

  }

  getClosestAppointment(appointments: any[]): any {
    const today = new Date();
    const futureAppointments = appointments.filter(
      (a) => new Date(a.date) >= today
    );
    if (futureAppointments.length > 0) {
      return futureAppointments[0];
    } else if (appointments.length > 0) {
      return appointments[appointments.length - 1];
    } else {
      return null;
    }
  }

  pageChanged(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.loadPatients();
  }
}
