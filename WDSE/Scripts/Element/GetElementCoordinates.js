var ele = $(arguments[0]);
var left = parseInt(ele.offset().left);
var top = parseInt(ele.offset().top - $(window).scrollTop());
var right = parseInt(left + ele.outerWidth());
var bottom = parseInt(top + ele.outerHeight());
var x = left;
var y = top;
var str = JSON.stringify({
    x: x,
    y: y,
    width: right - x,
    height: bottom - y
});
return str;