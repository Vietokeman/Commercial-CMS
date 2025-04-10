import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from '../../../shared/constants/messages.constant';
import { SeriesDetailComponent } from './series-detail.component';
import {
  AdminApiSeriesApiClient,
  PostInListDtoPagedResult,
  SeriesDto,
  SeriesInListDto,
} from '../../../api/admin-api.service.generated';
import { AlertService } from '../../../shared/services/alert.service';
import { SeriesPostsComponent } from '../../../views/content/series/series-posts.component';

@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
})
export class SeriesComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  public pageIndex: number = 1;
  public pageSize: number = 10;
  public totalCount?: number;

  public items?: SeriesInListDto[];
  public selectedItems: SeriesInListDto[] = [];
  public keyword: string = '';

  constructor(
    private seriesApiClient: AdminApiSeriesApiClient,
    public dialogService: DialogService,
    private notificationService: AlertService,
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

    this.seriesApiClient
      .getSeriesPaging(this.keyword, this.pageIndex, this.pageSize)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PostInListDtoPagedResult) => {
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
    const ref = this.dialogService.open(SeriesDetailComponent, {
      header: 'Thêm mới series bài viết',
      width: '70%',
    });
    ref.onClose.subscribe((data: SeriesDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
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
      this.notificationService.showError(
        MessageConstants.NOT_CHOOSE_ANY_RECORD
      );
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(SeriesDetailComponent, {
      data: { id: id },
      header: 'Cập nhật series bài viết',
      width: '70%',
    });
    ref.onClose.subscribe((data: SeriesDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData(data.id);
      }
    });
  }

  showPosts() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(
        MessageConstants.NOT_CHOOSE_ANY_RECORD
      );
      return;
    }
    const id = this.selectedItems[0].id;
    const ref = this.dialogService.open(SeriesPostsComponent, {
      data: { id: id },
      header: 'Quản lý danh sách bài viết',
      width: '70%',
    });
    ref.onClose.subscribe((data: SeriesDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData(data.id);
      }
    });
  }

  deleteItems() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(
        MessageConstants.NOT_CHOOSE_ANY_RECORD
      );
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

    this.seriesApiClient.deleteSeries(ids).subscribe({
      next: () => {
        this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
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
