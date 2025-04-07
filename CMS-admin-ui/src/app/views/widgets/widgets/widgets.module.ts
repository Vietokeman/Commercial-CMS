import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WidgetsRoutingModule } from '../widgets-routing.module';
import { WidgetsComponent } from './widgets.component';
import {
  CardModule,
  ProgressModule,
  GridModule,
  WidgetModule,
  ButtonModule,
  DropdownModule,
  SharedModule,
  BadgeModule,
} from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { ChartjsModule } from '@coreui/angular-chartjs';

@NgModule({
  imports: [
    CommonModule,
    WidgetsRoutingModule,
    ProgressModule,
    GridModule,
    WidgetModule,
    CardModule,
    IconModule,
    ButtonModule,
    DropdownModule,
    SharedModule,
    BadgeModule,
    ChartjsModule,
  ],
  declarations: [WidgetsComponent],
  schemas: [NO_ERRORS_SCHEMA],
})
export class WidgetsModule {}
