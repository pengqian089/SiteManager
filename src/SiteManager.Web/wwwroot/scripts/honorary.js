$(function () {
    $("[data-delete]").on("click", function () {
        let id = $(this).data("delete");
        $.confirm({
            title: '警告',
            content: '删除后无法恢复，确定删除吗？',
            type: 'orange',
            buttons: {
                confirm: {
                    text: '确认',
                    btnClass: 'btn-orange',
                    action: function () {
                        lightyear.loading('show');
                        $.ajax({
                            url: "/Main/DeleteHonorary",
                            type: "post",
                            data: {id}
                        }).done(function (result) {
                            if (result.success) {
                                location.reload();
                            }else{
                                lightyear.loading('hide');
                                $.alert(result.msg);
                            }
                        }).fail(function(xhr){
                            lightyear.loading('hide');
                            $.alert(xhr["responseJSON"].msg);
                        });
                    }
                },
                cancel: {
                    text: '关闭',
                    action: function () {

                    }
                }
            }
        });
    });
})