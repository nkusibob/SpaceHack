$(document).ready(function () {

  $('#menu li').click(function(){
  
    // POSITIONNER LA BARRE BLEUE
    $('#highlighter').animate({"left": $(this).offset().left},500);
  })
  $(".filter-button").click(function () {
    var value = $(this).attr('data-filter');

    if (value === "all") {

      $('.filter').show('1000');
      $(".clearDiv").removeClass('clearfix');
    }
    else {


      $(".filter").not('.' + value).hide('3000');
      $(".clearDiv").addClass('clearfix');
      $('.filter').filter('.' + value).show('3000');

    }
  });

  if ($(".filter-button").removeClass("active")) {
    $(this).removeClass("active");
  }
  $(this).addClass("active");

});
