import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { GroupsComponent } from './components/groups/groups.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
const appRouts = [
    { path: '', component: LoginComponent }
];
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            GroupsComponent,
            LoginComponent
        ],
        imports: [
            BrowserModule,
            FormsModule,
            ReactiveFormsModule,
            HttpClientModule,
            RouterModule.forRoot(appRouts, { enableTracing: true })
        ],
        providers: [],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map