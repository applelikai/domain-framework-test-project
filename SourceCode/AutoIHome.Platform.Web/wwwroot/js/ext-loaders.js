/// <reference path="../lib/jquery/jquery.js" />
/// <reference path="../lib/bootstrap/js/bootstrap.bundle.js" />
/// <reference path="ext-utils.js" />

//html加载类
function HtmlLoader(container) {
    this.url = undefined;               //访问后台html页面的url地址
    this.data = undefined;              //访问后台数据时用的参数

    //加载之前
    this.before_load = function (container) {
        $(container).html(get_loading_html());
        return true;
    };
    //回掉函数
    this.call_back = function (container) {
    };
    //加载
    this.load = function () {
        //获取当前实例
        var oSelf = this;
        //加载数据
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                return oSelf.before_load(container);
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                console.log(textStatus);
                //填充内容
                $(container).html(data);
                //执行回掉函数
                oSelf.call_back(container);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //显示错误信息
                document.write(jqXHR.responseText);
                //记录log
                console.log(jqXHR);
                console.log(textStatus);
                console.log(errorThrown);
            }
        });
    };
}
//Table加载类
function TableLoader(sSelector, nColspan) {
    //访问后台html页面的url地址
    this.url = undefined;
    //访问后台数据时用的参数
    this.data = undefined;

    //加载
    this.load = function () {
        //获取当前容器
        let divContainer = document.querySelector(sSelector);
        //加载数据
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //获取加载字符串
                var sLoadingHtml = '<tr><td colspan="' + nColspan + '"><i class="fa fa-2x fa-spinner fa-spin text-primary"></i></td></tr>';
                //加载tbody
                divContainer.querySelector('table > tbody').innerHTML = sLoadingHtml;
                //加载tfoot
                let tfoot = divContainer.querySelector('table > tfoot');
                if (tfoot != undefined)
                    tfoot.innerHTML = sLoadingHtml;
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                console.log(textStatus);
                //填充内容
                divContainer.innerHTML = data;
                //获取table
                let table = divContainer.querySelector('table');
                //加载提示框
                table.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                    let tooltip = new bootstrap.Tooltip(iTooltip, {
                        boundary: table
                    });
                });
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
//分页Table加载类
function PageTableLoader(sSelector, nColspan) {
    //访问后台html页面的url地址
    this.url = undefined;
    //访问后台数据时用的参数
    this.data = undefined;

    //加载
    this.load = function () {
        //获取当前容器
        let divContainer = document.querySelector(sSelector);
        //确定当前页和页大小
        if (this.data.pageIndex == undefined)
            this.data.pageIndex = divContainer.querySelector('input[name="PageIndex"]').value;
        if (this.data.pageSize == undefined)
            this.data.pageSize = divContainer.querySelector('input[name="PageSize"]').value;
        //加载数据
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //获取加载字符串
                var sLoadingHtml = '<tr><td colspan="' + nColspan + '"><i class="fa fa-2x fa-spinner fa-spin text-primary"></i></td></tr>';
                //加载tbody
                divContainer.querySelector('table tbody').innerHTML = sLoadingHtml;
                //加载tbody
                divContainer.querySelector('table tfoot').innerHTML = sLoadingHtml;
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                console.log(textStatus);
                //填充内容
                divContainer.innerHTML = data;
                //获取table
                let table = divContainer.querySelector('table');
                //加载提示框
                table.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                    //新建提示框
                    let tooltip = new bootstrap.Tooltip(iTooltip, {
                        boundary: table
                    });
                });
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
//分页列表加载类
function PagedListLoader(sSelector) {
    //访问后台html页面的url地址
    this.url = undefined;
    //访问后台数据时用的参数
    this.data = undefined;

    //加载
    this.load = function () {
        //获取当前容器
        let divContainer = document.querySelector(sSelector);
        //确定当前页和页大小
        if (this.data.pageIndex == undefined)
            this.data.pageIndex = divContainer.querySelector('input[name="PageIndex"]').value;
        if (this.data.pageSize == undefined)
            this.data.pageSize = divContainer.querySelector('input[name="PageSize"]').value;
        //加载数据
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //获取加载字符串
                let sLoadingHtml = '<div class="text-center">' + get_loading_html() + '</div>';
                //加载主体
                divContainer.querySelector('main').innerHTML = sLoadingHtml;
                //加载页尾
                divContainer.querySelector('footer').innerHTML = sLoadingHtml;
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                console.log(textStatus);
                //填充内容
                divContainer.innerHTML = data;
                //获取table
                let table = divContainer.querySelector('main table');
                //加载提示框
                table.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                    //新建提示框
                    let tooltip = new bootstrap.Tooltip(iTooltip, {
                        boundary: table
                    });
                });
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