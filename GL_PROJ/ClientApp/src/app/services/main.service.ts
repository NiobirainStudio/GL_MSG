import { HttpClient, HttpHeaders} from '@angular/common/http'
import { Injectable, Type } from '@angular/core';

import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  public data: string;
  public broadcast_data: string;

  readonly URL = 'https://localhost:7047/Home';

  private hubConnection: signalR.HubConnection;

  constructor(private http: HttpClient) { }

  public StartConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl('https://localhost:7047/hub')
      .build();

      this.hubConnection
      .start()
      .then(() => { 
        console.log('Connection started!');
        this.OnConnectionStart();
      })
      .catch(err => console.log('Error while connecting to hub ' + err));
  }


  private OnConnectionStart() {
    // Setup listeners
    this.ListenOnMessages();


    // Conncet to SignalR server groups
    setTimeout(() => { this.StartGroupChannel(+(localStorage.getItem("UserSession") || -1)) }, 1000);
  }

  // Reconnect on losing connection
  public OnReconnectedEvent(){
    this.hubConnection.onreconnected(() => {
      console.log("Reconnected!");
      this.OnConnectionStart();
    });
  }

  // Listeners
  public ListenOnMessages() {
    this.hubConnection.on('GroupMessages', (data) => {
      console.log(data);
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

  public PostAndRecieveData(data: number, url: string) {
    return this.http.post<any>(
      this.URL + url,
      JSON.stringify(data),
      { headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }
}