/// <reference path="../lib/jquery/jquery.js" />
/// <reference path="../lib/layui/layui.js" />

//获取加载html
function get_loading_html() {
    return '<i class="fa fa-2x fa-spinner fa-spin text-primary"></i>';
}
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

//设置日期选择
function set_date_picker(element) {
    layui.use('laydate', function () {
        //获取laydate控件
        var laydate = layui.laydate;
        //加载
        $(element).each(function (index, input) {
            laydate.render({
                elem: input
            });
        });
    });
}
//设置日期及具体时间选择
function set_datetime_picker(element) {
    layui.use('laydate', function () {
        //获取laydate控件
        var laydate = layui.laydate;
        //加载
        $(element).each(function (index, input) {
            laydate.render({
                elem: input,
                type: 'datetime'
            });
        });
    });
}

//选择或取消全部
function select_or_cancel_all(checkbox) {
    $(checkbox).parents('table').find('tbody input[type="checkbox"]').each(function (index, chkInput) {
        chkInput.checked = checkbox.checked;
    });
}
//加载table
function load_table(table, nColspan) {
    //获取加载字符串
    var sLoadingHtml = '<tr><td colspan="' + nColspan + '"><i class="fa fa-2x fa-spinner fa-spin text-primary"></i></td></tr>';
    //加载tbody
    $(table).find('tbody').html(sLoadingHtml);
    //加载tbody
    $(table).find('tfoot').html(sLoadingHtml);
}

//返回
function go_back(self) {
    //若存在悬停框则直接关闭
    $(self).find('input[data-bs-toggle="popover"]').each(function (index, inputPopover) {
        //获取悬停框对象
        let oPopover = bootstrap.Popover.getInstance(inputPopover);
        //若悬停框存在则销毁
        if (oPopover != undefined)
            oPopover.dispose();
    });
    //收起当前容器
    $(self).hide();
    //清空当前容器内的内容
    $(self).find('> .modal-header').html('');
    $(self).find('> .modal-body').html('');
    $(self).find('> .modal-footer').html('');
    //打开上一个同级容器容器
    $(self).prev().show();
}
//关闭模态框
function close_dialog(divDialog) {
    //关闭当前模态框
    let modal = bootstrap.Modal.getInstance(divDialog);
    modal.hide();
    //清空当前容器内的内容
    divDialog.querySelector('.modal-header').innerHTML = '';
    divDialog.querySelector('.modal-body').innerHTML = '';
    divDialog.querySelector('.modal-footer').innerHTML = '';
}