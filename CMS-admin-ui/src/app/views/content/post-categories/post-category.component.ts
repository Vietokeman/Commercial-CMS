import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from '../../../shared/constants/messages.constant';
import { PostCategoryDetailComponent } from './post-category-detail.component';
import {
  AdminApiPostCategoryApiClient,
  PostCategoryDto,
  PostCategoryDtoPagedResult,
} from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';

@Component({
  selector: 'app-post-category',
  templateUrl: './post-category.component.html',
  styleUrls: ['./post-category.component.scss'],
})
export class PostCategoryComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;
  public pageIndex: number = 1;
  public pageSize: number = 10;
  public totalCount?: number;
  public items?: PostCategoryDto[];
  public selectedItems: PostCategoryDto[] = [];
  public keyword: string = '';

  constructor(
    private postCategoryService: AdminApiPostCategoryApiClient,
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

  loadData() {
    this.toggleBlockUI(true);
    this.postCategoryService
      .getPostCategoriesPaging(this.keyword, this.pageIndex, this.pageSize)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PostCategoryDtoPagedResult) => {
          this.items = response.results;
          this.totalCount = response.rowCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  showAddModal() {
    const ref: DynamicDialogRef = this.dialogService.open(
      PostCategoryDetailComponent,
      {
        header: 'Thêm mới loại bài viết',
        width: '70%',
        contentStyle: { overflow: 'auto' },
        baseZIndex: 10000,
      }
    );

    ref.onClose.subscribe((data: PostCategoryDto) => {
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
    const ref: DynamicDialogRef = this.dialogService.open(
      PostCategoryDetailComponent,
      {
        data: { id: id },
        header: 'Cập nhật loại bài viết',
        width: '70%',
        contentStyle: { overflow: 'auto' },
        baseZIndex: 10000,
      }
    );

    ref.onClose.subscribe((data: PostCategoryDto) => {
      if (data) {
        this.alertService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
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
    this.postCategoryService.deletePostCategory(ids).subscribe({
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
