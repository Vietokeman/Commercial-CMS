import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Thêm FormsModule cho [(ngModel)]
import { SystemRoutingModule } from './system-routing.module';
import { UserComponent } from './users/user.component';
import { RoleComponent } from './roles/role.component';
import { RoleDetailComponent } from './roles/role-detail.component';
import { PermissionGrantComponent } from './roles/permission-grant.component';
import { ChangeEmailComponent } from './users/change-email.component';
import { RoleAssignComponent } from './users/role-assign.component';
import { SetPasswordComponent } from './users/set-password.component';
import { UserDetailComponent } from './users/user-detail.component';

// PrimeNG Modules
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { BlockUIModule } from 'primeng/blockui';
import { PaginatorModule } from 'primeng/paginator';
import { PanelModule } from 'primeng/panel';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { KeyFilterModule } from 'primeng/keyfilter';
import { BadgeModule } from 'primeng/badge';
import { PickListModule } from 'primeng/picklist';
import { ImageModule } from 'primeng/image';
import { TooltipModule } from 'primeng/tooltip'; // Cho pTooltip
import { DynamicDialogModule } from 'primeng/dynamicdialog'; // Cho DialogService
import { ConfirmDialogModule } from 'primeng/confirmdialog'; // Cho ConfirmationService
import { SharedModule } from 'primeng/api'; // Đã có, giữ nguyên
import { TeduSharedModule } from '../../../app/shared/modules/tedu-shared.module'; // Đã có, giữ nguyên

@NgModule({
  imports: [
    CommonModule,
    FormsModule, // Thêm để hỗ trợ [(ngModel)] trong user.component.html
    ReactiveFormsModule,
    SystemRoutingModule,
    TableModule,
    ProgressSpinnerModule, // Đã có, giữ nguyên
    BlockUIModule,
    PaginatorModule,
    PanelModule,
    CheckboxModule,
    ButtonModule,
    InputTextModule,
    KeyFilterModule,
    SharedModule,
    TeduSharedModule,
    BadgeModule,
    PickListModule,
    ImageModule,
    TooltipModule, // Thêm cho pTooltip
    DynamicDialogModule, // Thêm cho DialogService
    ConfirmDialogModule, // Thêm cho ConfirmationService
  ],
  declarations: [
    UserComponent,
    RoleComponent,
    RoleDetailComponent,
    PermissionGrantComponent,
    ChangeEmailComponent,
    RoleAssignComponent,
    SetPasswordComponent,
    UserDetailComponent,
  ],
})
export class SystemModule {}
