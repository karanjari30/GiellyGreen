
(function () {
    var host = window.location.host;
    $('#logo').attr('href', '/swagger/ui/index')
    $('.logo__img').height(50);
    $('.logo__img').width(50);
    $('.logo__title').hide();

    document.title = 'API';

    $('#api_selector .input').hide();
    $('#swagger-ui-container').hide();


    $("textarea[name=objModel]").css("font-weight", "bold");
    function getJwt(loginData, success, error, complete) {
        console.log("loginData.path", loginData.path)
        $.ajax({
            url: loginData.path,
            type: 'POST',
            data: {
                grant_type: 'password',
                username: loginData.user,
                password: loginData.pass

            },
            success: function (data) {
                success && success(data.access_token);
            },
            error: function (jqXhr, err, msg) {
                error && error(JSON.parse(jqXhr.responseText).error_description);

            },
            complete: complete
        });
    }

    function setJwt(key) {
        swaggerUi.api.clientAuthorizations.authz = {};
        swaggerUi.api.clientAuthorizations.add("key", new
            SwaggerClient.ApiKeyAuthorization("Authorization",
            "Bearer " + key, "header"));

    }
    $(function () {
        (function () {
            var styles = '<style>#sa-setting{display:inline-block;' +
                    'position: absolute;background: #89BF04;right: 20px;' +
                    'top: 48px;padding: 10px;box-shadow: 0 2px 1px rgba(0,0,0,0.5);display: none;' +
                    '}.sa-btn{display: inline-block;color: #FFF;font-weight: bold;background: #547F00;border-radius: 3px;' +
                    'padding: 6px 8px;font-family: "Droid Sans", sans-serif;font-size: 0.9em;cursor: pointer;' +
                '}#sa-setting input{margin-bottom: 5px;padding: 3px;border: 2px inset;}' +
            '</style>';

            $('head').append(styles);
            var settingTemplate = '<div id="sa-setting"><input id="sa-path" placeholder="Path" value="/Token">' +
                    '<br><input id="sa-username" placeholder="Username"><br>' +
                    '<input id="sa-password" type="password" placeholder="Password">' +
                    '<br><span id="sa-btn-login" class="sa-btn">Login</span>' +
                    '<span id="sa-btn-logout" class="sa-btn" style="display: none">Logout</span>'
                + '</div>';

            $('body').append(settingTemplate);



            $('<div id="sa-btn-setting" class="sa-btn">Login</div>')
                .click(function () {
                    $('#sa-setting').fadeToggle();
                })
                .appendTo('#api_selector');

        })();


        function login(loginData) {
            $('#sa-btn-setting').text('Working...');
            getJwt(loginData, function (jwt) {
                $('#sa-btn-setting').text('Hi ' + loginData.user).css('background', '#0f6ab4');
                $('#sa-setting').fadeOut();
                $('#sa-btn-logout').fadeIn();
                $('#swagger-ui-container').show();
                setJwt(jwt);
            },
                function (err) {
                    $('#sa-btn-setting').text('Failed').css('background', '#a41e22');

                    setJwt('');
                }, function () {
                });
        }

        $('#sa-btn-logout').click(function () {
            setJwt('');
            window.localStorage.removeItem('sa-login-data');
            $('#sa-username').val('');
            $('#sa-password').val('');
            $(this).fadeOut();
            delete_cookie("access_token");
            window.location.reload();
        });

        $('#sa-btn-login').click(function () {
            var loginData = {
                path: $('#sa-path').val(),
                user: $('#sa-username').val(),
                pass: $('#sa-password').val()
            };
            window.localStorage.setItem('sa-login-data', JSON.stringify(loginData));
            login(loginData);
        });
        //Auto login
        (function () {
            $('#api_selector .input').hide();
            $('#swagger-ui-container').hide();
            var objCookies = getCookie("access_token");
            if (objCookies != null && objCookies.length > 0) {
                var myJSON = JSON.parse(objCookies);
                setJwt(myJSON.access_token);
                $('#sa-btn-setting').text('Hi ' + myJSON.userName).css('background', '#0f6ab4');
                $('#sa-setting').fadeOut();
                $('#sa-btn-logout').fadeIn();
                $('#swagger-ui-container').show();
            } else {
                var oldLoginData = window.localStorage.getItem('sa-login-data');
                if (oldLoginData) {
                    oldLoginData = JSON.parse(oldLoginData);
                    if (oldLoginData.path.indexOf('/ilinkairapi/') > -1) {
                        oldLoginData.path = oldLoginData.path.replace('/ilinkairapi/', '/')
                    }
                    console.log('oldLoginData.path',oldLoginData.path)
                    $('#sa-path').val(oldLoginData.path);
                    $('#sa-username').val(oldLoginData.user);
                    $('#sa-password').val(oldLoginData.pass);
                    login(oldLoginData);
                }
            }


        })();

    });
})();




function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}
function delete_cookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
};

$(document).ready(function () {
    
    var tokenText = `<p>Go to Body tab and select x-www-form-urlencoded as Content Type and add below three key-value pair.</p>`;
    tokenText += `<pre><code>{
  "username": "your_email",
  "password": "your_password",
  "grant_type": "password"
}</code></pre>`;
    //$('#Account_Account_Token_content .markdown')[0].append(tokenText);
    $("#Account_Account_Token_content div.markdown").eq(0).append(tokenText)
    $('#resource_Account #Account_Account_Token_content').show();
    $('#Account_Account_Token_content .sandbox_header').css("display", "none");
    $('#Account_Account_Token_content .response-content-type').css("display", "none");

    /*var RFIDModelString = '<pre><code>{\n  "DeviceId": 0,\n  "SerialNo": "string",\n  "StockNo": "string",\n  "Description": "string",\n  "SystemNo": "string",\n  "Type": "string",\n  "Classification": "string",\n  "GroupName": "string",\n  "GeofenceName": "string",\n  "Location": "string",\n  "Message": "string",\n  "TTFF": 0,\n  "PDOP": 0,\n  "SequenceId": 0,\n  "MessageDate": "string",\n  "Latitude": 0,\n  "Longitude": 0,\n  "MacAddress": "string"\n}</code></pre>';*/
    var RFIDHistoryModelString = '<pre><code>{\n  "DeviceId": 0,\n  "SerialNo": "string",\n  "StockNo": "string",\n  "Description": "string",\n  "SystemNo": "string",\n  "CPU_Serial": "string",\n  "CPU_PlateNo": "string",\n  "CPU_Description": "string",\n  "CPU_Driver": "string",\n  "CPU_Product": "string",\n  "Type": "string",\n  "Classification": "string",\n  "GroupName": "string",\n  "GeofenceName": "string",\n  "Location": "string",\n  "Message": "string",\n  "TTFF": 0,\n  "PDOP": 0,\n  "SequenceId": 0,\n  "MessageDate": "string",\n  "Latitude": 0,\n  "Longitude": 0,\n  "MacAddress": "string"\n}</code></pre>';
    $("#Assets_Assets_GetRFIDDevicesLastLocations_content div.snippet_json").html(RFIDHistoryModelString);
    $("#Assets_Assets_GetRFIDDevicesHistory_content div.snippet_json").html(RFIDHistoryModelString);
});