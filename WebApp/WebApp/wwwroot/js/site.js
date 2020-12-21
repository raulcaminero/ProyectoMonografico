// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            }
        });

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
                            column_number: 1,
                            filter_type: "select",
                            filter_default_label: "Todos"
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

                $("#yadcf-filter-wrapper--reqs-tb-1").insertBefore("#rq-type-head");
                $("#yadcf-filter-wrapper--reqs-tb-2").insertBefore("#rq-school-head");
                $("#yadcf-filter-wrapper--reqs-tb-4").insertBefore("#rq-state-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });

    var servicesTB = $("#services-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(servicesTB,
                    [
                        {
                            column_number: 2,
                            filter_type: "select",
                            filter_default_label: "Todos"
                        },
                        {
                            column_number: 3,
                            filter_type: "select",
                            filter_default_label: "Todos"
                        },
                        {
                            column_number: 4,
                            filter_type: "select",
                            filter_default_label: "Todas"
                        },
                        {
                            column_number: 5,
                            filter_type: "select",
                            filter_default_label: "Todas"
                        },
                        {
                            column_number: 6,
                            filter_type: "select",
                            filter_default_label: "Todas"
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--services-tb-2").insertBefore("#sv-type-head");
                $("#yadcf-filter-wrapper--services-tb-3").insertBefore("#sv-campus-head");
                $("#yadcf-filter-wrapper--services-tb-4").insertBefore("#sv-faculty-head");
                $("#yadcf-filter-wrapper--services-tb-5").insertBefore("#sv-school-head");
                $("#yadcf-filter-wrapper--services-tb-6").insertBefore("#sv-career-head");


                $(".yadcf-filter-reset-button").remove();
            }
        });

    var gradesTB = $("#grades-tb").DataTable(
        {
            language:
            {
                url: 'https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json'
            },
            initComplete: function () {
                yadcf.init(gradesTB,
                    [
                        {
                            column_number: 0,
                            filter_type: "select",
                            filter_default_label: "Todos"
                        },
                        {
                            column_number: 1,
                            filter_type: "select",
                            filter_default_label: "Todos"
                        }
                    ],
                    {
                        cumulative_filtering: true
                    }
                );

                $("#yadcf-filter-wrapper--grades-tb-0").insertBefore("#gd-student-head");
                $("#yadcf-filter-wrapper--grades-tb-1").insertBefore("#gd-module-head");

                $(".yadcf-filter-reset-button").remove();
            }
        });
});