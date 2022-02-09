import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let LogRegService = class LogRegService {
    constructor(http) {
        this.http = http;
        this.URL = 'https://localhost:7047/Home/GetData';
    }
    postData(data) {
        return this.http.post(this.URL, data);
    }
};
LogRegService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], LogRegService);
export { LogRegService };
//# sourceMappingURL=log-reg.service.js.map