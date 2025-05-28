import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ChangeEmailComponent } from './change-email.component';
import { RoleAssignComponent } from './role-assign.component';
import { SetPasswordComponent } from './set-password.component';
import { UserDetailComponent } from './user-detail.component';
import {
  AdminApiUserApiClient,
  UserDto,
  UserDtoPagedResult,
} from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';
import { MessageConstants } from '../../../shared/constants/messages.constant';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
})
export class UserComponent implements OnInit, OnDestroy {
  // System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  // Paging variables
  public pageIndex: number = 1;
  public pageSize: number = 10;
  public totalCount?: number;

  // Business variables
  public items?: UserDto[];
  public selectedItems: UserDto[] = [];
  public keyword: string = '';

  constructor(
    private userService: AdminApiUserApiClient,
    public dialogService: DialogService,
    private alertService: AlertService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  ngOnInit() {
    this.loadData();
  }

  loadData(selectionId = null) {
    this.toggleBlockUI(true);
    this.userService
      .getAllUsersPaging(this.keyword, this.pageIndex, this.pageSize)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UserDtoPagedResult) => {
          this.items = response.results;
          this.totalCount = response.rowCount;
          if (selectionId != null && this.items && this.items.length > 0) {
            this.selectedItems = this.items.filter((x) => x.id === selectionId);
          }
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  showAddModal() {
    const ref = this.dialogService.open(UserDetailComponent, {
      header: 'Thêm mới người dùng',
      width: '70%',
    });
    ref.onClose.subscribe((data: UserDto) => {
      if (data) {
        this.alertService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

  pageChanged(event: any): void {
    this.pageIndex = event.page + 1;
    this.pageSize = event.rows;
    this.loadData();
  }

  showEditModal() {
    if (this.selectedItems.length === 0) {
      this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(UserDetailComponent, {
      data: { id: id },
      header: 'Cập nhật người dùng',
      width: '70%',
    });
    ref.onClose.subscribe((data: UserDto) => {
      if (data) {
        this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData(data.id);
      }
    });
  }

  deleteItems() {
    if (this.selectedItems.length === 0) {
      this.alertService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const ids = this.selectedItems.map((element) => element.id);
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteItemsConfirm(ids);
      },
    });
  }

  deleteItemsConfirm(ids: any[]) {
    this.toggleBlockUI(true);
    this.userService.deleteUsers(ids).subscribe({
      next: () => {
        this.alertService.showSuccess(MessageConstants.DELETED_OK_MSG);
        this.loadData();
        this.selectedItems = [];
        this.toggleBlockUI(false);
      },
      error: () => {
        this.toggleBlockUI(false);
      },
    });
  }

  setPassword(id: string) {
    const ref = this.dialogService.open(SetPasswordComponent, {
      data: { id: id },
      header: 'Đặt lại mật khẩu',
      width: '70%',
    });
    ref.onClose.subscribe((result: boolean) => {
      if (result) {
        this.alertService.showSuccess(
          MessageConstants.CHANGE_PASSWORD_SUCCCESS_MSG
        );
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

  changeEmail(id: string) {
    const ref = this.dialogService.open(ChangeEmailComponent, {
      data: { id: id },
      header: 'Đặt lại email',
      width: '70%',
    });
    ref.onClose.subscribe((result: boolean) => {
      if (result) {
        this.alertService.showSuccess(
          MessageConstants.CHANGE_EMAIL_SUCCCESS_MSG
        );
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

  assignRole(id: string) {
    const ref = this.dialogService.open(RoleAssignComponent, {
      data: { id: id },
      header: 'Gán quyền',
      width: '70%',
    });
    ref.onClose.subscribe((result: boolean) => {
      if (result) {
        this.alertService.showSuccess(MessageConstants.ROLE_ASSIGN_SUCCESS_MSG);
        this.loadData();
      }
    });
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 1000);
    }
  }
}
