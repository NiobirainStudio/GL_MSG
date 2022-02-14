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
//function responseBarHeight(){
//	$(".response_bar").first().height($(".response_input").first().height() + 20);
//}


// discart standard ENTER behavior
$('.response_input').trigger(function (event) {
	if (event.which == 13) {
		event.preventDefault();
	}
});

// add placeholder
//jQuery(function($){
//	$(".test").focusout(function(){
//		var element = $(this);        
//		if (!element.text().replace(" ", "").length) {
//			element.empty();
//		}
//	});
//});
function bodyHeight() {

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



}




setRightWinowSize();

//$(".message_bar").niceScroll();
//$(".userList_bar").niceScroll();
//$(".response_input").niceScroll();
//$("response_bar").first().addEventListener("input", function() {setRightWinowSize()}, false);

//$(window).resize(function(){setRightWinowSize()});
window.onresize = function (event) {
	setRightWinowSize();
	console.log('changed');
};
	//});
