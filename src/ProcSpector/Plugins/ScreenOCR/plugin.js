
function onProcessClick(s, e)
{
    let sta = c.findWindow(e, "Execution Status");
    let mem = c.findWindow(e, "Memory");

    while (true)
    {
        c.activate(sta);
        c.sleep("ms", 10);
        let img = c.screenShot(mem);
        c.doOcr(img, _root, "Training");

        c.activate(mem);
        c.sleep("ms", 10);
        c.sendKey(mem, "press", "next");
    }
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
    return plugin.Root;
}

_root = init();
