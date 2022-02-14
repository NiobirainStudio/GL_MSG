declare var jQuery: any;
import { Component, Inject, OnInit } from '@angular/core';
import { ResizedEvent } from 'angular-resize-event';
import { MainService } from 'src/app/services/main.service';

import { DOCUMENT } from '@angular/common';
import { GroupDTO } from 'src/app/model/GroupDTO';
import { MessageDTO } from 'src/app/model/MessageDTO';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})


export class MainComponent implements OnInit {
  groupArray: Array<GroupDTO> = [];
  messageArray: Array<MessageDTO> = [];

  constructor(public service: MainService, @Inject(DOCUMENT) private document: Document) {}
  
  onKey(event: any): void {
    // keyCode for the Enter key is 13
    if (event.keyCode === 13) {
      console.log('enterPressed', event.target.innerText);
      event.preventDefault();
    }
  }
  
  ngOnInit() {
    // DreaD_ver's addition
    this.service.StartConnection();
    //this.service.OnReconnectedEvent();

    // Conncet to SignalR server groups
    setTimeout(() => { 
      this.service.StartGroupChannel(+(localStorage.getItem("UserSession") || -1));
    }, 1000);

    this.GetGroups();

    // Setup listeners
    this.service.ListenOnMessages(this.MessageCallbackFunction);



    (function ($) {
      $(document).ready(function () {
        //$('document').ready(function(){


        function setMessageBarHeight() {
          var massageBar = $('.message_bar').first();
          var newHeight = $(window).height() - $(".right_header").height() - $(".response_bar").height();
          massageBar.height(newHeight);
          //console.log(newHeight + " - высота меседжбара");
        }

        function setRightMainColumMarginLeft() {
          var responseBar = $('.right_main_colum').first();
          var leftColum = $('.left_main_colum').first();
          responseBar.css('margin-left', leftColum.width())
        }


        function setUserListHeight() {
          var userList = $('.contactList').first();
          userList.height($(window).height() - $('.right_header').first().height());
        }

        function setResponseBarHeight() {
          $('.response_input').first().height();
          $('.response_bar').first().height($('.response_input').first().height() + 50);
        }

        // will remove to mediaquery
        function hideEmojiSendfileButtons() {
          if ($(window).width() > 700) {
            $('.response_contentWrap_button_sendFile').first().css('display', 'inline-block');
            $('.response_contentWrap_button_emoji').first().css('display', 'inline-block');
          } else {
            $('.response_contentWrap_button_sendFile').first().css('display', 'none');
            $('.response_contentWrap_button_emoji').first().css('display', 'none');
          }
        }
        // will remove to mediaquery
        function hideRightHeaderAvatarBar() {
          if ($(window).width() > 700) {
            $('.right_header_avatar_bar').first().css('display', 'inline-block');
          } else {
            $('.right_header_avatar_bar').first().css('display', 'none');

          }
        }
        function RightHeaderName() {
          if ($(window).width() > 700) {
            $('.right_header_userOrGroupInfo_name').first().width($('.right_main_colum').first().width() / 2 - $('.right_header_avatar').first().width() - 100);
          } else {
            $('.right_header_userOrGroupInfo_name').first().width($('.right_main_colum').first().width() / 2);

          }
        }


        function setRightWinowSize() {

          hideRightHeaderAvatarBar();
          setResponseBarHeight();
          setUserListHeight();
          setMessageBarHeight();
          hideEmojiSendfileButtons();
          setRightMainColumMarginLeft();
          window.scroll(0, 0);
          RightHeaderName();
          //responseBarHeight();
          console.log("отработала функция калибровки размера окон");

        }



        setRightWinowSize();
        window.onresize = function (event) {
          setRightWinowSize();
          console.log('changed');
        };

        //$(".response_input").keypress(function () {
        //  setRightWinowSize();
        //  console.log("респонс инпут изменил размер");
        //})

      });
    })(jQuery);
  }

  onResized(event: ResizedEvent) {
    (function ($) {
      $(document).ready(function () {

        function setMessageBarHeight() {
          var massageBar = $('.message_bar').first();
          var newHeight = $(window).height() - $(".right_header").height() - $(".response_bar").height();
          massageBar.height(newHeight);

        }

        function setRightMainColumMarginLeft() {
          var responseBar = $('.right_main_colum').first();
          var leftColum = $('.left_main_colum').first();
          responseBar.css('margin-left', leftColum.width())
        }


        function setUserListHeight() {
          var userList = $('.contactList').first();
          userList.height($(window).height() - $('.right_header').first().height());
        }

        function setResponseBarHeight() {
          $('.response_input').first().height();
          $('.response_bar').first().height($('.response_input').first().height() + 50);
        }

        // will remove to mediaquery
        function hideEmojiSendfileButtons() {
          if ($(window).width() > 700) {
            $('.response_contentWrap_button_sendFile').first().css('display', 'inline-block');
            $('.response_contentWrap_button_emoji').first().css('display', 'inline-block');
          } else {
            $('.response_contentWrap_button_sendFile').first().css('display', 'none');
            $('.response_contentWrap_button_emoji').first().css('display', 'none');
          }
        }
        // will remove to mediaquery
        function hideRightHeaderAvatarBar() {
          if ($(window).width() > 700) {
            $('.right_header_avatar_bar').first().css('display', 'inline-block');
          } else {
            $('.right_header_avatar_bar').first().css('display', 'none');

          }
        }
        function RightHeaderName() {
          if ($(window).width() > 700) {
            $('.right_header_userOrGroupInfo_name').first().width($('.right_main_colum').first().width() / 2 - $('.right_header_avatar').first().width() - 100);
          } else {
            $('.right_header_userOrGroupInfo_name').first().width($('.right_main_colum').first().width() / 2);

          }
        }



        function setRightWinowSize() {

          hideRightHeaderAvatarBar();
          setResponseBarHeight();
          setUserListHeight();
          setMessageBarHeight();
          hideEmojiSendfileButtons();
          setRightMainColumMarginLeft();
          window.scroll(0, 0);
          RightHeaderName();

        }

        setRightWinowSize();

      });

    })(jQuery);

  }



  /// DreaD_ver's methods
  WriteMessage() {
    this.service.SendTextMessage(this.GetSession(), this.document.getElementById('newMessage')?.innerHTML || '', 1);
  }

  GetGroups() {
    this.service.PostAndRecieveData< { groups: GroupDTO[]} >(this.GetSession(), '/GetUserGroups').subscribe(
      res => {
        console.log(res);
        res.groups.forEach(element => {
          this.groupArray.push(element);
        });
      },
      err => { 
        console.log(err); 
      }
    );
  }

  GetMessages(group_id: number) {
    this.service.PostAndRecieveData< { messages: MessageDTO[]} >(group_id, '/GetGroupMessages').subscribe(
      res => {
        console.log(res);
        res.messages.forEach(element => {
          this.messageArray.push(element);
        });
      },
      err => { 
        console.log(err); 
      }
    );
  }

  private GetSession() {
    return (+(localStorage.getItem("UserSession") || -1));
  }

  public MessageCallbackFunction = (data: any): void => {
    console.log("Callback!");
    console.log(data);
  }
}





