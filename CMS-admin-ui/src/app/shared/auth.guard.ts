import { inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateFn } from '@angular/router';
import { UrlConstants } from './constants/url.constants';
import { TokenStorageService } from './services/token-storage.service';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
): boolean => {
  const router = inject(Router);
  const tokenService = inject(TokenStorageService);

  const requiredPolicy = route.data['requiredPolicy'] as string;
  const loggedInUser = tokenService.getUser();

  if (loggedInUser) {
    const listPermission = JSON.parse(loggedInUser.permissions);
    if (
      listPermission != null &&
      listPermission != '' &&
      listPermission.filter((x: string) => x == requiredPolicy).length > 0
    ) {
      return true;
    } else {
      router.navigate([UrlConstants.ACCESS_DENIED], {
        queryParams: {
          returnUrl: state.url,
        },
      });
      return false;
    }
  } else {
    router.navigate([UrlConstants.LOGIN], {
      queryParams: {
        returnUrl: state.url,
      },
    });
    return false;
  }
};