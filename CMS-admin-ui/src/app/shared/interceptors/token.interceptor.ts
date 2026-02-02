import { inject } from '@angular/core';
import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpErrorResponse,
} from '@angular/common/http';
import {
  catchError,
  Observable,
  switchMap,
  throwError,
  Subject,
  tap,
} from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';
import {
  AdminApiTokenApiClient,
  AuthenticatedResult,
  TokenRequest,
} from '../../api/admin-api.service.generated';
import { Router } from '@angular/router';
import { AlertService } from '../services/alert.service';
import { BroadcastService } from '../services/boardcast.service';

let refreshTokenInProgress = false;
const tokenRefreshedSource = new Subject<string>();
const tokenRefreshed$ = tokenRefreshedSource.asObservable();

function addAuthHeader(
  request: HttpRequest<any>,
  tokenService: TokenStorageService
): HttpRequest<any> {
  const authHeader = tokenService.getToken();
  if (authHeader) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${authHeader}`,
      },
    });
  }
  return request;
}

function refreshToken(
  tokenService: TokenStorageService,
  tokenApiClient: AdminApiTokenApiClient,
  router: Router
): Observable<any> {
  if (refreshTokenInProgress) {
    return new Observable((observer) => {
      tokenRefreshed$.subscribe(() => {
        observer.next();
        observer.complete();
      });
    });
  } else {
    refreshTokenInProgress = true;
    const token = tokenService.getToken();
    const refreshTokenVal = tokenService.getRefreshToken();
    const tokenRequest = new TokenRequest({
      accessToken: token!,
      refreshToken: refreshTokenVal!,
    });
    return tokenApiClient.refresh(tokenRequest).pipe(
      tap((response: AuthenticatedResult) => {
        refreshTokenInProgress = false;
        tokenService.saveToken(response.token!);
        tokenService.saveRefreshToken(response.refreshToken!);
        tokenRefreshedSource.next(response.token!);
      }),
      catchError((err) => {
        refreshTokenInProgress = false;
        logout(tokenService, router);
        return throwError(() => err);
      })
    );
  }
}

function logout(tokenService: TokenStorageService, router: Router): void {
  tokenService.signOut();
  router.navigate(['login']);
}

async function handleResponseError(
  error: HttpErrorResponse,
  request: HttpRequest<any>,
  next: HttpHandlerFn,
  tokenService: TokenStorageService,
  tokenApiClient: AdminApiTokenApiClient,
  router: Router,
  alertService: AlertService,
  broadcastService: BroadcastService
): Promise<Observable<any>> {
  // Business error
  if (error.status === 400) {
    const errMessage = await new Response(error.error).text();
    alertService.showError(errMessage);
    broadcastService.httpError.set(true);
  }
  // Invalid token error
  else if (error.status === 401) {
    return refreshToken(tokenService, tokenApiClient, router).pipe(
      switchMap(() => {
        request = addAuthHeader(request, tokenService);
        return next(request);
      }),
      catchError((e) => {
        if (e.status !== 401) {
          return handleResponseError(
            e,
            request,
            next,
            tokenService,
            tokenApiClient,
            router,
            alertService,
            broadcastService
          );
        } else {
          logout(tokenService, router);
          return throwError(() => e);
        }
      })
    );
  }
  // Access denied error
  else if (error.status === 403) {
    logout(tokenService, router);
    broadcastService.httpError.set(true);
  }
  // Server error
  else if (error.status === 500) {
    alertService.showError('Hệ thống có lỗi xảy ra. Vui lòng liên hệ admin');
    broadcastService.httpError.set(true);
  }

  return throwError(() => error);
}

export const tokenInterceptor: HttpInterceptorFn = (
  request: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<any> => {
  const tokenService = inject(TokenStorageService);
  const tokenApiClient = inject(AdminApiTokenApiClient);
  const router = inject(Router);
  const alertService = inject(AlertService);
  const broadcastService = inject(BroadcastService);

  // Add auth header
  request = addAuthHeader(request, tokenService);

  // Handle response
  return next(request).pipe(
    catchError((error: HttpErrorResponse) => {
      return handleResponseError(
        error,
        request,
        next,
        tokenService,
        tokenApiClient,
        router,
        alertService,
        broadcastService
      );
    })
  );
};
