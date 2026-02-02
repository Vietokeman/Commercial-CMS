import { Routes } from '@angular/router';
import { DefaultLayoutComponent } from './containers';
import { authGuard } from './shared/auth.guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./views/auth/auth.routes').then((m) => m.AUTH_ROUTES),
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [authGuard],
    data: {
      title: 'Home',
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/dashboard.routes').then((m) => m.DASHBOARD_ROUTES),
      },
      {
        path: 'system',
        loadChildren: () => import('./views/system/system.routes').then((m) => m.SYSTEM_ROUTES),
      },
      {
        path: 'content',
        loadChildren: () => import('./views/content/content.routes').then((m) => m.CONTENT_ROUTES),
      },
      {
        path: 'royalty',
        loadChildren: () => import('./views/royalty/royalty.routes').then((m) => m.ROYALTY_ROUTES),
      },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];
