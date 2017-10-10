var viagogo = function($) {

    var ticketsUrl = "/Home/GetListings/#id";

    function fetchListings(e) {
        var id = $(e).data("event-id");
        var endpoint = ticketsUrl.replace("#id", id);
        var targetElement = $(e).data("target-element");

        $("#" + targetElement).removeClass("hidden").load(endpoint);
    }


    function init() {
        $(".event-view-more").click(function() { fetchListings(this); });
    }

    $(init);
}(jQuery);