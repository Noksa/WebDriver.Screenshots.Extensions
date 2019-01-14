function GetCoords(element) {
    const coords = $(element)[0].getBoundingClientRect();
    const left = parseInt(coords.left);
    const top = parseInt(coords.top);
    const width = parseInt(coords.width);
    const height = parseInt(coords.height);
    const bottom = parseInt(coords.bottom);
    const x = left;
    const y = top;
    const str = JSON.stringify({
        x: x,
        y: y,
        width: width,
        height: height,
        bottom: bottom
    });
    return str;
}

return GetCoords($(arguments[0]));