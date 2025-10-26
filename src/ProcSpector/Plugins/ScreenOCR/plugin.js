
function onProcessClick(s, e)
{
    var x = e.Name;
    c.LogDebug("I've found " + x + "!");

    var y = c.findWindow(e, "Memory");
    c.LogDebug("I've found " + y.Title + "!");
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
}

init();
