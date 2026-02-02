import { Routes } from '@angular/router';
import { PostComponent } from './posts/post.component';
import { PostCategoryComponent } from './post-categories/post-category.component';
import { authGuard } from '../../shared/auth.guard';
import { SeriesComponent } from './series/series.component';

export const CONTENT_ROUTES: Routes = [
  {
    path: '',
    redirectTo: 'posts',
    pathMatch: 'full',
  },
  {
    path: 'posts',
    component: PostComponent,
    data: {
      title: 'Bài viết',
      requiredPolicy: 'Permissions.Posts.View',
    },
    canActivate: [authGuard],
  },
  {
    path: 'post-categories',
    component: PostCategoryComponent,
    data: {
      title: 'Danh mục',
      requiredPolicy: 'Permissions.PostCategories.View',
    },
    canActivate: [authGuard],
  },
  {
    path: 'series',
    component: SeriesComponent,
    canActivate: [authGuard],
    data: {
      requiredPolicy: 'Permissions.Series.View',
    },
  },
];