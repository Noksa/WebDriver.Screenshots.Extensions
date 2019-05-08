function GetVisibleState(y, bottom) {
	var elementTop = y;
	var elementBottom = bottom;
	var  viewportTop = $(window).scrollTop();
	var  viewportBottom = $(window).scrollTop() + window.innerHeight;
	var result;
    if (elementTop >= 0) {
	    result = elementTop < viewportBottom;
    } else {
	    result = elementBottom > 0;
    }

    return result;
}

return GetVisibleState(arguments[0], arguments[1]);