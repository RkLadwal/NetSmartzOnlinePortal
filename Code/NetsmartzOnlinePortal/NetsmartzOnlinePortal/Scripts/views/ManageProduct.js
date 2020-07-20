$(function () {
    //datatable
    if ($('#search').val().length > 0) {
        $('#fafatimes').css('display', 'block');
        $("#faSearch").css('display', 'none');
    }

    $('#search').keyup(function () {
        if ($('#search').val().length == 0) {
            $('#fafatimes').css('display', 'none');
            $("#faSearch").css('display', 'block');
        }
        else {
            $('#fafatimes').css('display', 'block');
            $("#faSearch").css('display', 'none');
        }
    });

    $('#fafatimes').on('click', function () {
        $('#search').val('');
        $('#fafatimes').css('display', 'none');
        $("#faSearch").css('display', 'block');
        BindJqueryDatatable();
    });

    /* Load Jquery datatable*/
    function BindJqueryDatatable() {
        var tableColumns = [
            { data: "CategoryName" },
            { data: "ProductName" },
            { data: "Description" },
            { data: "Quantity" },
            { data: "Price" },
            { data: "Discount" },
            { data: "strExpirationDate" },
            { data: "ProductId" }

        ];
        var oTable = $('#jqueryDataTable').DataTable({
            "processing": true,
            "serverSide": true,
            "filter": true,
            "orderMulti": true,
            "pageLength": 10,
            "sPaginationType": "full_numbers",
            "bInfo": false, // used to hide Showing 1 to n enteries
            "destroy": true,
            "aaSorting": [[0, 'asc']],
            "ajax": {
                "url": "GetAllProducts",
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                }
            },
            "aoColumns": tableColumns,
            "columnDefs": [
                {
                    "targets": 7,
                    "bSortable": false,
                    "searching": false,
                    "orderable": false,
                    "render": function (data, type, full, meta) {
                        if (data != null) {
                            let editCat = '<a href="/user/manageprod/' + data + '"><i class="fa fa-edit cursor-pointer" title="' + full.data + '"></a></i></a>  ';
                            var deleteCat = '<a href="/user/DeleteProduct/' + data + '"><i class="fa fa-trash cursor-pointer" title="' + full.data + '"></a></i></a>';
                            return editCat + deleteCat;
                        }
                        return "N/A";
                    },
                },
            ],
            "initComplete": function (settings, json) {
                $("#jqueryDataTable_filter").hide(); // Hide default search button of jquery datatable
                $("#jqueryDataTable_length").addClass("m-t-20");
                $("#jqueryDataTable_length").find("select").addClass("form-control form-control-sm");
            },
            "oLanguage": {
                "sLengthMenu": "_MENU_ ",
                "sProcessing": "<div id='dvloader_processing'></div>"
            },
            "dom": '<"top"i>rt<"bottom"flp><"clear">' // Change page size position to bottom of table
        });
    }

    BindJqueryDatatable();

    $("#search").on('keyup', function () {
        $('#jqueryDataTable').dataTable().fnFilter(this.value);
    });
});