import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms'; // Import ReactiveFormsModule
import { BusinessCardRoutingModule } from './business-card-routing.module';
import { BusinessViewComponent } from './business-view/business-view.component';
import { BusinessCreateComponent } from './business-create/business-create.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    BusinessViewComponent,
    BusinessCreateComponent
  ],
  imports: [
    CommonModule,
    BusinessCardRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class BusinessCardModule { }
