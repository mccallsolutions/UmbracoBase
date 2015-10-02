main = {

    websiteLogin: {

        handleAjaxAntiForgeryToken: function (xhr) {
            
            // Add anti forgery token to header so it works with AJAX http://richiban.wordpress.com/2013/02/06/validating-net-mvc-4-anti-forgery-tokens-in-ajax-requests/
            var securityToken = $('[name=__RequestVerificationToken]').val();
            xhr.setRequestHeader('__RequestVerificationToken', securityToken);
        },

        changeUrlOnSuccess: function (data) {
            
            // If data Key is true then redirect to URL in Value
            if (data.Key !== undefined && data.Key === true) {
                window.location = data.Value;
            }
        }
    }
};