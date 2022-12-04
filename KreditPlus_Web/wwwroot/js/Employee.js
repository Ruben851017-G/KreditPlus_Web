
$(document).ready(function () {

    var selectedIds = [];

    var oTable = $('#employeesTable').DataTable(
        {
            columnDefs: [{
                orderable: false,
                className: 'select-checkbox',
                targets: 0,
                checkboxes: {
                    'selectRow': true
                }
            }],
            rowGroup: {
                dataSrc: 'group'
            },
            dom: "lBfrtip",
            buttons: [
                {
                    text: '<i class="fa fa-plus" aria-hidden="true"></i>',
                    titleAttr: 'Add New',
                    action: Addnew
                },
                {
                    text: '<i class="fa fa-check" aria-hidden="true"></i>',
                    titleAttr: 'Select All',
                    attr: {
                        id:'selectAll'
                    }
                },
                {
                    text: '<i class="fa fa-times-circle-o" aria-hidden="true"></i>',
                    titleAttr: 'Deselect All',
                    attr: {
                        id: 'DeselectAll'
                    }
                },
                {
                    text: '<i class="fa fa-trash-o" aria-hidden="true"></i>',
                    titleAttr: 'Remove Selected',
                    attr: {
                        id: 'removeAll'
                    }
                },
                {
                    extend: 'copyHtml5',
                    text: '<i class="fa fa-files-o"></i>',
                    titleAttr: 'Copy'
                },
                {
                    extend: 'excelHtml5',
                    text: '<i class="fa fa-file-excel-o"></i>',
                    titleAttr: 'Excel'
                },
                {
                    extend: 'csvHtml5',
                    text: '<i class="fa fa-file-text-o"></i>',
                    titleAttr: 'CSV'
                },
                {
                    extend: 'pdfHtml5',
                    text: '<i class="fa fa-file-pdf-o"></i>',
                    titleAttr: 'PDF'
                }
            ],
            ajax: {
                url: "Employees/GetEmployeeList",
                type: "POST",
            },
            processing: true,
            fixedHeader: true,
            serverSide: true,
            filter: true,
            select: true,
            responsive: false,
            //scrollY: 400,
            //scrollX: true,
            //scrollCollapse: true,
            columns: [
                {
                    data: null, defaultContent: '', className: 'text-center', orderable: false, searchable: false,
                    render: function (data, type, full, meta) {
                        return '<input type="checkbox" class="select-checkbox" name="check" value="' + data.employeeId + '">';
                    },
                    width: "5%"

                },
                { data: "employeeId", name: "EmployeeId", width: "60px", className: "dt-center", visible: false },
                { data: "employeeFirstName", name: "EmployeeFirstName", width: "80px", autoWidth: true, className: "dt-center" },
                { data: "employeeLastName", name: "EmployeeLastName", width: "80px", autoWidth: true, className: "dt-center" },
                {
                    data: "dateJoin", name: "DateJoin", width: "80px", autoWidth: true, className: "dt-center",
                    render: function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "-" + date.getDate() + "-" + date.getFullYear();
                    }
                },
                {
                    data: "salary", name: "Salary", autoWidth: true, className: "dt-center",
                    render: function (data, type) {
                        var number = $.fn.dataTable.render
                            .number(',', '.', 2, 'IDR.')
                            .display(data);
                        if (type === 'display') {
                            let color = 'green';
                            if (data < 7500000 && data > 5000000) {
                                color = 'orange';
                            } else if (data < 5000000) {
                                color = 'red';
                            }
                            return '<span style="color:' + color + '">' + number + '</span>';
                        }
                        return number;
                    }
                },
                { data: "designation", name: "Designation", autoWidth: true, className: "dt-center" },
                {
                    className: "f32",
                    data: "headOffice",
                    name: "HeadOffice",
                    width: "100px",
                    render: function (data, type) {
                        if (type === 'display') {
                            switch (data) {
                                case 'Argentina':
                                    country = 'ar';
                                    break;
                                case 'Edinburgh':
                                    country = '_Scotland';
                                    break;
                                case 'London':
                                    country = '_England';
                                    break;
                                case 'New York':
                                case 'San Francisco':
                                    country = 'us';
                                    break;
                                case 'Sydney':
                                    country = 'au';
                                    break;
                                case 'Tokyo':
                                    country = 'jp';
                                    break;
                                case 'Jakarta':
                                    country = 'id';
                                    break;
                            }
                            return '<span class="flag ' + country + '"></span> ' + data;
                        }
                        return data;
                    }
                },
                {
                    data: "progressWork",
                    name: "ProgressWork",
                    width: "100px",
                    render: function (data, type) {
                        return type === 'display'
                            ? '<progress value="' + data + '" max="100"></progress>'
                            : data;
                    }
                },
                {
                    data: "employeeId",
                    orderable: false,
                    render: function (data) {
                        return '<a id="selectedit" class="popupEdit" href="/Employees/_EditForm/' + data + '"><i class="fa fa-pencil"/></a>';
                    }
                },
                {
                    data: "employeeId",
                    orderable: false,
                    render: function (data) {
                        return '<a id="selectdelete" class="popupDelete" href="/Employees/Delete/' + data + '"><i class="fa fa-trash"/></a>';
                    }
                },
            ],
            select: {
                style: 'multi',
                selector: 'td:first-child'
            },
            order: [[1, 'asc']]
        }
    );

    $('#removeAll').prop('disabled', true).css('opacity', 0.5);

    $('#DeselectAll').prop('disabled', true).css('opacity', 0.5);

    $('#selectAll').on('click', function (e) {
        e.preventDefault();
        if (this.click) {
            //$('.select-checkbox').each(function () {
            //    $(this).prop("checked", true);
            //    $(this).toggleClass('selected');
            //    selectedIds.push(this.value);
            //});
            if ($(this).prop("checked")) {
                data = oTable.row($(this).parents('tr')).data();
                selectedIds.push(data.employeeId);
            }
            console.log(selectedIds);
        }
    });

    $('#DeselectAll').on('click', function () {
        if (this.click) {
            $('.select-checkbox').each(function () {
                this.checked = true;
                $(this).removeClass('selected');
            });
        }
    });

    $('#removeAll').on('click', function () {
        alert();
    });

    $('#employeesTable tbody').on('click', 'tr', function () {
        var id = oTable.row(this).data().employeeId;
        var index = $.inArray(id, selectedIds);
        if (index === -1) {
            selectedIds.push(id);
        } else {
            selectedIds.splice(index, 1);
        }
        $(this).toggleClass('selected');
        if (selectedIds.length > 1) {
            $('#removeAll').attr('disabled', false).css('opacity', 1);
            $('#DeselectAll').prop('disabled', false).css('opacity', 1);
        }
        else {
            $('#removeAll').prop('disabled', true).css('opacity', 0.5);
            $('#DeselectAll').prop('disabled', true).css('opacity', 0.5);
        }
        console.log(selectedIds);
    });

    $('#employeesTable tbody').on('dblclick', 'tr', function () {
        var id = oTable.row(this).data().employeeId;
        OpenPopup('/Employees/_EditForm/'+ id,id);
    });

    $('.container').on('click', 'a.popupEdit','tr', function (e) {
        e.preventDefault();
        //var id = $(this).closest('tr').find('td').eq(1).value; /*to get text value table*/
        data = oTable.row($(this).parents('tr')).data();
        var id = data.employeeId;
        OpenPopup('/Employees/_EditForm/' + id, id);
    });

    //$('#employeesTable').on('click', 'tr', function (e) {
    //    e.preventDefault();
    //    var id = oTable.row(this).data().employeeId;
    //    OpenPopup('/Employees/_EditForm/' + id, id);
    //});

    $('.container').on('click', 'a.popupDelete', function (e) {
        e.preventDefault();
        data = oTable.row($(this).parents('tr')).data();
        var id = data.employeeId;
        alert(id);

    });

    function OpenPopup(pageUrl,id) {
        var $pageContent = $('<div/>');
        $pageContent.load(pageUrl, function () {
            $('#editForm');
            LoadDDLDepartement();
        });
        $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
            .html($pageContent)
            .dialog({
                draggable: false,
                autoOpen: false,
                resizable: false,
                model: true,
                modal: true,
                show: {
                    effect: "slide",
                    duration: 200
                },
                title: 'Employee Dialog',
                height: 350,
                width: 600,
                close: function () {
                    $(this).dialog('destroy').remove()
                }
            })

        $('.popupWindow').on('submit', '#editForm', function (e) {
            var url = id == null ? '/Employees/Create' : '/Employees/Edit/' + id;
            $.ajax({
                type: "POST",
                url: url,
                data: $('#editForm').serialize(),
                success: function (data) {
                    if (data.status) {
                        $dialog.dialog('close');
                        oTable.ajax.reload();
                    }
                }
            })
            e.preventDefault();
        })
        $dialog.dialog('open');
    }

    function Addnew() {
        OpenPopup('/Employees/_EditForm/');
    }

    function LoadDDLDepartement() {
        $.getJSON('/Data/GetDepartement', function (data) {
            var dropdown = $('#DepartmentId');
            $.each(data, function () {
                dropdown.append
                    (
                        $("<option></option>").text(this.text).val(this.value)
                    )
            });
        });
    }

    $(function () {
        $("#DateJoin").datepicker({ dateFormat: 'dd/mm/yyyy' });
    });
});