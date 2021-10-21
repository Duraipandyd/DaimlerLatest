(function($) {

    'use strict';

    $(document).ready(function() {
        // Initializes search overlay plugin.
        // Replace onSearchSubmit() and onKeyEnter() with
        // your logic to perform a search and display results
        
        $(".list-view-wrapper").scrollbar();

        $('[data-pages="search"]').search({
            // Bind elements that are included inside search overlay
            searchField: '#overlay-search',
            closeButton: '.overlay-close',
            suggestions: '#overlay-suggestions',
            brand: '.brand',
             // Callback that will be run when you hit ENTER button on search box
            onSearchSubmit: function(searchString) {
                console.log("Search for: " + searchString);
            },
            // Callback that will be run whenever you enter a key into search box.
            // Perform any live search here.
            onKeyEnter: function(searchString) {
                console.log("Live search for: " + searchString);
                var searchField = $('#overlay-search');
                var searchResults = $('.search-results');

                /*
                    Do AJAX call here to get search results
                    and update DOM and use the following block
                    'searchResults.find('.result-name').each(function() {...}'
                    inside the AJAX callback to update the DOM
                */

                // Timeout is used for DEMO purpose only to simulate an AJAX call
                clearTimeout($.data(this, 'timer'));
                searchResults.fadeOut("fast"); // hide previously returned results until server returns new results
                var wait = setTimeout(function() {

                    searchResults.find('.result-name').each(function() {
                        if (searchField.val().length != 0) {
                            $(this).html(searchField.val());
                            searchResults.fadeIn("fast"); // reveal updated results
                        }
                    });
                }, 500);
                $(this).data('timer', wait);

            }
        })

    });


    $('.panel-collapse label').on('click', function(e){
        e.stopPropagation();
    })

    // auto init for parallax sets window as scrollElement.
    // set .page-container as scrollElement for horizontal layouts.
    $('.jumbotron').parallax({
      scrollElement: '.page-container'
    })

    $('.page-container').on('scroll', function() {
        $('.jumbotron').parallax('animate');
    });

})(window.jQuery);
$(document).ready(function() {
  $('#manageusers').DataTable({
	  "bInfo" : false,
	  "lengthChange": false
  });
  
  
  
  
  
     // $('#adduserform').validate({
        // rules: {
            // name: {
                // required: true,
                // maxlength: 40,
                // alphaspaceonly: true
            // },
            // email: {
                // required: true,
                // email: true,
                // maxlength: 64
            // },
            // phone: {
                // digits: true,
                // minlength: 7,
                // maxlength: 11,
                // required: true,

            // },
            // designation: {
                // required: true,
                // maxlength: 64
            // },
            
        // },
        ///////For custom messages
        // messages: {
            // name: {
                // required: "Name cannot be empty",
                // maxlength: "Name cannot exceed 40 characters",
                // alphaspaceonly: "Name should contain only alphabets"
            // },
            // email: {
                // required: "Email cannot be empty",
                // email: "Enter a valid Email ",
                // maxlength: "Email cannot exceed 64 characters"
            // },
            // phone: {
              // required: "Phone Number cannot be empty",
                // digits: "Phone Number can only contain Numerics",
                // minlength: "Phone Number cannot be less than 7 digits",
                // maxlength: "Phone Number cannot exceed 11 digits"
            // },
            // designation: {
                // required: "Designation cannot be empty",
                // maxlength: "Designation cannot exceed more than 64 characters"
            // },
           
        // },
        // errorElement: 'div',
        // errorPlacement: function (error, element) {
            // var placement = $(element).data('error');
            // if (placement) {
                // $(placement).append(error)
            // } else {
                // error.insertAfter(element);
            // }
        // }
    // });
    var chart = new CanvasJS.Chart("chartContainer",
    {
      title:{
        text: "BE Records",
        fontFamily: "Impact",
        fontWeight: "normal",
        credits:false,

      },
      legend:{
        verticalAlign: "bottom",
        horizontalAlign: "center"
      },
      data: [
      {
        //startAngle: 45,
       indexLabelFontSize: 20,
       indexLabelFontFamily: "Garamond",
       indexLabelFontColor: "darkgrey",
       indexLabelLineColor: "darkgrey",
       indexLabelPlacement: "outside",
       type: "doughnut",
       showInLegend: true,
       dataPoints: [
       {  y: 53.37, legendText:"Data1 53%", indexLabel: "Data1 53%" },
       {  y: 35.0, legendText:"Data2 35%", indexLabel: "Data2 35%" },
       {  y: 7, legendText:"Data3 7%", indexLabel: "Data3 7%" },
       {  y: 2, legendText:"Data4 2%", indexLabel: "Data4 Phone 2%" },
       {  y: 5, legendText:"Data5 5%", indexLabel: "Data5 5%" }
       ]
     }
     ]
   });

    chart.render();
    chart.credits().enabled(false);

} );
//selecting all required elements
const dropArea = document.querySelector(".drag-area"),
dragText = dropArea.querySelector("header"),
button = dropArea.querySelector("button"),
input = dropArea.querySelector("input");
let file; //this is a global variable and we'll use it inside multiple functions

button.onclick = ()=>{
  input.click(); //if user click on the button then the input also clicked
}

input.addEventListener("change", function(){
  //getting user select file and [0] this means if user select multiple files then we'll select only the first one
  file = this.files[0];
  dropArea.classList.add("active");
  showFile(); //calling function
  $('.customload').show()
  $('.customfileupload .errormsg').hide()
  $('.customfileupload .successmsg').hide()
  setTimeout(function(){ 
    $('.customload').hide()
    $('.successmsg').show()

 }, 3000);
});


//If user Drag File Over DropArea
dropArea.addEventListener("dragover", (event)=>{
  event.preventDefault(); //preventing from default behaviour
  dropArea.classList.add("active");
  dragText.textContent = "Release to Upload File";
});

//If user leave dragged File from DropArea
dropArea.addEventListener("dragleave", ()=>{
  dropArea.classList.remove("active");
  dragText.textContent = "Drag & Drop to Upload File";
});

//If user drop File on DropArea
dropArea.addEventListener("drop", (event)=>{
  event.preventDefault(); //preventing from default behaviour
  //getting user select file and [0] this means if user select multiple files then we'll select only the first one
  file = event.dataTransfer.files[0];
  showFile(); //calling function
 });

function showFile(){
    console.log(file.type)
  let fileType = file.type; //getting selected file type
  let validExtensions = ["application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"]; //adding some valid image extensions in array
  if(validExtensions.includes(fileType)){ //if user selected file is an image file
    let fileReader = new FileReader(); //creating new FileReader object
    fileReader.onload = ()=>{
      let fileURL = fileReader.result; //passing user file source in fileURL variable
      $('.customload').show()
      setTimeout(function(){ 
        $('.customload').hide()
        $('.customfileupload .errormsg').hide()

        $('.customfileupload .successmsg').show()
    
     }, 3000);
     dragText.textContent = "Drag & Drop to Upload File";

        // UNCOMMENT THIS BELOW LINE. I GOT AN ERROR WHILE UPLOADING THIS POST SO I COMMENTED IT
    //   let imgTag = `<img src="${fileURL}" alt="image">`; //creating an img tag and passing user selected file source inside src attribute
    //   dropArea.innerHTML = imgTag; //adding that created img tag inside dropArea container
    }
    fileReader.readAsDataURL(file);
  }else{
    $('.customfileupload .successmsg').hide()
    $('.customfileupload .errormsg').show()
     $('#errorlog').modal('show');
    dropArea.classList.remove("active");
    dragText.textContent = "Drag & Drop to Upload File";
  }
} 



function showBillFile(){
	$('#bill_list').modal('show');
}