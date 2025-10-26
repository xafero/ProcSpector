
function onProcessClick(s, e)
{
    onWindowClick(s, c.findWindow(e, "Memory"));
}

function onWindowClick(s, e)
{
    c.DoOcr(c.screenShot(e), _root, "Training");
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
    c.addContextOption("window", "Do OCR on this", onWindowClick);
    return plugin.Root;
}

_root = init();
