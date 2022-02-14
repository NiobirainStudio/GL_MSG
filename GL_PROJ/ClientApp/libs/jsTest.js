$('document').ready(function(){
	
	function setMessageBarHeight() {
		var massageBar = $('.message_bar').first();
		var newHeight=$(window).height() - $(".right_header").height() - $(".response_bar").height();
		massageBar.height(newHeight);
	}
	function setResponseBarMarginLeft() {
		var responseBar = $('.response_bar').first();
		var leftColum = $('.left_main_colum').first();
		responseBar.css('margin-left' , leftColum.width())
	}
	//function setuserLastMessageBarWidth() {
	//	var userLastMessageBar = $('.userLastMessage_bar').first();
	//	var leftColum = $('.left_main_colum').first();
	//	userLastMessageBar.width(leftColum.width()-74);
	
	function setUserLastMessageBarWidth() {
			$('.userLastMessage_bar').each(function(){$(this).width($('.left_main_colum').first().width()-84)});
	}
	
	function setUserListHeight(){
		var userList = $('.userList_bar').first();
		userList.height($(window).height() - 100);
	}
	function setResponseBarHeight(){
		$('.messageInput_bar').first().height();
		$('.response_bar').first().height($('.messageInput_bar').first().height()+50);
	}

	function hideEmojiSendfileButtons(){
		if($(window).width()>700){
			$('.sendFile_btn').first().css('display' , 'inline-block');
			$('.emoji_btn').first().css('display' , 'inline-block');
		}else{
			$('.sendFile_btn').first().css('display' , 'none');
			$('.emoji_btn').first().css('display' , 'none');

		}
		
	}
	
	
	$('.messageInput_bar').keypress(function(event) {
    if (event.keyCode == 13) {
        event.preventDefault();
    }
	});
	
	
	function setRightWinowSize(){
		
		
		setResponseBarHeight();
		setUserListHeight();
		setMessageBarHeight();
		hideEmojiSendfileButtons();
		
		setResponseBarMarginLeft();
		setUserLastMessageBarWidth();
		$(window).scrollTop(0);
		
		
	}
	
	
	$('.messageInput_bar').resize(console.log('говно сработало!'));
	
	
	setRightWinowSize();
	$(window).scrollTop(0);
	$(".message_bar").niceScroll();
	$(".userList_bar").niceScroll();
	$(".messageInput_bar").niceScroll();
	document.getElementsByClassName('response_bar')[0].addEventListener("input", function() {setRightWinowSize()}, false);
	
	$(window).resize(function(){setRightWinowSize()});
	$('.messageInput_bar').resize(function(){console.log("залупа")});
	//$('.messageInput_bar').resize(function(){setRightWinowSize()});
});