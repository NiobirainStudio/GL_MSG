export class LogRegDTO {
    UserName: string;
    Password: string;

    constructor(u: string, p: string){
        this.UserName = u;
        this.Password = p;
    }
}