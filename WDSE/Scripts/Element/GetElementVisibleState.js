var ele = $(arguments[0]);
var win = $(window);
var elementTop = ele.offset().top;
var elementBottom = elementTop + ele.outerHeight();
var viewportTop = win.scrollTop();
var viewportBottom = viewportTop + win.height();
var res = ele.is(":visible");
return elementBottom > viewportTop && elementTop < viewportBottom && res;