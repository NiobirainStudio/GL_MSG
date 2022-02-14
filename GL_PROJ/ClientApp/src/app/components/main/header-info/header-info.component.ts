import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-header-info',
  templateUrl: './header-info.component.html',
  styleUrls: ['./header-info.component.css']
})
export class HeaderInfoComponent implements OnInit {



   constructor() { }
   name:string = "";

   subscription?: Subscription;

   
   ngOnInit() {
     //this.subscription = this.data.currentEventMessage.subscribe(message => this.name = message.name)
   }
   
   ngOnDestroy() {
    if(this.subscription != undefined)
     this.subscription.unsubscribe();
   }
   friendOrGroupName: string = "";
   friendOrGroupStatus: string = "";
  @Input() friendOrGroupData: any = {name: 'null', id: 42};



}
