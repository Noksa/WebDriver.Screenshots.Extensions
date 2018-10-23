var ele = $(arguments[0]);

var elementTop = $(ele).offset().top;
var elementBottom = elementTop + $(ele).outerHeight();
var viewportTop = $(window).scrollTop();
var viewportBottom = viewportTop + $(window).height();
return elementBottom > viewportTop && elementTop < viewportBottom;