import { Component, Inject, OnInit } from '@angular/core';
import { LogRegDTO } from 'src/app/models/LogRegUser';
import { LogRegService } from 'src/app/services/log-reg.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  UserName: string;
  Password: string;

  constructor(
    public service: LogRegService, 
    private router: Router) {}

  // Login component constructor
  ngOnInit() {
    //this.checkUserSession();
  }

  // Send login form to the server
  onSubmitLoginForm() {
    this.service.postLogRegData(new LogRegDTO(this.UserName, this.Password), '/SignIn').subscribe(
      res => {
        // Recieving a JSON with an execution code
        // c: 0 - user doesn't exist
        // c: 1 - incorrect password
        // c: 2 - all good, recieving session
        switch (res.code){
          case 0: {
            console.log("User doesn't exist!");
            break;
          }
          
          case 1: {
            console.log("Incorrect password!")
            break;
          }

          case 2: {
            localStorage.setItem("UserSession", res.session);
            this.goToURL('/main');
            break;
          }
        }
      },
      err => { 
        console.log(err); 
      }
    );
  }

  // Redirection method
  goToURL(where: string) {
    this.router.navigate([where])
  }

  // Check if user's session is already in the system
  checkUserSession(){
    var data = localStorage.getItem("UserSession") || '';
    this.service.checkSession(data).subscribe(
      res => {
        if (res){
          this.goToURL('/main');
        }
      },
      err => { 
        console.log(err); 
      }
    );
  }
}