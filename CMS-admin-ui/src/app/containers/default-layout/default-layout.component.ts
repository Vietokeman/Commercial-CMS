import { Component, OnInit } from '@angular/core';

import { navItems } from './_nav';
import { TokenStorageService } from '../../shared/services/token-storage.service';
import { Router } from '@angular/router';
import { UrlConstants } from '../../shared/constants/url.constants';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent implements OnInit {
  public navItems = [];

  constructor(
    private tokenService: TokenStorageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const user = this.tokenService.getUser();
    if (user == null) this.router.navigate([UrlConstants.LOGIN]);
    const permissions = JSON.parse(user?.permissions);
    for (let index = 0; index < navItems.length; index++) {
      for (
        var childIndex = 0;
        childIndex < navItems[index].children?.length;
        childIndex++
      ) {
        if (
          navItems[index].children[childIndex].attributes &&
          permissions.filter(
            (x) =>
              x == navItems[index].children[childIndex].attributes['policyName']
          ).length == 0
        ) {
          navItems[index].children[childIndex].class = 'hidden';
        }
      }
    }
    this.navItems = navItems;
  }
}
