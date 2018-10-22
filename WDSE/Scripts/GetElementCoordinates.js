var el = $(arguments[0]);
var left = parseInt(el.offset().left);
var top = parseInt(el.offset().top);
var right = parseInt(left + el.outerWidth());
var bottom = parseInt(top + el.outerHeight());
const x = Math.max(left, 0);
const y = Math.max(top, 0);
var str = JSON.stringify({
    x: x,
    y: y,
    width: right - x,
    height: bottom - y
});
return str;