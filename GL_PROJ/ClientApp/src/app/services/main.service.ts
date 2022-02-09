import { HttpClient, HttpHeaders} from '@angular/common/http'
import { Injectable, Type } from '@angular/core';

import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  public data: string;
  public broadcast_data: string;

  private hubConnection: signalR.HubConnection;

  public startConnectionPoke() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7047/hub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while connecting to hub ' + err));
  }

  public addBroadcastListener() {
    this.hubConnection.on('GroupGlobal', (data) => {
      console.log(data);
    });
  }

  public WriteMessage() {
    this.hubConnection.invoke('PostMessage', 'session!', 'message_text', 1, 1)
      .catch(err => console.log(err));
  }

  public AddToGroups() {
    this.hubConnection.invoke('AddToGroups', 'session!')
      .catch(err => console.log(err));
  }


  constructor(private http:HttpClient) { }

  /*
  // Request and send data to server
  requestData<T>(data: any, url: string) {
    return this.http.post<T>(
      this.URL + url,
      data,
      { headers: new HttpHeaders({
      'Content-Type': 'application/json'
      }) 
    });
  }
  */


}
