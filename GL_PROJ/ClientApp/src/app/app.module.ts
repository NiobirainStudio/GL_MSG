import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { GroupsComponent } from './components/groups/groups.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NgxWebstorageModule } from 'ngx-webstorage';
import { MainComponent } from './components/main/main.component';

const appRouts: Routes = [
  {path: '', component: LoginComponent},
  {path: 'login', component: LoginComponent},
  {path: 'main', component: MainComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    GroupsComponent,
    LoginComponent,
    MainComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,    
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRouts, { enableTracing: true }),
    NgxWebstorageModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
