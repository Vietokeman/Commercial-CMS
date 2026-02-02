import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent, FooterModule } from '@coreui/angular';

@Component({
  selector: 'app-default-footer',
  standalone: true,
  imports: [CommonModule, FooterModule],
  templateUrl: './default-footer.component.html',
  styleUrls: ['./default-footer.component.scss'],
})
export class DefaultFooterComponent extends FooterComponent {
  constructor() {
    super();
  }
}
