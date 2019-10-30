$('.number').keypress(function (event) {
    var keycode = event.which;
    if (!(event.shiftKey === false && (keycode === 46 || keycode === 8 || keycode === 37 || keycode === 39 || (keycode >= 48 && keycode <= 57)))) {
        event.preventDefault();
    }
});
function geturl(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/“|”/g, "");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_|{|}|`/g, "-");
    str = str.replace(/ /g, "-");
    str = str.replace(/-+-/g, "-");
    str = str.replace(/^\-+|\-+$/g, "");
    return str;
}
$(".geturl").keypress(function () {
    document.getElementById('alias').value = geturl($(".geturl").val());
});
$(".geturl").keyup(function () {
    document.getElementById('alias').value = geturl($(".geturl").val());
});
function moveToPreviousPage(currentPage, formId) {
    moveToPage(currentPage - 1, formId);
}
function moveToNextPage(currentPage, formId) {
    moveToPage(currentPage + 1, formId);
}
function moveToPage(page, formId) {
    $(".page-to-move").val(page);
    $("#" + formId).submit();
}
$(document).ready(function () {
    $(".search").on("click", function () {
        $(".page-to-move").val(null);
        $(this).closest('form').submit();
    });
    $(".datepicker").datepicker({
        format: 'dd/mm/yyyy'
    });
});
function refresh(x) {
    window.location.href = "/Admin/" + x;
}
$("#thumbnail").click(function () {
    var ckfiner = new CKFinder();
    ckfiner.selectActionFunction = function (fileUrl) {
        $("#thumbnail").val(fileUrl);
    };
    ckfiner.popup();
});
$("#SiteBanner").click(function () {
    var ckfiner = new CKFinder();
    ckfiner.selectActionFunction = function (fileUrl) {
        $("#SiteBanner").val(fileUrl);
    };
    ckfiner.popup();
});
$("#SiteBannerEn").click(function () {
    var ckfiner = new CKFinder();
    ckfiner.selectActionFunction = function (fileUrl) {
        $("#SiteBannerEn").val(fileUrl);
    };
    ckfiner.popup();
});
$('.sidebar-menu  li a[href="' + location.pathname + '"]').parent('li').addClass('active');
$('.sidebar-menu li:has(li.active)').addClass('active');
$('.sidebar-menu  a[href="' + location.pathname + '"]').parent('li').addClass('active');

function trash(x, y) {
    var check = confirm('Bạn có chắn chắn muốn xóa không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/NewsPaper/Trash",
                data: { id: x },
                datatype: "json",
                success: function (data) {
                    window.location.href = "/Admin/" + y;
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}
function Approval(x, y) {
    var check = confirm('Bạn có chắn chắn muốn duyệt không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/NewsPaper/Approval",
                data: { id: x },
                datatype: "json",
                success: function (data) {
                    window.location.href = "/Admin/" + y;
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}
function UnApproval(x, y) {
    var check = confirm('Bạn có chắn chắn muốn hủy duyệt không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/NewsPaper/UnApproval",
                data: { id: x },
                datatype: "json",
                success: function (data) {
                    window.location.href = "/Admin/" + y;
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}

function setLanguage(x) {
    $.ajax({
        type: "GET",
        url: "/Admin/Default/_setLanguage",
        data: { _languageId: x },
        datatype: "json",
        success: function (data) {
            window.location.reload(true);
        }
    });
}

function trashFeedbackDetail(x) {
    var check = confirm('Bạn có chắn chắn muốn xóa không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/YKienDongGop/TrashDetail",
                data: { Id: x },
                datatype: "json",
                success: function (data) {
                    window.location.reload(true);
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}
function trashFeedback(x) {
    var check = confirm('Bạn có chắn chắn muốn xóa không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/YKienDongGop/Trash",
                data: { Id: x },
                datatype: "json",
                success: function (data) {
                    window.location.reload(true);
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}
function trashLienKet(x) {
    var check = confirm('Bạn có chắn chắn muốn xóa không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/LienKetWeb/Trash",
                data: { Id: x },
                datatype: "json",
                success: function (data) {
                    if (data)
                        window.location.reload(true);
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}
function trashMenu(x) {
    var check = confirm('Bạn có chắn chắn muốn xóa không?');
    if (check) {
        if (x > 0) {
            $.ajax({
                type: "GET",
                url: "/Admin/Menu/Trash",
                data: { Id: x },
                datatype: "json",
                success: function (data) {
                    if (data)
                        window.location.reload(true);
                }
            });
        }
        else {
            alert("Lỗi không thể xóa!");
        }
    }
}
function ApprovalDetail(x) {
    if (x > 0) {
        $.ajax({
            type: "GET",
            url: "/Admin/YKienDongGop/ApprovalDetail",
            data: { Id: x },
            datatype: "json",
            success: function (data) {
                window.location.reload(true);
            }
        });
    }
}
function UnApprovalDetail(x) {
    if (x > 0) {
        $.ajax({
            type: "GET",
            url: "/Admin/YKienDongGop/UnApprovalDetail",
            data: { Id: x },
            datatype: "json",
            success: function (data) {
                window.location.reload(true);
            }
        });
    }
}
function logout() {
    $.ajax({
        type: "GET",
        url: "/Account/LogOff",
        datatype: "json",
        success: function (data) {
            window.location.reload(true);
        }
    });
}