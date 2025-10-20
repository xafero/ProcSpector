
function onProcessClick(s, e)
{
    var x = c.GetFirstProcess('notepad++');
    c.LogDebug(x);
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
}

init();
