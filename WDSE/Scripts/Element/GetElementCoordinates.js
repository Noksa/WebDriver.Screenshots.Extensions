var ele = $(arguments[0]);
var left = parseInt(ele.offset().left);
var top = parseInt(ele.offset().top);
var right = parseInt(left + ele.outerWidth());
var bottom = parseInt(top + ele.outerHeight());
var x = Math.max(left, 0);
var y = Math.max(top, 0);
var str = JSON.stringify({
    x: x,
    y: y,
    width: right - x,
    height: bottom - y
});
return str;