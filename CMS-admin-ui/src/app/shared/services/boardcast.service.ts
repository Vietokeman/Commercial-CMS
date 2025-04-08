import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BroadcastService {
  public httpError: Subject<HttpErrorResponse> =
    new Subject<HttpErrorResponse>();

  constructor() {
    //initialize it to false
    this.httpError = new Subject<HttpErrorResponse>();
  }
}
