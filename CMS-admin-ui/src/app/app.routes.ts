import { Routes } from '@angular/router';
import { DefaultLayoutComponent } from './layout';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./views/auths/routes').then((m) => m.routes),
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Trang chủ',
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./views/dashboard/routes').then((m) => m.routes),
      },
      {
        path: 'system',
        loadChildren: () =>
          import('./views/system/routes').then((m) => m.routes),
      },
      {
        path: 'content',
        loadChildren: () =>
          import('./views/content/routes').then((m) => m.routes),
      },
    ],
  },

  { path: '**', redirectTo: 'dashboard' },
];
