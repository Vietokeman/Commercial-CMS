import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IconModule } from '@coreui/icons-angular';
import { ChartjsModule } from '@coreui/angular-chartjs';
import { ContentRoutingModule } from './content-routing.module';

// Components
import { PostComponent } from './posts/post.component';
import { PostDetailComponent } from './posts/post-detail.component';
import { PostCategoryComponent } from './post-categories/post-category.component';
import { PostCategoryDetailComponent } from './post-categories/post-category-detail.component';
import { PostReturnReasonComponent } from './posts/post-return-reason.component';
import { PostSeriesComponent } from './posts/post-series.component';
import { PostActivityLogsComponent } from './posts/post-activity-logs.component';
import { SeriesComponent } from './series/series.component';
import { SeriesDetailComponent } from './series/series-detail.component';
import { SeriesPostsComponent } from './series/series-posts.component';

// PrimeNG Modules
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PanelModule } from 'primeng/panel';
import { BlockUIModule } from 'primeng/blockui';
import { PaginatorModule } from 'primeng/paginator';
import { BadgeModule } from 'primeng/badge';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { EditorModule } from 'primeng/editor';
import { InputNumberModule } from 'primeng/inputnumber';
import { ImageModule } from 'primeng/image';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TeduSharedModule } from '../../shared/modules/tedu-shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ContentRoutingModule,
    IconModule,
    ChartjsModule,
    TeduSharedModule,
    ProgressSpinnerModule,
    PanelModule,
    BlockUIModule,
    PaginatorModule,
    BadgeModule,
    CheckboxModule,
    TableModule,
    KeyFilterModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    EditorModule,
    InputNumberModule,
    ImageModule,
    AutoCompleteModule,
    DynamicDialogModule,
    ConfirmDialogModule,
  ],
  declarations: [
    PostComponent,
    PostDetailComponent,
    PostCategoryComponent,
    PostCategoryDetailComponent,
    PostReturnReasonComponent,
    PostSeriesComponent,
    SeriesComponent,
    SeriesDetailComponent,
    SeriesPostsComponent,
    PostActivityLogsComponent,
  ],
})
export class ContentModule {}
