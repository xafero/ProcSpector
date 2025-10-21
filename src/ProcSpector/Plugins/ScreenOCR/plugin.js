
function onProcessClick(s, e)
{
    var x = e.Name;
    c.LogDebug("I've found " + x + "!");
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
}

init();
