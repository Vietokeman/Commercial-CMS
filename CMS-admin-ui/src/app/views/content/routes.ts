import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'post',
    pathMatch: 'full',
  },
  {
    path: 'post',

    loadComponent: () =>
      import('./posts/post.component').then((m) => m.PostComponent),
    data: {
      title: `Posts`,
    },
  },
];
