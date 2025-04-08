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
  errorMessage: string | null = null;

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
    this.broadCastService.httpError.asObservable().subscribe(() => {
      this.loading = false;
    });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  login() {
    if (!this.loginForm.valid) {
      this.alertService.showError('Vui lòng nhập đầy đủ thông tin.');
      this.errorMessage = 'Vui lòng nhập đầy đủ thông tin.';
      return;
    }

    this.loading = true;
    const request: LoginRequest = new LoginRequest({
      userName: this.loginForm.controls['userName'].value,
      password: this.loginForm.controls['password'].value,
    });
    console.log(request);

    this.authApiClient
      .login(request)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: AuthenticatedResult) => {
          this.loading = false;
          this.errorMessage = null; // Xóa thông báo lỗi nếu thành công
          if (res && res.token) {
            this.tokenSerivce.saveToken(res.token);
            if (res.refreshToken) {
              this.tokenSerivce.saveRefreshToken(res.refreshToken);
            }
            this.tokenSerivce.saveUser(res);
            this.router.navigate([UrlConstants.HOME]);
          } else {
            this.loading = false;
            this.alertService.showError('Tài khoản hoặc mật khẩu không đúng.');
            this.errorMessage = 'Tài khoản hoặc mật khẩu không đúng.';
          }
        },
        error: (error) => {
          this.loading = false;
          console.log('Lỗi đăng nhập:', error);
          if (error.status === 401) {
            this.alertService.showError('Tài khoản hoặc mật khẩu không đúng.');
            this.errorMessage = 'Tài khoản hoặc mật khẩu không đúng.';
          } else {
            this.alertService.showError(
              'Đăng nhập không thành công. Vui lòng thử lại sau.'
            );
            this.errorMessage =
              'Đăng nhập không thành công. Vui lòng thử lại sau.';
          }
        },
        complete: () => {
          setTimeout(() => {
            this.loading = false;
            this.errorMessage = null;
          }, 3000);
        },
      });
  }
}
