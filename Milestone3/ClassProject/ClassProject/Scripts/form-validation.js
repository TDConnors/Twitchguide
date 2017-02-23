// Wait for the DOM to be ready
$(function() {
  // Initialize form validation on the registration form.
  // It has the name attribute "registration"
  $("form[name='reviewForm']").validate({
    // Specify validation rules
    rules: {
      // The key name on the left side is the name attribute
      // of an input field. Validation rules are defined
      // on the right side
      ReviewerName: "required",
      EmailAddress: {
        required: true,
        email: true
      },
	  Rating: {
		required: true,
		min: 1,
		max: 5
	  },
	  ProductID: "required",
	  Comments: "required"
    },
    // Specify validation error messages
    messages: {
      ReviewerName: "Please enter your name",
      EmailAddress: "Please enter a valid email address",
	  Rating: "Please enter a rating between 1 and 5",
	  ProductID: "Product ID is required",
	  Comments: "You haven't told us anything yet!"
    },
    // Make sure the form is submitted to the destination defined
    // in the "action" attribute of the form when valid
    submitHandler: function(form) {
      form.submit();
    }
  });
});