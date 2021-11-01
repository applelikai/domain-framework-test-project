/// <reference path="../lib/bootbox/bootbox.all.js" />
/// <reference path="base.js" />

//清空表单临时查询时输入的值
function __clean_form_search(form) {
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

//Json数据表单提交类
function JsonFormSubmitter(form) {
    //url地址
    this.url = '';
    //获取表单数据的函数
    this.get_data = function (form) {
        //创建数据对象
        let data = new Object();
        //遍历表单元素并填充值
        Array.from(form.elements).forEach(function (element) {
            //赋值
            data[element.name] = element.value;
        });
        //获取数据对象
        return data;
    };
    //回掉函数
    this.call_back = function () { };
    //提交
    this.submit = function () {
        //清空表单临时查询时输入的值
        __clean_form_search(form);
        //添加验证css
        if (!form.classList.contains('was-validated'))
            form.classList.add('was-validated');
        //若验证失败则退出
        if (!form.checkValidity())
            return;

        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置HTTP请求头(发送到服务器的数据为json)
        oHttpRequest.setRequestHeader('content-type', 'application/json; charset=UTF-8');
        //设置接收的数据类型
        oHttpRequest.responseType = 'json'
        //绑定响应状态事件监听函数
        let self = this;
        oHttpRequest.onreadystatechange = function () {
            //若未完成则退出
            if (oHttpRequest.readyState != 4)
                return;
            //若请求不成功,则退出
            if (oHttpRequest.status != 200) {
                //打印log
                console.log('error');
                console.log(oHttpRequest.responseText);
                return;
            }
            //若请求成功,获取执行结果对象
            let jResult = oHttpRequest.response;
            //若执行成功
            if (jResult.status) {
                //弹出执行结果
                PNotify.success({ title: 'success', text: jResult.message });
                //执行回掉函数
                self.call_back();
            }
            else {
                //弹出执行结果
                PNotify.error({ title: 'error', text: jResult.message });
            }
        };
        //发送 HTTP 请求
        let sData = JSON.stringify(self.get_data(form));
        oHttpRequest.send(sData);
    };
}
//数据表单提交类
function DataFormSubmitter(form) {
    //url地址
    this.url = '';
    //获取表单数据的函数
    this.get_data = function (form) {
        return new FormData(form);
    };
    //回掉函数
    this.call_back = function () { };
    //提交
    this.submit = function () {
        //清空表单临时查询时输入的值
        __clean_form_search(form);
        //添加验证css
        if (!form.classList.contains('was-validated'))
            form.classList.add('was-validated');
        //若验证失败则退出
        if (!form.checkValidity())
            return;

        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置接收的数据类型
        oHttpRequest.responseType = 'json'
        //绑定响应状态事件监听函数
        let self = this;
        oHttpRequest.onreadystatechange = function () {
            //若未完成则退出
            if (oHttpRequest.readyState != 4)
                return;
            //若请求不成功,则退出
            if (oHttpRequest.status != 200) {
                //打印log
                console.log('error');
                console.log(oHttpRequest.responseText);
                return;
            }
            //若请求成功,获取执行结果对象
            let jResult = oHttpRequest.response;
            //若执行成功
            if (jResult.status) {
                //弹出执行结果
                PNotify.success({ title: 'success', text: jResult.message });
                //执行回掉函数
                self.call_back();
            }
            else {
                //弹出执行结果
                PNotify.error({ title: 'error', text: jResult.message });
            }
        };
        //发送 HTTP 请求
        oHttpRequest.send(this.get_data(form));
    };
}
//数据提交类
function DataSubmitter(title) {
    //访问后台操作数据的url
    this.url = '';
    //访问后台操作数据时用的参数
    this.data = {};

    //初始化回掉函数
    this.call_back = function () { };
    //提交
    this.submit = function () {
        //获取参数
        var self = this;
        //执行操作数据
        bootbox.confirm({
            message: title,
            buttons: {
                confirm: {
                    label: '确&nbsp;&nbsp;定',
                    className: 'btn btn-outline-success'
                },
                cancel: {
                    label: '取&nbsp;&nbsp;消',
                    className: 'btn btn-outline-danger'
                }
            },
            callback: function (result) {
                //若选择了取消,则退出
                if (!result)
                    return;
                //获取HttpRequest对象
                let oHttpRequest = get_http_request();
                //初始化请求
                oHttpRequest.open('POST', self.url);
                //设置HTTP请求头(发送到服务器的数据为key=value字符串)
                oHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
                //设置接收的数据类型
                oHttpRequest.responseType = 'json'
                //绑定响应状态事件监听函数
                oHttpRequest.onreadystatechange = function () {
                    //若未完成则退出
                    if (oHttpRequest.readyState != 4)
                        return;
                    //若请求不成功,则退出
                    if (oHttpRequest.status != 200) {
                        //打印log
                        console.log('error');
                        console.log(oHttpRequest.responseText);
                        return;
                    }
                    //若请求成功,获取执行结果对象
                    let jResult = oHttpRequest.response;
                    //若执行成功
                    if (jResult.status) {
                        //弹出执行结果
                        PNotify.success({ title: 'success', text: jResult.message });
                        //执行回掉函数
                        self.call_back();
                    }
                    else {
                        //弹出执行结果
                        PNotify.error({ title: 'error', text: jResult.message });
                    }
                };
                //发送 HTTP 请求
                let sData = to_urlencoded_data_string(self.data);
                oHttpRequest.send(sData);
            },
            closeButton: false
        });
    };
}