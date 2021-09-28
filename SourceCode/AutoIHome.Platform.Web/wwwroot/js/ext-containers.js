//modal-content容器类
function ModalContentContainer(parent, name) {
    //初始化属性
    this.url = '';  //访问后台表单html页面的url地址
    this.data = {}; //访问后台数据时用的参数

    //展开当前容器
    this.show = function () {
        //获取当前容器
        let divContainer = document.querySelector(parent).querySelector('[data-name="' + name + '"]');
        //填充表单并展开当前滑动框
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //加载标题
                divContainer.querySelector('.modal-header').innerHTML = get_loading_html();
                //加载modal-body
                let sLoadHtml = '<div class="text-center">' + get_loading_html() + '</div>';
                divContainer.querySelector('.modal-body').innerHTML = sLoadHtml;
                //加载modal-footer
                divContainer.querySelector('.modal-footer').innerHTML = get_loading_html();
                //收起其它容器
                $(parent).find('> div[data-name!="' + name + '"]').hide();
                //放下当前容器
                $(divContainer).show();
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                //console.log(data);
                console.log(textStatus);
                //填充内容
                divContainer.innerHTML = data;
                //加载tooltips
                let divModalBody = divContainer.querySelector('.modal-body');
                divModalBody.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (iTooltip) {
                    let tooltip = new bootstrap.Tooltip(iTooltip, {
                        boundary: divModalBody
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
//modal-content表单容器类
function ModalContentFormContainer(parent, name) {
    //初始化属性
    this.url = '';                  //访问后台表单html页面的url地址
    this.data = {};                 //访问后台数据时用的参数

    //设置表单的函数
    this.set_form = function (form) {
        //加载日期选择
        set_date_picker($(form).find('input[data-role="datepicker"]'));
        //加载时间选择
        set_datetime_picker($(form).find('input[data-role="datetimepicker"]'));
    };

    //展开当前容器
    this.show = function () {
        //获取当前容器
        let divContainer = document.querySelector(parent).querySelector('[data-name="' + name + '"]');
        //获取当前实例
        var oSelf = this;
        //填充表单并展开当前滑动框
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //加载标题
                divContainer.querySelector('.modal-header').innerHTML = get_loading_html();
                //加载modal-body
                let sLoadHtml = '<div class="text-center">' + get_loading_html() + '</div>';
                divContainer.querySelector('.modal-body').innerHTML = sLoadHtml;
                //加载modal-footer
                divContainer.querySelector('.modal-footer').innerHTML = get_loading_html();
                //收起其它容器
                $(parent).find('> div[data-name!="' + name + '"]').hide();
                //放下当前容器
                $(divContainer).show();
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                //console.log(data);
                console.log(textStatus);
                //填充内容
                divContainer.innerHTML = data;
                //初始化表单
                oSelf.set_form(divContainer.querySelector('.modal-body form'));
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

//modal-dialog容器类
function ModalDialogContainer(sContainerId) {
    //初始化属性
    this.url = '';  //访问后台表单html页面的url地址
    this.data = {}; //访问后台数据时用的参数

    //弹出当前容器
    this.show = function () {
        //获取当前容器
        let divContainer = document.getElementById(sContainerId);
        //填充表单并展开当前滑动框
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //加载标题
                divContainer.querySelector('.modal-header').innerHTML = get_loading_html();
                //加载modal-body
                let sLoadHtml = '<div class="text-center">' + get_loading_html() + '</div>';
                divContainer.querySelector('.modal-body').innerHTML = sLoadHtml;
                //加载modal-footer
                divContainer.querySelector('.modal-footer').innerHTML = get_loading_html();
                //弹出当前框
                let modal = bootstrap.Modal.getInstance(divContainer);
                modal.show();
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                //console.log(data);
                console.log(textStatus);
                //填充内容
                divContainer.querySelector('.modal-content').innerHTML = data;
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
//modal-dialog表单容器类
function ModalDialogFormContainer(sContainerId) {
    //初始化属性
    this.url = '';                  //访问后台表单html页面的url地址
    this.data = {};                 //访问后台数据时用的参数

    //设置表单的函数
    this.set_form = function (form) {
        //加载日期选择
        set_date_picker($(form).find('input[data-role="datepicker"]'));
        //加载时间选择
        set_datetime_picker($(form).find('input[data-role="datetimepicker"]'));
    };

    //弹出当前容器
    this.show = function () {
        //获取当前容器
        let divContainer = document.getElementById(sContainerId);
        //获取当前实例
        let oSelf = this;
        //填充表单并展开当前滑动框
        $.ajax({
            type: 'POST',
            url: this.url,
            cache: false,
            dataType: 'html',
            data: this.data,
            beforeSend: function () {
                //加载标题
                divContainer.querySelector('.modal-header').innerHTML = get_loading_html();
                //加载modal-body
                let sLoadHtml = '<div class="text-center">' + get_loading_html() + '</div>';
                divContainer.querySelector('.modal-body').innerHTML = sLoadHtml;
                //加载modal-footer
                divContainer.querySelector('.modal-footer').innerHTML = get_loading_html();
                //弹出当前框
                let modal = bootstrap.Modal.getInstance(divContainer);
                modal.show();
            },
            success: function (data, textStatus, jqXHR) {
                //打印log
                //console.log(data);
                console.log(textStatus);
                //填充内容
                divContainer.querySelector('.modal-content').innerHTML = data;
                //初始化表单
                oSelf.set_form(divContainer.querySelector('.modal-body form'));
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