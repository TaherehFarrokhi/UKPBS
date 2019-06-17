import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Event, EventDetail } from '../models';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseUrl = 'https://localhost:5001/api/events';

  constructor(private http: HttpClient) {}

  getEvents(fromDate: Date, toDate: Date): Observable<Event[]> {
    return this.http.get<Event[]>(
      `${this.baseUrl}/from/${this.toDateStringWithoutTime(
        fromDate
      )}/to/${this.toDateStringWithoutTime(toDate)}`
    );
  }

  getEvent(id: number, startDate: Date): Observable<EventDetail> {
    return this.http.get<EventDetail>(
      `${this.baseUrl}/${id}/${this.toDateStringWithoutTime(startDate)}`
    );
  }

  private toDateStringWithoutTime(date: Date): string {
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
  }
}
