$(function() {
    var placeholderElement = $('#modal-placeholder').modal('handleUpdate');
    $(document).on('click',
        'a[data-toggle="modal"]',
        function(event) {
            var url = $(this).attr('data-url');
            $.get(url).done(function(data) {
                placeholderElement.html(data);
                placeholderElement.find('.modal').modal('show');
            });
        });
    placeholderElement.on('click',
        '[data-save="modal"]',
        function(event) {
            event.preventDefault();
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function(data) {
                
                var newBody = $('.modal-body', data);
                var isValid = newBody.find('[name="IsValid"]').val() == 'True';
                placeholderElement.find('.modal-body').replaceWith(newBody);

                // find IsValid input field and check it's value
                // if it's valid then hide modal window        
                if (isValid) {
                    var partial = $('#partial');
                    var partialUrl = partial.data('url');
                    AjaxCall(partialUrl).done(function(data) { 
                        partial.html(data);
                        placeholderElement.find('.modal').modal('hide');
                    });
                }
            });
        });
    placeholderElement.on('click',
        '[data-delete="modal"]',
        function(event) {
            event.preventDefault();
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function() {                
                var partial = $('#partial');
                var partialUrl = partial.data('url');
                AjaxCall(partialUrl).done(function(data) {
                    var findElement = $(data).find('div[data-find-element]');
                    console.log(findElement);
                    if (findElement.length !== 0) {
                        partial.replaceWith($(data).find('#partial'));
                    }else {
                        partial.html(data);
                    }
                    placeholderElement.find('.modal').modal('hide');
                });                
            });
        });
    $(placeholderElement).on('hidden.bs.modal',
        function() {
            // remove the bs.modal data attribute from it
            $(this).removeData('bs.modal');
            // and empty the modal-content element
            $(placeholderElement).empty();
            $(".modal-backdrop").remove();
        });
});
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
}