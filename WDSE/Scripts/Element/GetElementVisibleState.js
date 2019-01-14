function GetVisibleState(y, bottom) {
    const win = $(window);
    const elementTop = Math.max(y, 0);
    const elementBottom = Math.min(bottom, win.height());
    const viewportTop = 0;
    const viewportBottom = win.height();
    const result = elementTop >= viewportTop &&
        elementTop < viewportBottom &&
        elementBottom <= viewportBottom &&
        elementBottom > viewportTop;
    return result;
}

return GetVisibleState(arguments[0], arguments[1]);