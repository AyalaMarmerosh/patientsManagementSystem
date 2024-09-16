import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { Patient } from '../_models/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private apiUrl = 'https://localhost:7040/api/Patients';

  constructor(private http : HttpClient) { }

  getPatients(page: number, pageSize: number): Observable<{patients : Patient[], totalPatients: number}>{
    return this.http.get<{ patients: Patient[], totalPatients: number}>( `${this.apiUrl}?page=${page}&pageSize=${pageSize}`)
      .pipe(catchError(this.handleError<{ patients: Patient[], totalPatients: number }>('getPatients', {patients: [], totalPatients: 0}))
    );
  }

  searchPatients(query: string): Observable<Patient[]>{
    return this.http.get<Patient[]>(`${this.apiUrl}/search?query=${query}`)
      .pipe(
      catchError(this.handleError<Patient[]>('searchPatients', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: HttpErrorResponse): Observable<T> => {
      console.error(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
