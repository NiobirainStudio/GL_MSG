import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http'
import { LogRegDTO } from '../models/LogRegUser';

@Injectable({
  providedIn: 'root'
})
export class LogRegService {

  readonly URL = 'https://localhost:7047/Home';

  constructor(private http:HttpClient) { }

  // Post login data to server
  postLogRegData(data: LogRegDTO, url: string) {
    return this.http.post<any>(this.URL + url, data);
  }

  // Check if session exists in the server database
  checkSession(session: string) {
    return this.http.post<boolean>(
      this.URL + '/CheckSession',
      JSON.stringify(session),
      { headers: new HttpHeaders({
      'Content-Type': 'application/json'
      }) 
    });
  }
}
