import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { 
  ClassToggleService, 
  HeaderComponent, 
  HeaderModule, 
  ContainerComponent, 
  DropdownModule, 
  AvatarModule, 
  NavModule, 
  BadgeModule,
  SidebarToggleDirective,
  BreadcrumbModule
} from '@coreui/angular';
import { UrlConstants } from '../../../shared/constants/url.constants';
import { TokenStorageService } from '../../../shared/services/token-storage.service';
import { IconModule } from '@coreui/icons-angular';

@Component({
  selector: 'app-default-header',
  standalone: true,
  imports: [
    CommonModule,
    HeaderModule,
    ContainerComponent,
    DropdownModule,
    AvatarModule,
    NavModule,
    BadgeModule,
    IconModule,
    SidebarToggleDirective,
    BreadcrumbModule
  ],
  templateUrl: './default-header.component.html',
})
export class DefaultHeaderComponent extends HeaderComponent {
  @Input() sidebarId: string = 'sidebar';

  public newMessages = new Array(4);
  public newTasks = new Array(5);
  public newNotifications = new Array(5);

  constructor(
    private classToggler: ClassToggleService,
    private tokenService: TokenStorageService,
    private router: Router
  ) {
    super();
  }

  logout() {
    this.tokenService.signOut();
    this.router.navigate([UrlConstants.LOGIN]);
  }
}
