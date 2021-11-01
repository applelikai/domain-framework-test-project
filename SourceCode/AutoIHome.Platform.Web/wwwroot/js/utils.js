/// <reference path="../lib/jquery/jquery.js" />
/// <reference path="../lib/layui/layui.js" />

//获取table下面所有的选中项id
function get_checked_ids(table) {
    //初始化id数组
    var dIds = [];
    //遍历添加id
    $(table).find('tbody > tr > td > :checked').each(function (index, checkbox) {
        dIds.push(checkbox.value);
    });
    //返回所有的选中项id
    return dIds;
}

//选择或取消全部
function select_or_cancel_all(checkbox) {
    $(checkbox).parents('table').find('tbody input[type="checkbox"]').each(function (index, chkInput) {
        chkInput.checked = checkbox.checked;
    });
}

//获取表单数据json对象
function get_form_json(formSelector) {
    //获取表单
    let form = get_html_element(formSelector);
    //创建数据对象
    let data = new Object();
    //遍历表单元素并填充值
    Array.from(form.elements).forEach(function (element) {
        //赋值
        data[element.name] = element.value;
    });
    //获取数据对象
    return data;
}
//获取表单urlencoded字符串(key1=value1&key2=value2)
function get_form_urlencoded_string(formSelector) {
    //获取表单
    let form = get_html_element(formSelector);
    //获取urlencoded字符串(key1=value1&key2=value2)
    return Array.from(form.elements).map(function (element) {
        //若value为字符串,则value进行encode转码
        if (typeof (element.value) === 'string') {
            //获取key=value字符串
            return element.name + '=' + encodeURI(element.value);
        }
        //获取key=value字符串
        return element.name + '=' + element.value;
    }).join('&');
}

//返回
function go_back(divContainer) {
    //若存在悬停框则直接销毁
    divContainer.querySelectorAll('input[data-bs-toggle="popover"]').forEach(function (inputPopover) {
        //获取悬停框对象
        let oPopover = bootstrap.Popover.getInstance(inputPopover);
        //若悬停框存在则销毁
        if (oPopover != undefined)
            oPopover.dispose();
    });
    //收起当前容器
    if (!divContainer.classList.contains('d-none'))
        divContainer.classList.add('d-none');
    //清空当前容器内的内容
    divContainer.querySelector('.modal-header').innerHTML = '';
    divContainer.querySelector('.modal-body').innerHTML = '';
    divContainer.querySelector('.modal-footer').innerHTML = '';
    //打开上一个同级容器容器
    let sPrev = divContainer.getAttribute('data-prev');                                     //获取上级容器名称
    let divPrev = divContainer.parentNode.querySelector('div[data-name="' + sPrev + '"]');  //获取上级容器
    if (divPrev.classList.contains('d-none'))
        divPrev.classList.remove('d-none');
}
//关闭模态框
function close_dialog(divDialog) {
    //若存在悬停框则直接销毁
    divDialog.querySelectorAll('input[data-bs-toggle="popover"]').forEach(function (inputPopover) {
        //获取悬停框对象
        let oPopover = bootstrap.Popover.getInstance(inputPopover);
        //若悬停框存在则销毁
        if (oPopover != undefined)
            oPopover.dispose();
    });
    //关闭当前模态框
    bootstrap.Modal.getInstance(divDialog).hide();
    //清空当前容器内的内容
    divDialog.querySelector('.modal-header').innerHTML = '';
    divDialog.querySelector('.modal-body').innerHTML = '';
    divDialog.querySelector('.modal-footer').innerHTML = '';
}