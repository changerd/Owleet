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
            var button = this;
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function(data) {

                var newBody = $('.modal-body', data);
                if (! button.hasAttribute('data-delete')) {
                    var isValid = newBody.find('[name="IsValid"]').val() == 'True';
                } else {
                    var isValid = newBody.find('[name="IdErrorMessage"]').text() == '';
                }

                placeholderElement.find('.modal-body').replaceWith(newBody);

                // find IsValid input field and check it's value
                // if it's valid then hide modal window        
                if (isValid) {
                    var tableElement = $('#main-table');
                    var tableUrl = tableElement.data('url');
                    AjaxCall(tableUrl).done(function(data) {
                        var options = table.options;
                        $('#partialTable').html(data);
                        SetDataTable(table);
                        placeholderElement.find('.modal').modal('hide');
                    });
                }
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