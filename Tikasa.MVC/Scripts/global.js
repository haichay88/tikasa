var root = "http://localhost:8111/";
    var CommonUtils = {
        showWait: function (val) {
            if (val)
                $('#loading').show();
            else {
                setTimeout(function () {
                    $('#loading').hide();
                }, 1000);
            }
               
        },
        showMessage: function (msg) {
            $(".alert").text(msg);
            $(".alert").alert();
        },
        RootUrl: function (url) {
            return root + url;
        },
        token: function () {
            var context = localStorage.getItem('context');
            return context;
        }
};

function formatTime(str) {
    var dur = moment.duration(parseInt(str, 10), 'seconds');
    var minutes = dur.minutes();
    var seconds = dur.seconds();
    return "00:" + minutes + ":" + seconds;
    }

function formatNumber(str) {
    return new Intl.NumberFormat().format(str);
    return str;
}
