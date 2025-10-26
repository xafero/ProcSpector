
function onProcessClick(s, e)
{
    let w = c.findWindow(e, "Memory");
    onWindowClick(s, w);
}

function onWindowClick(s, e)
{
    c.LogDebug("I've found [" + e.Title + "] ?!");
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
    c.addContextOption("window", "Do OCR on this", onWindowClick);
}

init();
