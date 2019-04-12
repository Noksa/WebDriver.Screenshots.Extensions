function GetElement(byStr) {
    var res = "";
    if (byStr.startsWith("By.XPath: ")) {
        res = byStr.replace("By.XPath: ", "");
        res = res.replace("'", "\'");
        return document.evaluate(res, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null).snapshotItem(0);
    } else if (byStr.startsWith("By.Id: ")) {
        res = byStr.replace("By.Id: ", "");
        return document.getElementById(res);
    } else if (byStr.startsWith("By.TagName: ")) {
        res = byStr.replace("By.TagName: ", "");
        return document.getElementsByTagName(res)[0];
    } else if (byStr.startsWith("By.Name: ")) {
        res = byStr.replace("By.Name: ", "");
        return document.getElementsByName(res)[0];
    } else if (byStr.startsWith("By.ClassName[Contains]: ")) {
        res = byStr.replace("By.ClassName[Contains]: ", "");
        return document.getElementsByClassName(res)[0];
    }


}

return GetElement(arguments[0]);