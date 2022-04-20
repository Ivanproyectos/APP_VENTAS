//(function($) {
//    "use strict"

    //gradient bar chart
    function CargarGrafico_VentasMes(Labels,Data){
       const barChart_2 = document.getElementById("barChart_2").getContext('2d');
            //generate gradient
       const barChart_2gradientStroke = barChart_2.createLinearGradient(0, 0, 0, 250);
            barChart_2gradientStroke.addColorStop(0, "rgba(64, 120, 171, 1)");
            barChart_2gradientStroke.addColorStop(1, "rgba(64, 120, 171, 0.5)");
            barChart_2.height = 100;
            new Chart(barChart_2, {
                type: 'bar',
                data: {
                    defaultFontFamily: 'Poppins',
                    labels: Labels,
                    datasets: [
                        {
                            label: "Monto",
                            data: Data,
                            borderColor: barChart_2gradientStroke,
                            borderWidth: "0",
                            backgroundColor: barChart_2gradientStroke, 
                            hoverBackgroundColor: barChart_2gradientStroke
                        }
                    ]
                },
                options: {
                    legend: false, 
                    plugins: {
                        datalabels: {
                            display: false, 
                        }},
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }],
                        xAxes: [{
                            // Change here
                            barPercentage: 0.5
                        }]
                    }
                }
            });
    }
    //pie chart
    function CargarGrafico_TipoPago(Labels,Data){
        const pie_chart = document.getElementById("pie_chart").getContext('2d');
            pie_chart.height = 100;
            new Chart(pie_chart, {
                type: 'pie',
                data: {
                    defaultFontFamily: 'Poppins',
                    datasets: [{
                        data: Data,
                        borderWidth: 0, 
                        backgroundColor: [
                            "rgba(232, 41, 41, .9)", // al credito
                            "rgba(64, 120, 171 , .8)", // contado
                            "rgba(57, 201, 192, .8)" // deposito
                        ],
                        hoverBackgroundColor: [
                            "rgba(232, 41, 41, 1)",
                            "rgba(64, 120, 171 , 1)",
                            "rgba(57, 201, 192, 1)"
                        ]

                    }],
                    labels: Labels
                },
                options: {
                    legend: {
                        position: 'right',
                        display: true,
                    },
                    responsive: true, 
                    tooltips : {
                        enabled: false
                    },
                    plugins: {           
                        datalabels: {
                            color: '#fff',
                            display: true, 
                            formatter: function (value, ctx) {
                                debugger;
                                return ((value * 100) / total(ctx)).toFixed(2) + '%'; 
                            },
                        },
                    },
                    maintainAspectRatio: false
                }
            });

    }
    function total(chart){
        let data = chart.chart.data.datasets[0].data;
        const reducer = (accumulator, currentValue) => accumulator + currentValue;
        var total = data.reduce(reducer);
        return total;
    }

//dual line chart
    function CargarGrafico_ComparativaLine(Labels, DataLine1,DataLine2){
    const lineChart_3 = document.getElementById("lineChart_3").getContext('2d');
        //generate gradient
        lineChart_3.height = 100;

        new Chart(lineChart_3, {
            type: 'line',
            data: {
                defaultFontFamily: 'Poppins',
                labels: Labels,
                datasets: [
                    {
                        label: "Producto",
                        data: DataLine1,
                        borderColor: 'rgba(0, 0, 128 , 0.8)',
                        borderWidth: "2",
                        backgroundColor: 'transparent', 
                        pointBackgroundColor: 'rgba(0, 0, 128 , 1)'
                    }, {
                        label: "Servicio",
                        data: DataLine2,
                        borderColor: 'rgba(57, 201, 192, 0.8)',
                        borderWidth: "2",
                        backgroundColor: 'transparent', 
                        pointBackgroundColor: 'rgba(57, 201, 192, 1)'
                    }
                ]
            },
            options: {
                responsive: true,
                //legend: true, 
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    datalabels: {
                        display: false, 
                    }
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true, 
                            //max: 100, 
                            min: 0, 
                            stepSize: 20, 
                            padding: 10
                        }
                    }],
                    xAxes: [{ 
                        ticks: {
                            padding: 5
                        }
                    }]
                }
            }
        });
    
    }
      
//doughut chart
    function CargarGrafico_ProductoMv(Labels,Data){
        const doughnut_chart = document.getElementById("doughnut_chart").getContext('2d');
         doughnut_chart.height = 100;
        new Chart(doughnut_chart, {
            type: 'doughnut',
            data: {
                defaultFontFamily: 'Poppins',
                datasets: [{
                    data: Data,
                    borderWidth: 0, 
                    backgroundColor: [
                        "rgba(0, 0, 128, .9)",
                        "rgba(0, 0, 128, .8)",
                        "rgba(0, 0, 128, .6)",
                        "rgba(0, 0, 128, .5)",
                        "rgba(0, 0, 128, .4)"
                    ],
                    hoverBackgroundColor: [
                        "rgba(0, 0, 128, .7)",
                        "rgba(0, 0, 128, .4)",
                        "rgba(0, 0, 128, .3)",
                        "rgba(0, 0, 128, .2)",
                        "rgba(0, 0, 128, .2)"
                    ]

                }],
                labels:Labels
            },
            options: {
                legend: {
                    position: 'right',
                    display: true,
                },
                responsive: true,
                plugins: {
                    datalabels: {
                        color: '#FFFFFF',
                        display: true, },
                  },
                maintainAspectRatio: false
            }
        });
    }

//})(jQuery);