
function onProcessClick(s, e)
{
    let w = c.findWindow(e, "Memory");
    onWindowClick(s, w);
}

function onWindowClick(s, e)
{
    c.logDebug("I've found [" + e.Title + "] ?!");
    
    let b = c.screenShot(e);
    c.logDebug(" ??? " + b.FullName + " ???");
    
    c.xxx(b, _root, "Training");
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
    c.addContextOption("window", "Do OCR on this", onWindowClick);
    return plugin.Root;
}

_root = init();
