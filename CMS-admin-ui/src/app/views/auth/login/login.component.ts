import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import {
  AdminApiAuthApiClient,
  AuthenticatedResult,
  LoginRequest,
} from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';
import { UrlConstants } from '../../../shared/constants/url.constants';
import { TokenStorageService } from '../../../shared/services/token-storage.service';
import { Subject, takeUntil } from 'rxjs';
import { toObservable } from '@angular/core/rxjs-interop';
import { BroadcastService } from '../../../shared/services/boardcast.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  private ngUnsubscribe = new Subject<void>();
  loading = false;

  constructor(
    private fb: FormBuilder,
    private authApiClient: AdminApiAuthApiClient,
    private alertService: AlertService,
    private router: Router,
    private tokenSerivce: TokenStorageService,
    private broadCastService: BroadcastService
  ) {
    this.loginForm = this.fb.group({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }
  ngOnInit(): void {
    toObservable(this.broadCastService.httpError).subscribe((values) => {
      this.loading = false;
    });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  login() {
    this.loading = true;
    var request: LoginRequest = new LoginRequest({
      userName: this.loginForm.controls['userName'].value,
      password: this.loginForm.controls['password'].value,
    });

    this.authApiClient
      .login(request)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: AuthenticatedResult) => {
          //Save token and refresh token to localstorage
          this.tokenSerivce.saveToken(res.token ?? 'default-token');
          this.tokenSerivce.saveRefreshToken(
            res.refreshToken ?? 'default-token'
          );
          this.tokenSerivce.saveUser(res);
          //Redirect to dashboard
          this.router.navigate([UrlConstants.HOME]);
        },
        error: (error: any) => {
          console.log('Lỗi đăng nhập:', error);
          if (error.status === 401) {
            this.alertService.showError('Thông tin xác thực không hợp lệ.');
          } else {
            this.alertService.showError('Đăng nhập không đúng.');
          }
          this.loading = false;
        },
        complete: () => {
          setTimeout(() => {
            this.loading = false;
          }, 3000);
        },
      });
  }
}
