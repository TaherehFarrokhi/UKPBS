import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { EventDetail } from './../models/event-detail';
import { EventService } from './../services/event.service';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrls: ['./event.component.css']
})
export class EventComponent implements OnInit {
  event: EventDetail;
  loading = false;

  constructor(
    private eventService: EventService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      const eventId = +params['id'];
      const startDate = new Date(params['startDate']);
      this.loadEvent(eventId, startDate);
    });
  }
  private loadEvent(eventId: number, startDate: Date): void {
    this.loading = true;
    this.eventService
      .getEvent(eventId, startDate)
      .pipe(
        finalize(() => {
          this.loading = false;
        })
      )
      .subscribe((event: EventDetail) => (this.event = event), err => {});
  }
}
