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
    console.log('Start login');
    this.loading = true;
    const request: LoginRequest = new LoginRequest({
      userName: this.loginForm.controls['userName'].value,
      password: this.loginForm.controls['password'].value,
    });
    console.log('Request:', request);

    this.authApiClient
      .login(request)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: AuthenticatedResult) => {
          console.log('Login success:', res);
          if (res.token && res.refreshToken) {
            this.tokenSerivce.saveToken(res.token);
            this.tokenSerivce.saveRefreshToken(res.refreshToken);
            this.tokenSerivce.saveUser(res);
            this.router.navigate([UrlConstants.HOME]);
          }
        },
        error: (error: Error) => {
          console.log('Login error:', error);
          this.alertService.showError('Đăng nhập không đúng.');
          this.loading = false;
        },
        complete: () => {
          console.log('Login complete');
        },
      });
  }
}
