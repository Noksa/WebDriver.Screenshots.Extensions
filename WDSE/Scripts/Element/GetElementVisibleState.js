function GetVisibleState(y, bottom) {
	var  win = $(window);
	var  elementTop = Math.max(y, 0);
	var  elementBottom = Math.min(bottom, win.height());
	var  viewportTop = 0;
	var  viewportBottom = win.height();
	var  result = elementTop >= viewportTop &&
        elementTop < viewportBottom &&
        elementBottom <= viewportBottom &&
        elementBottom > viewportTop;
    return result;
}

return GetVisibleState(arguments[0], arguments[1]);