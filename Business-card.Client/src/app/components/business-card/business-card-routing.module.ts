import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessViewComponent } from './business-view/business-view.component';
import { BusinessCreateComponent } from './business-create/business-create.component';

const routes: Routes = [{path:'',component:BusinessViewComponent},{path:'Create',component:BusinessCreateComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BusinessCardRoutingModule { }
