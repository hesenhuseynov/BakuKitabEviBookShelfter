import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { UserGuard } from '../../core/guards/user.guard';

const routes: Routes = [

  { 
     path:'',
    component:UserPanelComponent,
    canActivate:[UserGuard],
    children:[
      { path: 'profile', component: UserProfileComponent },
      { path: 'dashboard', component: UserDashboardComponent },
      { path: 'settings', component: UserSettingsComponent },
    ]
  

  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserPanelRoutingModule { }
