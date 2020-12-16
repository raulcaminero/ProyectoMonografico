// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var campusTB = $("#campus-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(campusTB,
                    [
                        {
                            column_number: 3,
                            filter_type: "select",
                            filter_default_label: "Todos",
                            case_insensitive: false,
                            data: ["Activo", "Inactivo"]
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--campus-tb-3").insertBefore("#cp-state-head");
                $("#yadcf-filter-wrapper--campus-tb-3").insertBefore("#cp-state-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });

    var careersTB = $("#careers-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(careersTB,
                    [
                        {
                            column_number: 1,
                            filter_type: "select",
                            filter_default_label: "Todas"
                        },
                        {
                            column_number: 2,
                            filter_type: "select",
                            filter_default_label: "Todas"
                        },
                        {
                            column_number: 4,
                            filter_type: "select",
                            filter_default_label: "Todos",
                            case_insensitive: false,
                            data: ["Activo", "Inactivo"]
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--careers-tb-1").insertBefore("#cr-faculty-head");
                $("#yadcf-filter-wrapper--careers-tb-2").insertBefore("#cr-school-head");
                $("#yadcf-filter-wrapper--careers-tb-4").insertBefore("#cr-state-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });

    var schoolsTB = $("#schools-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(schoolsTB,
                    [
                        {
                            column_number: 1,
                            filter_type: "select",
                            filter_default_label: "Todas"
                        },
                        {
                            column_number: 3,
                            filter_type: "select",
                            filter_default_label: "Todos",
                            case_insensitive: false,
                            data: ["Activo", "Inactivo"]
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--schools-tb-1").insertBefore("#sc-faculty-head");
                $("#yadcf-filter-wrapper--schools-tb-3").insertBefore("#sc-state-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });

    var facultiesTB = $("#faculties-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(facultiesTB,
                    [
                        {
                            column_number: 5,
                            filter_type: "select",
                            filter_default_label: "Todos",
                            case_insensitive: false,
                            data: ["Activo", "Inactivo"]
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--faculties-tb-5").insertBefore("#fc-state-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });

    var reqsTB = $("#reqs-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(reqsTB,
                    [
                        {
                            column_number: 4,
                            filter_type: "select",
                            filter_default_label: "Todos",
                            case_insensitive: false,
                            data: ["Activo", "Inactivo"]
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--reqs-tb-4").insertBefore("#rq-state-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });
});