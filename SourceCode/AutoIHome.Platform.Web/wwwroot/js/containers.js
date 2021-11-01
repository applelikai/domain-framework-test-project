/// <reference path="../lib/jquery/jquery.js" />
/// <reference path="base.js" />

//加载容器
function __loading(divContainer) {
    //加载标题
    divContainer.querySelector('.modal-header').innerHTML = get_loading_html();
    //加载modal-body
    let sLoadHtml = '<div class="text-center">' + get_loading_html() + '</div>';
    divContainer.querySelector('.modal-body').innerHTML = sLoadHtml;
    //加载modal-footer
    divContainer.querySelector('.modal-footer').innerHTML = get_loading_html();
}
//加载日期控件列表
function __load_datepickers(form) {
    //加载日期选择
    layui.use('laydate', function () {
        //获取laydate控件
        let laydate = layui.laydate;
        //遍历加载日期控件
        form.querySelectorAll('input[data-role="datepicker"]').forEach(function (input) {
            laydate.render({
                elem: input
            });
        });
        //遍历加载日期时间控件
        form.querySelectorAll('input[data-role="datetimepicker"]').forEach(function (input) {
            laydate.render({
                elem: input,
                type: 'datetime'
            });
        });
    });
}

//modal-content容器类
function ModalContentContainer(parent, name) {
    //访问后台表单html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {}; 

    //加载当前容器
    this.__load = function (divContainer) {
        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置HTTP请求头(表示服务器接收表单数据格式的数据)
        oHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
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
            //若请求成功,则加载内容
            divContainer.innerHTML = oHttpRequest.responseText;
            //加载tooltips
            let divModalBody = divContainer.querySelector('.modal-body');
            divModalBody.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                let tooltip = new bootstrap.Tooltip(iTooltip, {
                    boundary: divModalBody
                });
            });
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    }
    //展开当前容器
    this.show = function () {
        //遍历父容器下的所有子容器
        for (let htmlElement of document.querySelector(parent).children) {
            //若当前子容器不为div，则跳过
            if (htmlElement.nodeName !== 'DIV')
                continue;
            //若不为当前容器，则隐藏并跳过
            if (htmlElement.getAttribute('data-name') !== name) {
                //若类样式不包含d-none则添加d-none类样式
                if (!htmlElement.classList.contains('d-none'))
                    htmlElement.classList.add('d-none');
                continue;
            }
            //若为当前容器，则显示并加载
            if (htmlElement.classList.contains('d-none'))
                htmlElement.classList.remove('d-none');
            //初始化当前容器
            __loading(htmlElement);
            //加载当前容器
            this.__load(htmlElement);
        }
    };
}
//modal-content表单容器类
function ModalContentFormContainer(parent, name) {
    //访问后台表单html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //加载当前容器
    this.__load = function (divContainer) {
        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置HTTP请求头(表示服务器接收表单数据格式的数据)
        oHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
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
            //若请求成功,则加载内容
            divContainer.innerHTML = oHttpRequest.responseText;
            //初始化表单
            self.set_form(divContainer.querySelector('.modal-body form'));
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    }
    //设置表单的函数
    this.set_form = function (form) {
        //加载表单日期时间控件
        __load_datepickers(form);
    };
    //展开当前容器
    this.show = function () {
        //遍历父容器下的所有子容器
        for (let htmlElement of document.querySelector(parent).children) {
            //若当前子容器不为div，则跳过
            if (htmlElement.nodeName !== 'DIV')
                continue;
            //若不为当前容器，则隐藏并跳过
            if (htmlElement.getAttribute('data-name') !== name) {
                //若类样式不包含d-none则添加d-none类样式
                if (!htmlElement.classList.contains('d-none'))
                    htmlElement.classList.add('d-none');
                continue;
            }
            //若为当前容器，则显示并加载
            if (htmlElement.classList.contains('d-none'))
                htmlElement.classList.remove('d-none');
            //初始化当前容器
            __loading(htmlElement);
            //加载当前容器
            this.__load(htmlElement);
        }
    };
}

//modal-dialog容器类
function ModalDialogContainer(sContainerId) {
    //访问后台表单html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //弹出当前容器
    this.show = function () {
        //获取当前容器
        let divContainer = document.getElementById(sContainerId);
        //加载当前容器
        __loading(divContainer);
        //弹出当前框
        bootstrap.Modal.getInstance(divContainer).show();

        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置HTTP请求头(表示服务器接收表单数据格式的数据)
        oHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
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
            //若请求成功,则加载内容
            divContainer.querySelector('.modal-content').innerHTML = oHttpRequest.responseText;
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    };
}
//modal-dialog表单容器类
function ModalDialogFormContainer(sContainerId) {
    //访问后台表单html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //设置表单的函数
    this.set_form = function (form) {
        //加载表单日期时间控件
        __load_datepickers(form);
    };
    //弹出当前容器
    this.show = function () {
        //获取当前容器
        let divContainer = document.getElementById(sContainerId);
        //加载当前容器
        __loading(divContainer);
        //弹出当前框
        bootstrap.Modal.getInstance(divContainer).show();

        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置HTTP请求头(表示服务器接收表单数据格式的数据)
        oHttpRequest.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
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
            //若请求成功,则加载内容
            divContainer.querySelector('.modal-content').innerHTML = oHttpRequest.responseText;
            //初始化表单
            self.set_form(divContainer.querySelector('.modal-body form'));
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    };
}