import { __decorate, __param } from "tslib";
import { Component, Inject } from '@angular/core';
import { LogRegDTO } from 'src/app/models/LogRegUser';
import { DOCUMENT } from '@angular/common';
let LoginComponent = class LoginComponent {
    constructor(service, document) {
        this.service = service;
        this.document = document;
    }
    ngOnInit() {
    }
    onSubmitForm() {
        this.service.postData(new LogRegDTO(this.UserName, this.Password)).subscribe(res => {
            alert(res);
        }, err => {
            console.log(err);
        });
    }
    goToURL(path) {
        this.document.location.href = path;
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    }),
    __param(1, Inject(DOCUMENT))
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map