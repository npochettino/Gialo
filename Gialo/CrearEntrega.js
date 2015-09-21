function grillaTandaItem_OnEndCallBack(s, e) {
    if (s.cpHaceCallBack != undefined && (s.cpHaceCallBack == "true" || s.cpHaceCallBack == true)) {
        grillaTandaItemAgregados.PerformCallback();
        delete (s.cpHaceCallBack);
    }
}
function grillaTandaItemAgregados_OnEndCallBack(s, e) {
    if (s.cpHaceCallBack != undefined && (s.cpHaceCallBack == "true" || s.cpHaceCallBack == true)) {
        grillaTandaItem.PerformCallback();
        delete (s.cpHaceCallBack);
    }
}