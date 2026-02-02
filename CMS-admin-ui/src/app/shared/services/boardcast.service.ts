import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable()
export class BroadcastService {
    public httpError: WritableSignal<boolean>;

    constructor() {
        // Initialize with signal
        this.httpError = signal<boolean>(false);
    }
}