$(document).ready(function() {

//if resize
	$( window ).on('resize load', function() {
		var w=window,d=document,e=d.documentElement,g=d.getElementsByTagName('body')[0],x=w.innerWidth||e.clientWidth||g.clientWidth,y=w.innerHeight||e.clientHeight||g.clientHeight;
		if (x < 960) {
			$('.wrapper').addClass('resized');
			mini();
		} else {
			$('.wrapper').removeClass('resized');
			mini();
		}
	});

//language choose
	$(document).on('click', '.for-active-lang', function() {
		$('.for-nonactive-lang').slideToggle();
		$('.arrow-mini').toggleClass('arrow-mini-active');
	});
	$(document).on('click', '.for-nonactive-lang .flag-item', function() {
		$('.for-nonactive-lang').slideToggle();
		$('.arrow-mini').toggleClass('arrow-mini-active');
		var forThis = $(this);
		setTimeout(function () {
			$('.for-active-lang .flag-item').appendTo('.for-nonactive-lang');
			forThis.prependTo('.for-active-lang');
		}, 200)
	});

//smooth scroll
	$(function() {
	  $('a[href*=#]:not([href=#])').click(function() {
	    if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
	      var target = $(this.hash);
	      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
	      if (target.length) {
	        $('html, body').animate({
	          scrollTop: target.offset().top - 44
	        }, 500);
	        return false;
	      }
	    }
	  });
	});

//accordion
	$('.more').click(function() {
		$(this).animate({
			opacity: '0'
		}, 100)
		$(this).next().slideDown();
	});

	$('.swiper-slide').each(function() {
    //set size
    var th = $(this).height(),//box height
        tw = $(this).width(),//box width
        im = $(this).children('img'),//image
        ih = im.height(),//inital image height
        iw = im.width();//initial image width
    if (ih>iw) {//if portrait
        im.addClass('ww').removeClass('wh');//set width 100%
    } else {//if landscape
        im.addClass('wh').removeClass('ww');//set height 100%
    }
    //set offset
    var nh = im.height(),//new image height
        nw = im.width(),//new image width
        hd = (nh-th)/2,//half dif img/box height
        wd = (nw-tw)/2;//half dif img/box width
    if (nh<nw) {//if portrait
        im.css({marginLeft: '-'+wd+'px', marginTop: 0});//offset left
    } else {//if landscape
        im.css({marginTop: '-'+hd+'px', marginLeft: 0});//offset top
    }
});

//offcanvas menu
	function mini() {
		if ($('.resized').length > 0) {
			$('nav').prependTo('body');
			$('nav').addClass('pushy pushy-left').css('display', 'block');
			$('.menu-btn').click(function() {
				$('nav').addClass('pushy pushy-left pushy-open pusshy');
			})
			$('.site-overlay, nav a').click(function() {
				if ($('.resized').length > 0) {
					$('nav').removeClass('pushy-open pusshy');
					$('.wrapper').removeClass('container-push');
					$('body').removeClass('pushy-active');
				}
			});
		} else {
			$('nav').attr('class', '').prependTo('header .page');
			$('.wrapper').removeClass('container-push');
			$('body').removeClass('pushy-active');
		}
	}

//input and textarea
   $('input,textarea').focus(function(){
       $(this).data('placeholder',$(this).attr('placeholder'))
       $(this).attr('placeholder','');
    });
    $('input,textarea').blur(function(){
       $(this).attr('placeholder',$(this).data('placeholder'));
    });

//scroll to top
	$('.arrow-to-top').click(function(){
		$('html, body').animate({scrollTop : 0}, 500);
	});

//end of ready function
});

$(window).load(function() {

//end of load function
});

