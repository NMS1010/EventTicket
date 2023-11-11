/* Copy to Clipboard */
function copyToClipboardFF(text) {
	window.prompt("Copy to clipboard: Ctrl C, Enter", text);
}

function copyToClipboard(inputId) {
var input = $(inputId);
	var success = true,
			range = document.createRange(),
			selection;
	// For IE.
	if (window.clipboardData) {
		window.clipboardData.setData("Text", input.val());
	} else {
		// Create a temporary element off screen.
		var tmpElem = $('<div>');
		tmpElem.css({
			position: "absolute",
			left: "-1000px",
			top: "-1000px",
		});
		// Add the input value to the temp element.
		tmpElem.text(input.val());
		$("body").append(tmpElem);
		// Select temp element.
		range.selectNodeContents(tmpElem.get(0));
		selection = window.getSelection();
		selection.removeAllRanges();
		selection.addRange(range);
		// Lets copy.
		try {
			success = document.execCommand("copy", false, null);
		}
		catch (e) {
			copyToClipboardFF(input.val());
		}
		if (success) {
			/* Alert the copied text */
			Toast.fire({
				icon: 'success',
				title: 'Copied Link to Clipboard'
			})
			// remove temp element.
			tmpElem.remove();
		}
	}
}
/* End Copy to Clipboard */

/* Barren custom JS Code */
/*--- Tooltip Widget ---*/
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
  return new bootstrap.Tooltip(tooltipTriggerEl)
})

/*--- Bookmark Event ---*/
jQuery(document).ready(function($) {
	$('.bookmark-icon, .bookmark-button').on('click', function(e) {
		e.preventDefault();
		$(this).toggleClass('bookmarked');
		$(this).children('.bookmark-icon').toggleClass('bookmarked');
	});
	$(".simplefavorites-clear").addClass("co-main-btn co-btn-width min-width d-inline-block h_40");
});

/*--- Owl Sliders ---*/

// Engaging Online and Venue Events Slider
jQuery(document).ready(function($) {
	$('.engaging-slider').owlCarousel({
		items:5,
		loop:true,
		margin:20,
		nav:true,
		dots:true,
		navText: ["<i class='uil uil-angle-left'></i>", "<i class='uil uil-angle-right'></i>"],
		smartSpeed:800,
		autoplay:true,
		autoplayTimeout:3000,
		autoplayHoverPause:true,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:1
			},
			1000:{
				items:2
			},
			1200:{
				items:3
			},
			1400:{
				items:3
			}
		}
	});

	// Testimonial Slider
	$('.testimonial-slider').owlCarousel({
		items:10,
		loop:true,
		margin:20,
		nav:false,
		dots:true,
		smartSpeed:800,
		autoplay:true,
		autoplayTimeout:3000,
		autoplayHoverPause:true,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:1
			},
			1000:{
				items:2
			},
			1200:{
				items:2
			},
			1400:{
				items:2
			}
		}
	});

	// Organisations Slider
	$('.organisations-slider').owlCarousel({
		items:7,
		loop:true,
		margin:20,
		nav:false,
		dots:false,
		smartSpeed:800,
		autoplay:true,
		autoplayTimeout:3000,
		autoplayHoverPause:true,
		responsive:{
			0:{
				items:2
			},
			600:{
				items:2
			},
			1000:{
				items:3
			},
			1200:{
				items:4
			},
			1400:{
				items:5
			}
		}
	});

	// More Events Slider
	$('.moreEvents-slider').owlCarousel({
		items:7,
		loop:true,
		margin:20,
		nav:true,
		dots:false,
		navText: ["<i class='uil uil-angle-left'></i>", "<i class='uil uil-angle-right'></i>"],
		responsive:{
			0:{
				items:1
			},
			600:{
				items:2
			},
			800:{
				items:2
			},
			1000:{
				items:3
			},
			1200:{
				items:4
			},
			1400:{
				items:4
			}
		}
	});

	// Most Posts Slider
	$('.most-posts-slider').owlCarousel({
		items:1,
		loop:true,
		margin:20,
		nav:false,
		dots:true,
		smartSpeed:800,
		autoplay:true,
		autoplayTimeout:3000,
		autoplayHoverPause:true,
		responsive:{
			0:{
				items:1
			},
			600:{
				items:1
			},
			800:{
				items:1
			},
			1000:{
				items:1
			},
			1200:{
				items:1
			},
			1400:{
				items:1
			}
		}
	});

	// Related Posts Slider
	$('.related-posts-slider').owlCarousel({
		items:4,
		loop:true,
		margin:20,
		nav:true,
		dots:false,
		navText: ["<i class='uil uil-angle-left'></i>", "<i class='uil uil-angle-right'></i>"],
		responsive:{
			0:{
				items:1
			},
			600:{
				items:2
			},
			800:{
				items:2
			},
			1000:{
				items:3
			},
			1200:{
				items:3
			},
			1400:{
				items:4
			}
		}
	});
});

/*--- Multi Dropdown JS ---*/ 

jQuery(document).ready(function(){
  jQuery('.dropdown-submenu a.submenu-item').on("click", function(e){
    jQuery(this).next('ul').toggle();
    e.stopPropagation();
    e.preventDefault();
  });
});


/*--- Multi Dropdown JS ---*/ 
jQuery(document).ready(function(){
    jQuery('input[type="radio"]').click(function(){
        var inputValue = $(this).attr("value");
        var targetBox = $("." + inputValue);
        jQuery(".event-box").not(targetBox).hide();
        jQuery(targetBox).show();
    });
});

/* SanSuKien.com custom JS Code */
/* Infinite scroll on events page */
jQuery(document).ready(function($) {
	if ( $( '.next.page-numbers').length ) { 
		$('.events-list-container').infiniteScroll({
			path: '.next.page-numbers',
			append: '.events-list-item',
			status: '.page-load-status',
			history: false,
			hideNav: '.em-pagination'
		});
	}
	else{
		$( '.infinite-scroll-request' ).hide();
		$( '.infinite-scroll-error' ).hide();
	}
});

/* Fixed Scroll Ads */
jQuery(document).ready(function() {
	$(window).on("resize", function (e) {
		checkScreenSize();
	});
	checkScreenSize();
	function checkScreenSize(){
		if(window.matchMedia("(max-width: 991px)").matches){
			// The viewport is less than 992 pixels wide
			//alert("This is a mobile device.");
			$('.scrolldesktop').removeClass('scrollstop');
			$('.scrollmobile').addClass('scrollstop');
			if ( $('#fsticky').length ) {
				$('.footer-copyright-text').addClass('mb-5');
			}
		} else{
			// The viewport is at least 992 pixels wide
			//alert("This is a tablet or desktop.");
			if ($("#scrolldesktop").hasClass("scrollstop")) {
				$('.scrollmobile').removeClass('scrollstop');
			} else {
				$('.scrollmobile').removeClass('scrollstop');
				$('.scrolldesktop').addClass('scrollstop');
			}
			if ( $('#fsticky').length ) {
				$('.footer-copyright-text').removeClass('mb-5');
			}
		}
	}
	
	jQuery('#scrollitem').scrollToFixed( { 
		marginTop: 85,
		removeOffsets: true,
		unfixed: function() {
			jQuery('#scrollitem').css('position', 'relative');
		},
		fixed: function() { jQuery(this).find('.add-card').addClass('main-card'); },
		unfixed: function() { jQuery(this).find('.add-card').removeClass('main-card'); },
		/* postAbsolute: function() { jQuery('#scrollitem').removeClass('d-none'); }, */
		/* preAbsolute: function() { jQuery('#scrollitem').hide(); }, */
		limit: function() {
			var limit = jQuery('.scrollstop').offset().top -  jQuery('#scrollitem').outerHeight(true) - 10;
			return limit;
		}
	});
});

/* Ads Vip Scrolling */
jQuery(document).ready(function(jQuery){
	(function(nicescroll){
		if( ! nicescroll ){
			return;
		}

		var temp = jQuery('<div />'),
			adsvip_width = jQuery('#adsvip').outerWidth(),
			adsvip_item_width = 0,
			scale,
			item_width,
			body_width = jQuery('body').width();

		if( body_width <= 480 || adsvip_width <= 480 ){
			scale = 2;
		}
		else if( (body_width > 480 && body_width <= 992) || (adsvip_width > 480 && adsvip_width <= 992) ){
			scale = 3;
		}
		else{
			scale = 4;
		}

		// Get adv item width
		item_width = parseInt(jQuery('#adsvip').innerWidth()/scale);

		jQuery('#adsvip-item > li').each(function(){
			jQuery(this).css({
				'width' : item_width,
				'max-width' : 'none',
			}).clone().appendTo(temp);
			adsvip_item_width += jQuery(this).outerWidth() + 4;

			//jQuery(this).find('.img-responsive').css('height', '104px');
		});

		temp.appendTo('body').ready(function(){
			jQuery('#adsvip-item').css('width', adsvip_item_width);
			var nicescroll = jQuery('#adsvip').niceScroll({
				cursorcolor	: "#888",
				cursorwidth : '6px',
				cursorborder: 'none',
				overflowy	: false,
				overflowx	: true
			});
			temp.remove();
		});
	})(jQuery.nicescroll);
});

/*--- Show Hide Password with EYE Icon in TextBox ---*/
jQuery(document).ready(function($) {
	$(function () {
		$(".pass-show-eye").click(function () {
			 if ($("#password").attr("type") == "password")
			{
				//Change type attribute
				$("#password").attr("type", "text");
				$(this).html('<i class="fas fa-eye"></i>');
			} else
			{
				//Change type attribute
				$("#password").attr("type", "password");
				$(this).html('<i class="fas fa-eye-slash"></i>');
			}
		});
		
		$(".pass-show-eye1").click(function () {
			 if ($("#pass1").attr("type") == "password")
			{
				//Change type attribute
				$("#pass1").attr("type", "text");
				$(this).html('<i class="fas fa-eye"></i>');
			} else
			{
				//Change type attribute
				$("#pass1").attr("type", "password");
				$(this).html('<i class="fas fa-eye-slash"></i>');
			}
		});
		
		$(".pass-show-eye2").click(function () {
			 if ($("#pass2").attr("type") == "password")
			{
				//Change type attribute
				$("#pass2").attr("type", "text");
				$(this).html('<i class="fas fa-eye"></i>');
			} else
			{
				//Change type attribute
				$("#pass2").attr("type", "password");
				$(this).html('<i class="fas fa-eye-slash"></i>');
			}
		});
	});
});

function closeFPopup(){
	jQuery('#fpopup').hide('slow');
}

/* Calculate Service Price */
jQuery(document).ready( function($){
	$("#payment-type input").on("change", calcualtePrice);
	$("#payment-fee-type input").on("change", calcualtePrice);
	$("#per-ticket-price").on("change", calcualtePrice);
	$("#ticket-no").on("change", calcualtePrice);
	
	function calcualtePrice(){
		var total_receive = 0;
		var per_ticket_price_after = 0;
		var total_service_fee = 0;
		
		var payment_fee = $('input[name=payment_type]:checked', '#payment-type').val();
		var payment_fee_type = $('input[name=payment_fee_type]:checked', '#payment-fee-type').val();
		var per_ticket_price = $("#per-ticket-price").val();
		var ticket_no = $("#ticket-no").val();
		var svcharge = ssk_scripts.fixedfee;
		if ( per_ticket_price > 0 && ticket_no > 0 ) {
			var per_ticket_fee = Math.round(parseInt((per_ticket_price)) * parseInt(payment_fee) / 100) + parseInt(svcharge);
			var total_service_fee = parseInt((per_ticket_fee)) * parseInt(ticket_no);
			// var total_receive = parseInt(per_ticket_price) * parseInt(ticket_no);
			
			if ( payment_fee_type === 'pass-onto-attendees' ) {
				var per_ticket_price_after = parseInt((per_ticket_price)) + parseInt(per_ticket_fee);
			} else if ( payment_fee_type === 'absorb-the-fee' ) {
				var per_ticket_price_after = per_ticket_price;
			}
		
		// var total_receive = parseInt((per_ticket_price_after)) * parseInt(ticket_no);
		}
		
		$("#per-ticket-receive").html(per_ticket_price.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,'));
		$("#per-ticket-price-after").html(per_ticket_price_after.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,'));
		$("#total-service-fee").html(total_service_fee.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,'));
	}
});

/* Cf7 Send Information */
document.addEventListener( 'wpcf7submit', function( event ) {
	var status = event.detail.status;  
	console.log(status);  
	//if( status === 'validation_failed'){
		jQuery('.wpcf7-submit').val("Send");
	//}    
}, false );

jQuery('.wpcf7-submit').on('click',function(){
	jQuery(this).val("Submitting....");
});