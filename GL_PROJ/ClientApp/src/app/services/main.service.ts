import { HttpClient, HttpHeaders} from '@angular/common/http'
import { Injectable } from '@angular/core';

import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  readonly URL = 'https://localhost:7047/Home';

  private hubConnection: signalR.HubConnection;

  constructor(private http: HttpClient) { }

  public StartConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      //.withAutomaticReconnect()
      //.configureLogging(signalR.LogLevel.Information)
      .withUrl('https://localhost:7047/hub')
      .build();

      this.hubConnection
      .start()
      .then(() => { 
        console.log('Connection started!');
      })
      .catch(err => console.log('Error while connecting to hub ' + err));
  }

  /*
  // Reconnect on losing connection
  public OnReconnectedEvent(){
    this.hubConnection.onreconnected(() => {
      console.log("Reconnected!");
      this.OnConnectionStart();
    });
  }*/

  // Listeners
  public ListenOnMessages(callbackFunction: (args: any) => void) {
    this.hubConnection.on('GroupMessages', (data) => {
      callbackFunction(data);
    });
  }


  // Join SignalR channels
  public StartGroupChannel(user_id: number) {
    this.hubConnection.invoke('AddToGroups', user_id)
      .catch(err => console.log("StartGroupChannel " + err));
  }


  // Send data
  public SendTextMessage(user_id: number, message_data: string, group_id: number) {
    this.hubConnection.invoke('PostMessage', user_id, message_data, 1, group_id)
      .catch(err => console.log("SendMessage " + err));
  }

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

  public PostAndRecieveData<T>(data: number, url: string) {
    return this.http.post<T>(
      this.URL + url,
      JSON.stringify(data),
      { headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  public EEE(){
    console.log("EEE");
  }
}
