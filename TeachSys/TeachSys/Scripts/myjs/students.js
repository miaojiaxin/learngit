function query(n) {
    console.log(n);
    $("#tb").datagrid("load", { classid: n });
}
$(function () {

    $("#majors").combobox({
        width: 200,
        editable: false,
        //url: "/majors/GetMajorsByDepartmentId?deptId=20",
        url: '/majors/GetMajors',
        valueField: 'ID',
        textField: "Name",
        onChange: function (n, o) {
            
            $("#classes").combobox({
                width: 200,
                editable: false,
                //url: "/majors/GetMajorsByDepartmentId?deptId=20",
                url: '/classes/GetClassesListByMajorId?majorid=' + n,
                valueField: 'ID',
                textField: "Name",
                onChange: function (n, o) {
                    query(n);
                    
                },
                onLoadSuccess: function () {
                    $("#classes").combobox("setValue", $("#classes").combobox("getData")[0].ID);
                }
            });
        },
        onLoadSuccess: function () {
           
            $("#majors").combobox("setValue", $("#majors").combobox("getData")[0].ID);
        }
    });
    
    var tb = [
            {
                text: '添加',
                iconCls: 'icon-add',
                handler: function () {
                    var url = "/students/Add";
                    $("#students_dlg").dialog({
                        title: "学生信息添加--",
                        iconCls: 'icon-add',

                        modal: true,  //指定该对话框是模态对话框
                        width: 420,
                        height: 360,
                        href: url,
                        buttons: [
                           {
                               text: '保存',
                               iconCls: 'icon-save',
                               handler: function () {
                                   $("#students_add").form({
                                       url: "/students/AddStudent/",
                                       queryParams:{
                                           ClassID:$("#classes").combobox("getValue")
                                       },
                                       success: function (data) {
                                           if (data == "ok") {
                                               $.messager.show({
                                                   title: '提示...',
                                                   msg: '保存成功！',
                                                   showType: 'show'
                                               });
                                               $("#students_dlg").dialog("close");
                                               $("#tb").datagrid("reload");
                                           }
                                           else
                                               $.messager.show({
                                                   title: '提示...',
                                                   msg: '保存失败！',
                                                   showType: 'show'
                                               });
                                       }
                                   });
                                   $("#students_add").form("submit");
                                   
                               }
                           },
                            {
                                text: '取消',
                                iconCls: 'icon-cancel',
                                handler: function () {
                                    $("#students_dlg").dialog("close");

                                }
                            }
                        ]
                    });
                }

            },
            {
                text: "修改",
                iconCls: "icon-edit",
                handler: function () {
                    var row = $("#tb").datagrid("getSelected");
                    if (row == null) {
                        $.messager.alert("提示...", "请选择行!", "warning");
                        return;
                    }
                    var id = row.ID;
                    var url = "/students/Edit/" + id;
                    $("#students_dlg").dialog({
                        title: "学生信息修改--",
                        iconCls: 'icon-add',

                        modal: true,  //指定该对话框是模态对话框
                        width: 420,
                        height: 360,
                        href: url,
                        buttons: [
                           {
                               text: '保存',
                               iconCls: 'icon-save',
                               handler: function () {

                                   $("#students_edit").form({
                                       url: "/students/EditStudent/",
                                       queryParams: {
                                           ClassID: $("#classes").combobox("getValue")
                                       },
                                       success: function (data) {
                                           if (data == "ok") {
                                               $.messager.show({
                                                   title: '提示...',
                                                   msg: '保存成功！',
                                                   showType: 'show'
                                               });
                                               $("#students_dlg").dialog("close");
                                               $("#tb").datagrid("reload");
                                           }
                                           else
                                               $.messager.show({
                                                   title: '提示...',
                                                   msg: '保存失败！',
                                                   showType: 'show'
                                               });
                                       }
                                   });
                                   $("#students_edit").form("submit");

                               }
                           },
                            {
                                text: '取消',
                                iconCls: 'icon-cancel',
                                handler: function () {
                                    $("#students_dlg").dialog("close");

                                }
                            }
                        ]
                    });
                }
            },
            '-',
            {
                text: '删除',
                iconCls: 'icon-remove',
                handler: function () {
                    var row = $("#tb").datagrid("getSelected");
                    if (row == null) {
                        $.messager.alert("提示...", "请选择行!", "warning");
                        return;
                    }
                    $.messager.confirm("提示...", "确信要删除学生“" + row.Name + "”吗?", function (t) {
                        if (!t)
                            return;
                        var id = row.ID;
                        console.log(id);
                        var url = "/students/Delete/" + id;
                        $.post(url, function (data) {
                            if (data == "ok") {
                                $.messager.show({
                                    title: '提示...',
                                    msg: '删除成功！',
                                    showType: 'show'
                                });

                                $("#tb").datagrid("reload");
                            }
                            else
                                $.messager.show({
                                    title: '提示...',
                                    msg: '删除失败！',
                                    showType: 'show'
                                });
                        }, "text");
                    });
                }
            }
    ];
    $("#tb").datagrid({
        url: '/Students/GetStudents/',
        queryParams:{classid:0},
        width: 900,
        height: 400,
        fitColumns: true,
        singleSelect: true,
        toolbar: tb,
        columns: [[
            { field: 'Name', title: '姓名', width: 60 },
            { field: 'StudentNo', title: '学号', width: 70 },
            { field: 'TelNum', title: '联系电话', width: 80 },
            { field: 'QQ', title: 'QQ号', width: 60 },
            { field: 'WeChat', title: '微信号', width: 70 },
            { field: 'PTelNo1', title: '家长联系方式1', width:80 },
            { field: 'PTelNo2', title: '家长联系方式2', width:80 },
            { field: 'Memo', title: '备注', width: 180 }
        ]]
    });
});