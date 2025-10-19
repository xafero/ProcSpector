
function onProcessClick(s, e)
{
    c.LogDebug(' # ' + s + ' # ' + e + ' # ');
}

function init()
{
    c.addContextOption("process", "Do OCR on this", onProcessClick);
}

init();
