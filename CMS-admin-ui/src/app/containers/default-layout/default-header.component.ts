import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { 
  HeaderComponent, 
  HeaderNavComponent, 
  HeaderTogglerDirective, 
  ContainerComponent,
  DropdownComponent,
  DropdownToggleDirective,
  DropdownMenuDirective,
  DropdownItemDirective,
  SidebarToggleDirective
} from '@coreui/angular';

@Component({
  selector: 'app-default-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    HeaderComponent,
    HeaderNavComponent,
    HeaderTogglerDirective,
    ContainerComponent,
    DropdownComponent,
    DropdownToggleDirective,
    DropdownMenuDirective,
    DropdownItemDirective,
    SidebarToggleDirective
  ],
  templateUrl: './default-header.component.html'
})
export class DefaultHeaderComponent {
  constructor() {}
}
