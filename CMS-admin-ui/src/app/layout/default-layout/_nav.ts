import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Trang chủ',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW',
    },
  },
  {
    name: 'Nội dung',
    url: '/content',
    iconComponent: { name: 'cil-notes' },
    badge: {
      color: 'info',
      text: 'NEW',
    },
    children: [
      {
        name: 'Danh mục',
        url: '/content/post-categories',
        icon: 'nav-icon-bullet',
      },
      {
        name: 'Bài viết',
        url: '/content/posts',
        icon: 'nav-icon-bullet',
      },
      {
        name: 'Loạt bài',
        url: '/content/series',
        icon: 'nav-icon-bullet',
      },
      {
        name: 'Nhuân bút',
        url: '/content/royalty',
        icon: 'nav-icon-bullet',
      },

      // {

      //   name: 'Pages',
      //   url: '/login',
      //   iconComponent: { name: 'cil-star' },
      //   children: [
      //     {
      //       name: 'Login',
      //       url: '/login',
      //       icon: 'nav-icon-bullet',
      //     },
      //     {
      //       name: 'Register',
      //       url: '/register',
      //       icon: 'nav-icon-bullet',
      //     },
      //     {
      //       name: 'Error 404',
      //       url: '/404',
      //       icon: 'nav-icon-bullet',
      //     },
      //     {
      //       name: 'Error 500',
      //       url: '/500',
      //       icon: 'nav-icon-bullet',
      //     },
      //   ],
      // },
    ],
  },
  {
    name: 'Hệ thống',
    url: '/system',
    iconComponent: { name: 'cil-calculator' },
    children: [
      {
        name: 'Quyền',
        url: '/system/roles',
        icon: 'nav-icon-bullet',
      },
      {
        name: 'Người đăng',
        url: '/system/users',
        icon: 'nav-icon-bullet',
      },
    ],
  },
];
