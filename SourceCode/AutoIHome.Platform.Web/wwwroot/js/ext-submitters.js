/// <reference path="../lib/bootbox/bootbox.all.js" />

//数据表单提交类
function DataFormSubmitter(form) {
    //url地址
    this.url = '';
    //检查验证结果函数
    this.validate = function (form) {
        //添加验证css
        if (!$(form).hasClass('was-validated'))
            $(form).addClass('was-validated');
        //返回验证结果
        return form.checkValidity();
    }
    //回掉函数
    this.call_back = function () { };
    //提交
    this.submit = function () {
        var oSelf = this;
        //提交
        $.ajax({
            type: 'POST',
            url: this.url,
            data: $(form).serializeObject(),
            cache: false,
            dataType: "json",
            beforeSend: function () {
                //返回是否触发成功
                return oSelf.validate(form);
            },
            success: function (data, status, xhr) {
                //记录log
                console.log(data);
                console.log(status);
                //若执行成功
                if (data.status) {
                    //弹出执行结果
                    PNotify.success({ title: 'success', text: data.message });
                    //执行回掉函数
                    oSelf.call_back();
                } else {
                    //弹出执行结果
                    PNotify.error({ title: 'error', text: data.message });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //记录log
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
            }
        });
    };
}
//文件表单提交类
function FileFormSubmitter(form) {
    //url地址
    this.url = '';
    //检查验证结果函数
    this.validate = function (form) {
        //添加验证css
        if (!$(form).hasClass('was-validated'))
            $(form).addClass('was-validated');
        //返回验证结果
        return form.checkValidity();
    };
    //回掉函数
    this.call_back = function () { };
    //提交
    this.submit = function () {
        var oSelf = this;
        //提交
        $.ajax({
            type: 'POST',
            url: this.url,
            data: new FormData(form),
            cache: false,
            processData: false,
            contentType: false,
            dataType: "json",
            beforeSend: function () {
                //返回是否触发成功
                return oSelf.validate(form);
            },
            success: function (data, status, xhr) {
                //记录log
                console.log(data);
                console.log(status);
                //若执行成功
                if (data.status) {
                    //弹出执行结果
                    PNotify.success({ title: 'success', text: data.message });
                    //执行回掉函数
                    oSelf.call_back();
                } else {
                    //弹出执行结果
                    PNotify.error({ title: 'error', text: data.message });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //记录log
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
            }
        });
    };
}
//数据提交类
function DataSubmitter(title) {
    this.url = '';   //访问后台操作数据的url
    this.data = {};  //访问后台操作数据时用的参数

    //执行提交
    this.__submit = function (result) {
        //获取当前对象
        var oSelf = this;
        //执行提交
        $.ajax({
            type: 'POST',
            url: oSelf.url,
            data: oSelf.data,
            cache: false,
            dataType: "json",
            beforeSend: function () {
                return result;
            },
            success: function (data, status, xhr) {
                //记录log
                console.log(data);
                console.log(status);
                //处理执行结果
                if (data.status) {
                    //弹出执行结果
                    PNotify.success({ title: 'success', text: data.message });
                    //若执行成功,调用回掉函数
                    oSelf.call_back();
                } else {
                    //弹出执行结果
                    PNotify.error({ title: 'error', text: data.message });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //记录log
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
            }
        });
    }

    //初始化回掉函数
    this.call_back = function () { };
    //提交
    this.submit = function () {
        //获取参数
        var oSelf = this;
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
                oSelf.__submit(result);
            },
            closeButton: false
        });
    };
}