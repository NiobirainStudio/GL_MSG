import { Component, OnInit } from '@angular/core';
import { MainService } from 'src/app/services/main.service';

import { GroupDTO } from 'src/app/models/Group';
import { MessageDTO } from 'src/app/models/Message';
import { UserDTO } from 'src/app/models/User';
import { HttpClient } from '@angular/common/http';
import { LogRegDTO } from 'src/app/models/LogRegUser';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(public service: MainService, private http: HttpClient) { }

  ngOnInit(): void {
    this.service.StartConnection();
    this.service.OnReconnectedEvent();
  }

  WriteMessage() {
    this.service.SendTextMessage(this.GetSession(), "I'm a new message!", 1);
  }

  GetGroups() {
    this.service.PostAndRecieveData(this.GetSession(), '/GetUserGroups').subscribe(
      res => {
        console.log(res);
      },
      err => { 
        console.log(err); 
      }
    );
  }

  private GetSession() {
    return (+(localStorage.getItem("UserSession") || -1));
  }
}
