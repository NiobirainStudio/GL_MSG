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
    this.service.startConnectionPoke();

    this.service.addBroadcastListener();

    //this.startHttpRequest();
  }

  public WriteMessage() {
    this.service.WriteMessage();
  }

  public AddToGroups() {
    this.service.AddToGroups();
  }

  /*
  private startHttpRequest = () => {
    this.http.get('https://localhost:7047/home/pinger')
      .subscribe(res => {
        console.log(res);
      })
  }
  */

  /*
  // DATA STORAGE NAMING CONVENTION DCV-1
  // - Storing data separately
  // - user syntax:
  //   u-<user_id>
  // - group syntax:
  //   g-<group_id>
  // - message syntax:
  // - m-<group_id>


  // Request all groups for the user
  requestAllUserGroups() {
    var session = sessionStorage.getItem("UserSession");
    
    this.service.requestData<GroupDTO[]>(JSON.stringify(session), '/GetGroups').subscribe(
      res => {
        console.log(res);

        res.forEach(element => {
          localStorage.setItem('g-' + element.Id, JSON.stringify(element))
        });
      },
      err => { 
        console.log(err); 
      }
    );
  }

  // Request last X messages of a specific group
  requestLastXMessagesForGroup(id: number) {
    var session = JSON.stringify(sessionStorage.getItem("UserSession"));

    this.service.requestData<MessageDTO[]>(JSON.stringify({session, id}), '/GetLastXMessages').subscribe(
      res => {
        console.log(res);
      },
      err => { 
        console.log(err); 
      }
    );
  }

  // Request user data for this group
  requestUserDataForGroup(id: number){
    var session = JSON.stringify(sessionStorage.getItem("UserSession"));

    this.service.requestData<UserDTO[]>(JSON.stringify({session, id}), '/GetUsersForGroup').subscribe(
      res => {
        console.log(res);
      },
      err => { 
        console.log(err); 
      }
    );
  }

  //------------------------------------------------------------//
  //
  // Access methods
  //
  // x
  */
}
