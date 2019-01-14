function GetCoords(element) {
    var coords = $(element)[0].getBoundingClientRect();
    var left = parseInt(coords.left);
    var top = parseInt(coords.top);
    var width = parseInt(coords.width);
    var height = parseInt(coords.height);
    const x = Math.max(left, 0);
    const y = Math.max(top, 0);
    var str = JSON.stringify({
        x: x,
        y: y,
        width: width,
        height: height
    });
    return str;
}

return GetCoords($(arguments[0]));