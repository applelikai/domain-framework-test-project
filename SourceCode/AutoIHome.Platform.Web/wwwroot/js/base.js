
//获取XMLHttpRequest对象
function get_http_request() {
    //默认主流浏览器
    if (window.XMLHttpRequest)
        return new XMLHttpRequest();
    //老版本的IE浏览器
    else if (window.ActiveXObject)
        return new ActiveXObject('Microsoft.XMLHTTP');
    //浏览器不支持XMLHTTP
    throw new DOMException('Your browser does not support XMLHTTP.');
}

//拼接key=value字符串数组
function append_urlencoded_items_object(aUrlEncodedItems, key, data) {
    //遍历key值
    for (sKey in data) {
        //添加字符串数组
        append_urlencoded_items_value(aUrlEncodedItems, key + '.' + sKey, data[sKey]);
    }
}
//拼接key=value字符串数组
function append_urlencoded_items_array_item(aUrlEncodedItems, key, index, data) {
    //初始化字符串
    let sFirst = key + '[' + index.toString() + ']' + '.';
    //遍历key值
    for (sKey in data) {
        //添加字符串数组
        append_urlencoded_items_value(aUrlEncodedItems, sFirst + sKey, data[sKey]);
    }
}
//拼接key=value字符串数组
function append_urlencoded_items_value(aUrlEncodedItems, key, value) {
    switch (typeof (value)) {
        //若当前项目为对象，则解析对象并拼接到目标字符串
        case 'object':
            //若是数组
            if (Array.isArray(value)) {
                //遍历每一项
                value.forEach(function (item, index) {
                    //若item为object
                    if (typeof (item) === 'object') {
                        append_urlencoded_items_array_item(aUrlEncodedItems, key, index, item);
                    } else {
                        //添加到字符串数组
                        append_urlencoded_items_value(aUrlEncodedItems, key, item);
                    }
                });
            } else {
                //若不是数组，则解析value对象并添加到字符串数组
                append_urlencoded_items_object(aUrlEncodedItems, key, value)
            }
            break;
        //若为字符串
        case 'string':
            //拼接key=value
            aUrlEncodedItems.push(key + '=' + encodeURI(value));
            break;
        //默认
        default:
            //拼接key=value
            aUrlEncodedItems.push(key + '=' + value.toString());
            break;
    }
}
//获取key=value&数据格式字符串
function to_urlencoded_data_string(data) {
    //初始化urlencoded字符串数组
    let aUrlEncodedItems = [];
    //遍历key值
    for (key in data) {
        //添加字符串数组
        append_urlencoded_items_value(aUrlEncodedItems, key, data[key]);
    }
    //获取表单字符串
    return aUrlEncodedItems.join('&');
}
//获取key=value&数据格式字符串
function get_urlencoded_string(data) {
    //若data为字符串,则直接获取
    if (typeof (data) === 'string')
        return data;
    //解析获取urlencoded字符串
    return to_urlencoded_data_string(data);
}

//获取dom对象
function get_html_element(selector) {
    //判断对象类型
    switch (typeof selector) {
        //若为字符串,则作为选择器获取html对象
        case 'string':
            return document.querySelector(selector);
        //若为对象,则直接作为html对象返回
        case 'object':
            return selector;
        //默认
        default:
            return undefined;
    }
}
//获取加载html
function get_loading_html() {
    return '<i class="fa fa-2x fa-spinner fa-spin text-primary"></i>';
    //return '<i class="layui-icon layui-icon-loading layui-anim layui-anim-rotate layui-anim-loop" style="font-size:30px;"></i>';
}

//清空表单数据
function clean_form_data(form) {
    //遍历所有的input并清空
    form.querySelectorAll('input').forEach(function (input) {
        input.value = '';
    });
}
//清空表单临时查询时输入的值
function clean_form_search(form) {
    //遍历所有的需要查询选择值的输入框
    form.querySelectorAll('input[data-bs-toggle="popover"][data-id-field]').forEach(function (inputPopover) {
        //获取当前输入框字段对应的id字段
        let sIdField = inputPopover.getAttribute('data-id-field');
        //获取id字段的值
        let sId = form.querySelector('input[name="' + sIdField + '"]').value;
        //若id字段为空，说明当前输入框的值只是查询用的，并非选择后的值，则清空当前输入框
        if (sId.length == 0)
            inputPopover.value = '';
    });
}