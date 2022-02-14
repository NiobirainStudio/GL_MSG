import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-my-message',
  templateUrl: './my-message.component.html',
  styleUrls: ['./my-message.component.css']
})
export class MyMessageComponent implements OnInit {
  @Input() messageData:any;
 
  constructor() { }

  ngOnInit(): void {
  }
}
