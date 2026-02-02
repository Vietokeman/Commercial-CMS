import {
  ApplicationConfig,
  provideZoneChangeDetection,
  importProvidersFrom,
} from '@angular/core';
import { provideRouter, withHashLocation } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { MessageService, ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { IconSetService } from '@coreui/icons-angular';

import { routes } from './app-routing';
import { ADMIN_API_BASE_URL } from './api/admin-api.service.generated';
import { environment } from '../environments/environment';
import { AlertService } from './shared/services/alert.service';
import { TokenStorageService } from './shared/services/token-storage.service';
import { UtilityService } from './shared/services/utility.service';
import { UploadService } from './shared/services/upload.service';
import { BroadcastService } from './shared/services/boardcast.service';
import { tokenInterceptor } from './shared/interceptors/token.interceptor';

// API Clients
import {
  AdminApiAuthApiClient,
  AdminApiPostApiClient,
  AdminApiPostCategoryApiClient,
  AdminApiRoleApiClient,
  AdminApiRoyaltyApiClient,
  AdminApiSeriesApiClient,
  AdminApiTestApiClient,
  AdminApiTokenApiClient,
  AdminApiUserApiClient,
} from './api/admin-api.service.generated';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withHashLocation()),
    provideAnimations(),
    provideHttpClient(withInterceptors([tokenInterceptor])),

    // API Base URL
    { provide: ADMIN_API_BASE_URL, useValue: environment.API_URL },

    // Core Services
    IconSetService,
    Title,
    MessageService,
    AlertService,
    TokenStorageService,
    UtilityService,
    ConfirmationService,
    UploadService,
    BroadcastService,
    DialogService,

    // API Clients
    AdminApiAuthApiClient,
    AdminApiTestApiClient,
    AdminApiTokenApiClient,
    AdminApiRoleApiClient,
    AdminApiUserApiClient,
    AdminApiPostCategoryApiClient,
    AdminApiPostApiClient,
    AdminApiSeriesApiClient,
    AdminApiRoyaltyApiClient,
  ],
};
