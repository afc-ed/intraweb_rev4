
// --------------- DIRECTIVE ------------------------

// displays encoded html.
app.directive("compileHtml", function($parse, $sce, $compile)
{
    return {
        restrict: "A",
        link: function (scope, element, attributes)
        { 
            var expression = $sce.parseAsHtml(attributes.compileHtml);
            var getResult = function ()
            {
                return expression(scope);
            }; 
            scope.$watch(getResult, function (newValue)
            {
                var linker = $compile(newValue);
                element.append(linker(scope));
            });
        }
    }
});

// uses jquery datepicker.
app.directive('jqdatepicker', function()
{
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl)
        {
            element.bind('mouseover', function ()
            {
                 element.css('cursor', 'pointer');
            });
           $(element).datepicker(
            {
                dateFormat: 'mm/dd/yy',
                changeMonth: true,
                changeYear: true,
                onSelect: function(date)
                {
                    ctrl.$setViewValue(date);
                    ctrl.$render();
                    scope.$apply();
                }
            });
       }     
    };
});

// history back function.
app.directive('pageback', function(){
    return {
      restrict: 'A',
      link: function(scope, element, attrs)
      {
        element.bind('click', pageBack);
        function pageBack()
        {
          window.history.back();
          scope.$apply();
        }
      }
    };
});
