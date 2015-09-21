/*global $*/
$(function () {
	$("#messenger .head").click(function () {
		$("#messenger .body").slideToggle();
	});


	// Declare a proxy to reference the hub.
	var chat = $.connection.chatHub;
	// Create a function that the hub can call to broadcast messages.
	chat.client.broadcastMessage = function (name, message) {
		// Html encode display name and message.
		var encodedName = $('<div />').text(name).html(),
			encodedMsg = $('<div />').text(message).html(),

		// Add the message to the page.
			template = '<div class="message-item">';
		template +=	'<div class="image">';
		template +=	'<img src="/Content/Images/profile-img.png" alt="Avatar">';
		template +=	'</div><div class="message">';
		template +=	'<strong>' + encodedName + ' :</strong>';
		template +=	encodedMsg + '</div></div>';

		$('.message-area').append(template);

	};

	// Start the connection.
	$.connection.hub.start().done(function () {
		$('#send').click(function () {
			if ($('#message').val() !== '') {
				chat.server.send($('#username').val(), $('#message').val());
				$('#message').val('').focus();
			}
		});
	});
});
