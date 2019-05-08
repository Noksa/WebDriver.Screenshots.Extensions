function GetVisibleState(y, bottom) {
	var elementTop = y;
	var elementBottom = bottom;
	var  viewportTop = 0;
	var  viewportBottom = window.innerHeight;
	var  result = elementTop >= viewportTop &&
        elementTop < viewportBottom &&
        elementBottom <= viewportBottom &&
        elementBottom > viewportTop;
    return result;
}

return GetVisibleState(arguments[0], arguments[1]);