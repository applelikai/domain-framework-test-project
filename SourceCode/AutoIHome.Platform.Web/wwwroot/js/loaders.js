/// <reference path="../lib/jquery/jquery.js" />
/// <reference path="../lib/bootstrap/js/bootstrap.bundle.js" />
/// <reference path="base.js" />

//html加载类
function HtmlLoader(selector) {
    //访问后台html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //加载
    this.load = function () {
        //获取当前容器 和 加载字符串
        let divContainer = get_html_element(selector),
            sLoadingHtml = '<div class="text-center">' + get_loading_html() + '</div>';
        //加载
        divContainer.innerHTML = sLoadingHtml;

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
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    };
}
//Table加载类
function TableLoader(sSelector, nColspan) {
    //访问后台html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //加载
    this.load = function () {
        //获取当前容器
        let divContainer = get_html_element(sSelector),
            sLoadingHtml = '<tr><td colspan="' + nColspan + '">' + get_loading_html() + '</td></tr>';
        //加载tbody
        divContainer.querySelector('table > tbody').innerHTML = sLoadingHtml;
        //加载tfoot
        let tfoot = divContainer.querySelector('table > tfoot');
        if (tfoot != undefined)
            tfoot.innerHTML = sLoadingHtml;

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
            //获取table
            let table = divContainer.querySelector('table');
            //加载提示框
            table.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                let tooltip = new bootstrap.Tooltip(iTooltip, {
                    boundary: table
                });
            });
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    };
}
//分页Table加载类
function PageTableLoader(sSelector, nColspan) {
    //访问后台html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //加载
    this.load = function () {
        //获取当前容器
        let divContainer = get_html_element(sSelector);
        //确定当前页和页大小
        if (this.data.pageIndex == undefined)
            this.data.pageIndex = divContainer.querySelector('input[name="PageIndex"]').value;
        if (this.data.pageSize == undefined)
            this.data.pageSize = divContainer.querySelector('input[name="PageSize"]').value;
        //获取加载字符串并加载容器
        let sLoadingHtml = '<tr><td colspan="' + nColspan + '"><i class="fa fa-2x fa-spinner fa-spin text-primary"></i></td></tr>';
        divContainer.querySelector('table tbody').innerHTML = sLoadingHtml; //加载tbody
        divContainer.querySelector('table tfoot').innerHTML = sLoadingHtml; //加载tbody

        //获取HttpRequest对象
        let oHttpRequest = get_http_request();
        //初始化请求
        oHttpRequest.open('POST', this.url);
        //设置HTTP请求头(发送到服务器的数据为key=value字符串)
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
            //获取table
            let table = divContainer.querySelector('table');
            //加载提示框
            table.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                //新建提示框
                let tooltip = new bootstrap.Tooltip(iTooltip, {
                    boundary: table
                });
            });
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    };
}
//分页列表加载类
function PagedListLoader(sSelector) {
    //访问后台html页面的url地址
    this.url = '';
    //访问后台数据时用的参数
    this.data = {};

    //加载
    this.load = function () {
        //获取当前容器
        let divContainer = document.querySelector(sSelector);
        //确定当前页和页大小
        if (this.data.pageIndex == undefined)
            this.data.pageIndex = divContainer.querySelector('input[name="PageIndex"]').value;
        if (this.data.pageSize == undefined)
            this.data.pageSize = divContainer.querySelector('input[name="PageSize"]').value;
        //获取加载字符串并加载容器
        let sLoadingHtml = '<div class="text-center">' + get_loading_html() + '</div>';
        divContainer.querySelector('main').innerHTML = sLoadingHtml;    //加载主体
        divContainer.querySelector('footer').innerHTML = sLoadingHtml;  //加载页尾

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
            //获取table
            let table = divContainer.querySelector('main table');
            //加载提示框
            table.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                //新建提示框
                let tooltip = new bootstrap.Tooltip(iTooltip, {
                    boundary: table
                });
            });
        };
        //发送 HTTP 请求
        oHttpRequest.send(get_urlencoded_string(this.data));
    };
}