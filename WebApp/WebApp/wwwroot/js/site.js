// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var careersTB = $("#careers-tb").DataTable();

    yadcf.init(careersTB,
        [
            {
                column_number: 2,
                filter_type: "select",
                filter_default_label: "Todas"
            },
            {
                column_number: 5,
                filter_type: "select",
                filter_default_label: "Todas",
                data: ["Activo", "Inactivo"]
            }
        ],
        {
            cumulative_filtering: true
        }
    );

    $("#yadcf-filter-wrapper--careers-tb-2").insertBefore("#school-head");
    $("#yadcf-filter-wrapper--careers-tb-5").insertBefore("#state-head");

    $(".yadcf-filter-reset-button").remove();
});