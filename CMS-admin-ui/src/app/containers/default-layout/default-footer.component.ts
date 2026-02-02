import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from '@coreui/angular';

@Component({
  selector: 'app-default-footer',
  standalone: true,
  imports: [
    CommonModule,
    FooterComponent
  ],
  templateUrl: './default-footer.component.html'
})
export class DefaultFooterComponent {
  constructor() {}
}
