import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SidebarComponent } from './pages/sidebar/sidebar.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ExchangeratesComponent } from './pages/exchangerates/exchangerates.component';
import { PurchaseComponent } from './pages/purchase/purchase.component';


const routes: Routes = [
  {path:'',
  component: SidebarComponent,  children: [
    {path: 'dashboard', component: DashboardComponent},
    {path: 'exchagerates', component: ExchangeratesComponent},
    {path: 'purchase', component: PurchaseComponent},
    {path:'**', component: DashboardComponent},
  ]
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  
})
export class AppRoutingModule { }
