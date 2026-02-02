import { Routes } from '@angular/router';
import { DefaultLayoutComponent } from './containers';
import { authGuard } from './shared/auth.guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./views/auth/auth.module').then((m) => m.AuthModule),
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
        loadChildren: () => import('./views/dashboard/dashboard.module').then((m) => m.DashboardModule),
      },
      {
        path: 'system',
        loadChildren: () => import('./views/system/system.module').then((m) => m.SystemModule),
      },
      {
        path: 'content',
        loadChildren: () => import('./views/content/content.module').then((m) => m.ContentModule),
      },
      {
        path: 'royalty',
        loadChildren: () => import('./views/royalty/royalty.module').then((m) => m.RoyaltyModule),
      },
    ],
  },
  { path: '**', redirectTo: 'dashboard' },
];
