
function onProcessClick(s, e)
{
    let w = ctx.findWindow(e, "Memory");
    onWindowClick(s, w);
}

function onWindowClick(s, e)
{
    ctx.logDebug("I've found [" + e.Title + "] ?!");
    
    let b = c.screenShot(e);
    ctx.logDebug(" ??? " + b.FullName + " ???");
}

function init()
{
    ctx.logDebug(" ??? " + plugin.Root + " ???");
    
    ctx.addContextOption("process", "Do OCR on this", onProcessClick);
    ctx.addContextOption("window", "Do OCR on this", onWindowClick);
}

init();
