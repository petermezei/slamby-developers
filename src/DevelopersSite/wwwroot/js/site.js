(function () {
    buildMenu();

    $('#sidebar_col').affix({
        offset: {
            top: 0
        }
    });
    $(document.body).scrollspy({
        target: '#sidebar_col',
        offset: 0
    });
    // Add smooth scrolling to all links inside a navbar
    $("#sidebar a").on('click', function (event) {
        event.preventDefault();
        scrollToHash(this.hash);
    });

    // Add smooth scrolling to all links inside a documentation
    $("#documentation a").on('click', function (event) {

        if (!this.hash) {
            //event.preventDefault();
            //var href = $(this).attr("href");
            //window.location = window._currentUrl + '/' + href;
            return;
        }
        event.preventDefault();
        scrollToHash(this.hash.replace("/definitions/", "definition-"));
    });

    function scrollToHash(hash) {
        var offsetTop = 70;
        $('html, body').animate({
            scrollTop: ($(hash).offset().top - offsetTop)
        }, 200, function () {
            window.location.hash = hash;
        });
    }
})();



function buildMenu() {
    var headers = $('#documentation').find('h2,h3');
    var menuHtml = '';
    var firstH2 = true;

    $.each(headers, function (key, header) {
        var id = $(header).attr('id');
        if (!id) {
            return;
        }

        var tagName = $(header).prop('tagName').toLowerCase();
        var text = $(header).text();

        if (tagName == 'h2') {
            if (!firstH2) {
                menuHtml += '</ul></li>';
            }
            menuHtml += '<li><a href="#' + id + '">' + text + '</a><ul class="nav">';
            firstH2 = false;
        }
        else {
            menuHtml += '<li class="header-' + tagName + '" >' +
                            '<a href="#' + id + '">' + text + '</a>' +
                        '</li>';
        }
    });

    $("#sidebar").append(menuHtml);
}