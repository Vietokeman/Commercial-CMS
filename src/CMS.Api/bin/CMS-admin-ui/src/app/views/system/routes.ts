import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'user',
    pathMatch: 'full',
  },
  {
    path: 'user',
    loadComponent: () =>
      import('./users/user.component').then((m) => m.UserComponent),
    data: {
      title: `Posts`,
    },
  },
];
