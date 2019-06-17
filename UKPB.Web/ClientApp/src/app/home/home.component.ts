import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';

import { Event } from './../models';
import { EventService } from './../services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  dateRange: Date[];

  loading = false;
  events: Event[] = [];

  constructor(private eventService: EventService) {
    const date = new Date(Date.now());
    this.dateRange = [this.getStartOfWeek(date), this.getEndOfWeek(date)];
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  reload() {
    this.loadEvents();
  }

  private loadEvents(): void {
    this.loading = true;
    this.eventService
      .getEvents(this.dateRange[0], this.dateRange[1])
      .pipe(
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe((events: Event[]) => (this.events = events), err => {});
  }

  private getStartOfWeek(date: Date): Date {
    const diff =
      date.getDate() - date.getDay() + (date.getDay() === 0 ? -6 : 1);
    return new Date(date.setDate(diff));
  }

  private getEndOfWeek(date: Date): Date {
    const lastday = date.getDate() - (date.getDay() - 1) + 6;
    return new Date(date.setDate(lastday));
  }
}
