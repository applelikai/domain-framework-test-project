
//jquery扩展方法
(function ($) {
    //序列化表单为对象
    $.fn.serializeObject = function () {
        var oModel = {};
        $(this).serializeArray().forEach(function (oItem) {
            //若当前对象已包含当前属性
            if (oModel.hasOwnProperty(oItem.name)) {
                //当前属性变为数组并添加新值
                oModel[oItem.name] = [oItem.value].concat(oModel[oItem.name]);
            } else {
                //若不包含,则添加属性添加值
                oModel[oItem.name] = oItem.value;
            }
        });
        return oModel;
    };
})(jQuery);