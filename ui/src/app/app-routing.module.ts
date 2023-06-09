import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { AccountListComponent } from './account-list/account-list.component';

const routes: Routes = [
    { path: 'account-details/:id', component: AccountDetailsComponent },
    { path: 'account-list', component: AccountListComponent },
    { path: '', redirectTo: '/account-list', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
