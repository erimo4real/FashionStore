'use strict';
$(document).ready(function () {

    var chart_default_colors = ['#7689f1','#fcddde', '#b1cddb', '#ccf5eb', '#fdf2cd','#d0d4d9',  '#ffa5c8', '#9aaebd','#25d366', '#0077b5', '#37474f'];

    var Charts = {

        chart1: function () {
            if ($('#chart1').length) {
                var chart = echarts.init(document.getElementById('chart1'));
                var option = {
                    color: chart_default_colors,
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    calculable: true,
                    grid: {
                        top: 30,
                        left: 25,
                        right: 40,
                        bottom: 20,
                        containLabel: true
                    },
                    xAxis: [
                        {
                            type: 'category',
                            data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
                            axisTick: {
                                show: false
                            },
                            splitLine: {
                                lineStyle: {
                                    color: '#e9ebee'
                                }
                            },
                            axisLine: {
                                lineStyle: {
                                    color: '#e9ebee'
                                }
                            },
                            axisLabel: {
                                margin: 20,
                                fontFamily: 'Open Sans',
                                fontSize: 14,
                                color: '#939daa'
                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            axisTick: {
                                show: false
                            },
                            splitLine: {
                                lineStyle: {
                                    color: '#e9ebee'
                                }
                            },
                            axisLine: {
                                lineStyle: {
                                    color: '#e9ebee'
                                }
                            },
                            axisLabel: {
                                margin: 20,
                                fontFamily: 'Open Sans',
                                fontSize: 14,
                                color: '#939daa'
                            }
                        }
                    ],
                    series: [
                        {
                            type: 'bar',
                            barWidth: '70%',
                            data: [120, 52, 200, 334, 390, 330, 220],
                            label: {
                                normal: {
                                    show: true,
                                    position: 'insideTop'
                                }
                            }
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart2: function () {
            if ($('#chart2').length) {
                var chart = echarts.init(document.getElementById('chart2'));
                var option = {
                    color: chart_default_colors,
                    legend: null,
                    tooltip: {
                        show: true,
                        formatter: function (params) {
                            var id = params.dataIndex;
                            return cities[id] + '<br>最低：' + data[id][0] + '<br>最高：' + data[id][1] + '<br>平均：' + data[id][2];
                        }
                    },
                    series: [
                        {
                            name: '1',
                            type: 'pie',
                            hoverAnimation: false,
                            radius: ['80%', '100%'],
                            avoidLabelOverlap: false,
                            label: false,
                            labelLine: {
                                normal: {
                                    show: false
                                }
                            },
                            data: [
                                {
                                    value: 15,
                                    name: '1',
                                    itemStyle: {
                                        normal: {
                                            color: '#3b5998'
                                        }
                                    }
                                },
                                {
                                    value: 15,
                                    name: '2',
                                    itemStyle: {
                                        normal: {
                                            color: '#db4437'
                                        }
                                    }
                                },
                                {
                                    value: 25,
                                    name: '3',
                                    itemStyle: {
                                        normal: {
                                            color: '#0077b5'
                                        }
                                    }
                                },
                                {
                                    value: 15,
                                    name: '3',
                                    itemStyle: {
                                        normal: {
                                            color: '#55acee'
                                        }
                                    }
                                },
                                {
                                    value: 15,
                                    name: '3',
                                    itemStyle: {
                                        normal: {
                                            color: '#517fa4'
                                        }
                                    }
                                },
                                {
                                    value: 40,
                                    name: '3',
                                    itemStyle: {
                                        normal: {
                                            color: '#25d366'
                                        }
                                    }
                                }
                            ]
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart3: function () {
            if ($('#chart3').length) {
                var chart = echarts.init(document.getElementById('chart3'));
                var option = {
                    color: chart_default_colors,
                    title: null,
                    legend: null,
                    toolbox: null,
                    tooltip: {
                        trigger: 'axis'
                    },
                    grid: {
                        top: 0,
                        right: 0,
                        left: 0,
                        bottom: 30
                    },
                    xAxis: [
                        {
                            type: 'category',
                            boundaryGap: false,
                            data: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31],
                            axisLine: {
                                show: false,
                                lineStyle: {}
                            },
                            axisTick: {
                                show: false
                            },
                            axisLabel: {
                                margin: 10,
                                fontFamily: 'Open Sans',
                                color: function (value, index) {
                                    if (((index + 1) % 7 === 0) || (index + 1 === 1)) {
                                        return '#111111';
                                    }

                                    return '#939daa';
                                },
                                fontSize: 12
                            }
                        }
                    ],
                    yAxis: [
                        {
                            show: false
                        }
                    ],
                    series: [
                        {
                            name: 'Foreign',
                            type: 'line',
                            stack: 'Foreign',
                            showSymbol: false,
                            smooth: true,
                            areaStyle: {
                                normal: {
                                    opacity: 1
                                }
                            },
                            data: [242, 232, 251, 264, 320, 360, 390, 250, 232, 211, 264, 390, 360, 410, 260, 282, 271, 264, 360, 380, 410, 350, 232, 241, 354, 340, 370, 410, 290, 330, 350]
                        },
                        {
                            name: 'Another Index',
                            type: 'line',
                            stack: 'Another Index',
                            showSymbol: false,
                            smooth: true,
                            areaStyle: {
                                normal: {
                                    opacity: 1
                                }
                            },
                            data: [220, 182, 191, 234, 290, 330, 310, 220, 182, 191, 234, 290, 330, 310, 220, 232, 241, 244, 290, 330, 310, 220, 182, 191, 234, 290, 330, 310, 220, 182, 191]
                        },
                        {
                            name: 'Your Index',
                            type: 'line',
                            showSymbol: false,
                            stack: 'Your Index',
                            smooth: true,
                            areaStyle: {
                                normal: {
                                    opacity: 1
                                }
                            },
                            data: [120, 132, 101, 134, 90, 230, 210, 120, 132, 101, 134, 90, 230, 210, 134, 90, 230, 210, 120, 132, 101, 132, 101, 134, 90, 230, 210, 120, 132, 101, 134]
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart4: function () {
            if ($('#chart4').length) {
                var chart = echarts.init(document.getElementById('chart4'));
                var option = {
                    color: chart_default_colors,
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'cross',
                            label: {
                                backgroundColor: '#283b56'
                            }
                        }
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            dataView: {readOnly: false},
                            restore: {},
                            saveAsImage: {}
                        }
                    },
                    dataZoom: {
                        show: false,
                        start: 0,
                        end: 100
                    },
                    xAxis: [
                        {
                            type: 'category',
                            boundaryGap: true,
                            data: (function (){
                                var now = new Date();
                                var res = [];
                                var len = 10;
                                while (len--) {
                                    res.unshift(now.toLocaleTimeString().replace(/^\D*/,''));
                                    now = new Date(now - 2000);
                                }
                                return res;
                            })()
                        },
                        {
                            type: 'category',
                            boundaryGap: true,
                            data: (function (){
                                var res = [];
                                var len = 10;
                                while (len--) {
                                    res.push(10 - len - 1);
                                }
                                return res;
                            })()
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: 'Price',
                            max: 30,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        },
                        {
                            type: 'value',
                            scale: true,
                            name: 'Quantity',
                            max: 1200,
                            min: 0,
                            boundaryGap: [0.2, 0.2]
                        }
                    ],
                    series: [
                        {
                            name:'Pre-order queue',
                            type:'bar',
                            xAxisIndex: 1,
                            yAxisIndex: 1,
                            normal: {
                                color: new echarts.graphic.LinearGradient(
                                    0, 0, 0, 1,
                                    [
                                        {offset: 0, color: '#83bff6'},
                                        {offset: 0.5, color: '#188df0'},
                                        {offset: 1, color: '#188df0'}
                                    ]
                                )
                            },
                            data:(function (){
                                var res = [];
                                var len = 10;
                                while (len--) {
                                    res.push(Math.round(Math.random() * 1000));
                                }
                                return res;
                            })()
                        },
                        {
                            name:'Latest transaction price',
                            type:'line',
                            data:(function (){
                                var res = [];
                                var len = 0;
                                while (len < 10) {
                                    res.push((Math.random()*10 + 5).toFixed(1) - 0);
                                    len++;
                                }
                                return res;
                            })()
                        }
                    ]
                };

                chart.setOption(option);

                var a = 11;
                setInterval(function (){
                    var axisData = (new Date()).toLocaleTimeString().replace(/^\D*/,'');

                    var data0 = option.series[0].data;
                    var data1 = option.series[1].data;
                    data0.shift();
                    data0.push(Math.round(Math.random() * 1000));
                    data1.shift();
                    data1.push((Math.random() * 10 + 5).toFixed(1) - 0);

                    option.xAxis[0].data.shift();
                    option.xAxis[0].data.push(axisData);
                    option.xAxis[1].data.shift();
                    option.xAxis[1].data.push(a++);

                    chart.setOption(option);
                }, 2000);

            }
        },

        chart5: function () {
            if ($('#chart5').length) {
                var chart = echarts.init(document.getElementById('chart5'));
                var option = {
                    color: chart_default_colors,
                    dataset: {
                        source: [
                            ['score', 'amount', 'product'],
                            [89.3, 58212, 'Matcha Latte'],
                            [57.1, 78254, 'Milk Tea'],
                            [74.4, 41032, 'Cheese Cocoa'],
                            [50.1, 12755, 'Cheese Brownie'],
                            [89.7, 20145, 'Matcha Cocoa'],
                        ]
                    },
                    grid: {containLabel: true},
                    xAxis: {name: 'amount'},
                    yAxis: {type: 'category'},
                    visualMap: {
                        orient: 'horizontal',
                        left: 'center',
                        min: 10,
                        max: 100,
                        text: ['High Score', 'Low Score'],
                        // Map the score column to color
                        dimension: 0,
                        inRange: {
                            color: [chart_default_colors[6], chart_default_colors[0]]
                        }
                    },
                    series: [
                        {
                            type: 'bar',
                            encode: {
                                // Map the "amount" column to X axis.
                                x: 'amount',
                                // Map the "product" column to Y axis
                                y: 'product'
                            }
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart6: function () {
            if ($('#chart6').length) {
                var chart = echarts.init(document.getElementById('chart6'));
                var option = {
                    color: chart_default_colors,
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'cross',
                            crossStyle: {
                                color: '#999'
                            }
                        }
                    },
                    toolbox: {
                        feature: {
                            dataView: {show: true, readOnly: false},
                            magicType: {show: true, type: ['line', 'bar']},
                            restore: {show: true},
                            saveAsImage: {show: true}
                        }
                    },
                    xAxis: [
                        {
                            type: 'category',
                            data: ['Jan', 'Feb', 'Mar', 'Apr', 'Aug', 'Sept'],
                            axisPointer: {
                                type: 'shadow'
                            }
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            name: '水量',
                            min: 0,
                            max: 250,
                            interval: 50,
                            axisLabel: {
                                formatter: '{value} ml'
                            }
                        },
                        {
                            type: 'value',
                            name: '温度',
                            min: 0,
                            max: 25,
                            interval: 5,
                            axisLabel: {
                                formatter: '{value} °C'
                            }
                        }
                    ],
                    series: [
                        {
                            name:'蒸发量',
                            type:'bar',
                            data:[109.0, 4.9, 207.0, 123.2, 125.6, 76.7]
                        },
                        {
                            name:'降水量',
                            type:'bar',
                            data:[200.6, 5.9, 19.0, 26.4, 128.7, 70.7]
                        },
                        {
                            name:'平均温度',
                            type:'line',
                            yAxisIndex: 1,
                            data:[50.0, 2.2, 3.3, 4.5, 6.3, 10.2]
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart7: function () {
            if ($('#chart7').length) {
                var chart = echarts.init(document.getElementById('chart7'));
                var option = {
                    color: chart_default_colors,
                    tooltip : {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                    },
                    legend: {
                        orient: 'vertical',
                        left: 'left',
                        data: ['直接访问','邮件营销','联盟广告','视频广告','搜索引擎']
                    },
                    series : [
                        {
                            name: '访问来源',
                            type: 'pie',
                            radius : '55%',
                            center: ['50%', '60%'],
                            data:[
                                {value:335, name:'直接访问'},
                                {value:310, name:'邮件营销'},
                                {value:234, name:'联盟广告'},
                                {value:135, name:'视频广告'},
                                {value:1548, name:'搜索引擎'}
                            ],
                            itemStyle: {
                                emphasis: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                                }
                            }
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart8: function () {
            if ($('#chart8').length) {
                var chart = echarts.init(document.getElementById('chart8'));
                var option = {
                    color: chart_default_colors,
                    tooltip : {
                        trigger: 'axis',
                        axisPointer : {            // 坐标轴指示器，坐标轴触发有效
                            type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                        }
                    },
                    legend: {
                        data:['利润', '支出', '收入']
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    xAxis : [
                        {
                            type : 'value'
                        }
                    ],
                    yAxis : [
                        {
                            type : 'category',
                            axisTick : {show: false},
                            data : ['周一','周二','周三','周四','周五']
                        }
                    ],
                    series : [
                        {
                            name:'利润',
                            type:'bar',
                            label: {
                                normal: {
                                    show: true,
                                    position: 'inside'
                                }
                            },
                            data:[200, 170, 240, 244, 200]
                        },
                        {
                            name:'收入',
                            type:'bar',
                            stack: '总量',
                            label: {
                                normal: {
                                    show: true
                                }
                            },
                            data:[320, 302, 341, 374, 390]
                        },
                        {
                            name:'支出',
                            type:'bar',
                            stack: '总量',
                            label: {
                                normal: {
                                    show: true,
                                    position: 'left'
                                }
                            },
                            data:[-120, -132, -101, -134, -190]
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart9: function () {
            if ($('#chart9').length) {
                var chart = echarts.init(document.getElementById('chart9'));
                var option = {
                    color: chart_default_colors,
                    tooltip : {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'cross',
                            label: {
                                backgroundColor: '#6a7985'
                            }
                        }
                    },
                    toolbox: {
                        feature: {
                            saveAsImage: {}
                        }
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    xAxis : [
                        {
                            type : 'category',
                            boundaryGap : false,
                            data : ['Jan', 'Feb', 'Mar', 'Apr', 'Aug', 'Sept', 'Oct']
                        }
                    ],
                    yAxis : [
                        {
                            type : 'value'
                        }
                    ],
                    series : [
                        {
                            name:'Email marketing',
                            type:'line',
                            stack: 'Total amount',
                            areaStyle: {},
                            data:[120, 132, 101, 134, 90, 230, 210]
                        },
                        {
                            name:'Alliance advertising',
                            type:'line',
                            stack: 'Total amount',
                            areaStyle: {},
                            data:[220, 182, 191, 234, 290, 330, 310]
                        },
                        {
                            name:'Video ad',
                            type:'line',
                            stack: 'Total amount',
                            areaStyle: {},
                            data:[150, 232, 201, 154, 190, 330, 410]
                        },
                        {
                            name:'Direct interview',
                            type:'line',
                            stack: 'Total amount',
                            areaStyle: {normal: {}},
                            data:[320, 332, 301, 334, 390, 330, 320]
                        },
                        {
                            name:'Search engine',
                            type:'line',
                            stack: 'Total amount',
                            label: {
                                normal: {
                                    show: true,
                                    position: 'top'
                                }
                            },
                            areaStyle: {normal: {}},
                            data:[820, 932, 901, 934, 1290, 1330, 1320]
                        }
                    ]
                };

                chart.setOption(option);
            }
        },

        chart10: function () {
            if ($('#chart10').length) {
                // Generate data
                var category = [];
                var dottedBase = +new Date();
                var lineData = [];
                var barData = [];

                for (var i = 0; i < 20; i++) {
                    var date = new Date(dottedBase += 3600 * 24 * 1000);
                    category.push([
                        date.getFullYear(),
                        date.getMonth() + 1,
                        date.getDate()
                    ].join('-'));
                    var b = Math.random() * 200;
                    var d = Math.random() * 200;
                    barData.push(b)
                    lineData.push(d + b);
                }

                var chart = echarts.init(document.getElementById('chart10'));
                var option = {
                    color: chart_default_colors,
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    legend: {
                        data: ['line', 'bar'],
                        textStyle: {
                            color: '#ccc'
                        }
                    },
                    xAxis: {
                        data: category,
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    yAxis: {
                        splitLine: {show: false},
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    series: [{
                        name: 'line',
                        type: 'line',
                        smooth: true,
                        showAllSymbol: true,
                        symbol: 'emptyCircle',
                        symbolSize: 15,
                        data: lineData
                    }, {
                        name: 'bar',
                        type: 'bar',
                        barWidth: 10,
                        itemStyle: {
                            normal: {
                                barBorderRadius: 5,
                                color: new echarts.graphic.LinearGradient(
                                    0, 0, 0, 1,
                                    [
                                        {offset: 0, color: '#14c8d4'},
                                        {offset: 1, color: '#43eec6'}
                                    ]
                                )
                            }
                        },
                        data: barData
                    }, {
                        name: 'line',
                        type: 'bar',
                        barGap: '-100%',
                        barWidth: 10,
                        itemStyle: {
                            normal: {
                                color: new echarts.graphic.LinearGradient(
                                    0, 0, 0, 1,
                                    [
                                        {offset: 0, color: 'rgba(20,200,212,0.5)'},
                                        {offset: 0.2, color: 'rgba(20,200,212,0.2)'},
                                        {offset: 1, color: 'rgba(20,200,212,0)'}
                                    ]
                                )
                            }
                        },
                        z: -12,
                        data: lineData
                    }, {
                        name: 'dotted',
                        type: 'pictorialBar',
                        symbol: 'rect',
                        itemStyle: {
                            normal: {
                                color: '#0f375f'
                            }
                        },
                        symbolRepeat: true,
                        symbolSize: [12, 4],
                        symbolMargin: 1,
                        z: -10,
                        data: lineData
                    }]
                };

                chart.setOption(option);
            }
        },

        chart11: function () {
            if ($('#chart11').length) {
                var data = [[10, 16, 3, 'A'], [16, 18, 15, 'B'], [18, 26, 12, 'C'], [26, 32, 22, 'D'], [32, 56, 7, 'E'], [56, 62, 17, 'F']];
                data = echarts.util.map(data, function (item, index) {
                    return {
                        value: item,
                        itemStyle: {
                            normal: {
                                color: chart_default_colors[index]
                            }
                        }
                    };
                });

                function renderItem(params, api) {
                    var yValue = api.value(2);
                    var start = api.coord([api.value(0), yValue]);
                    var size = api.size([api.value(1) - api.value(0), yValue]);
                    var style = api.style();

                    return {
                        type: 'rect',
                        shape: {
                            x: start[0],
                            y: start[1],
                            width: size[0],
                            height: size[1]
                        },
                        style: style
                    };
                }

                var chart = echarts.init(document.getElementById('chart11'));
                var option = {
                    color: chart_default_colors,
                    tooltip: {
                    },
                    xAxis: {
                        scale: true
                    },
                    yAxis: {
                    },
                    series: [{
                        type: 'custom',
                        renderItem: renderItem,
                        label: {
                            normal: {
                                show: true,
                                position: 'top'
                            }
                        },
                        dimensions: ['from', 'to', 'profit'],
                        encode: {
                            x: [0, 1],
                            y: 2,
                            tooltip: [0, 1, 2],
                            itemName: 3
                        },
                        data: data
                    }]
                };

                chart.setOption(option);
            }
        },

        chart12: function () {
            if ($('#chart12').length) {
                var chart = echarts.init(document.getElementById('chart12'));
                var option = {
                    color: chart_default_colors,
                    tooltip: {
                        trigger: 'item',
                        formatter: '{a} <br/>{b} : {c}'
                    },
                    xAxis: {
                        type: 'category',
                        name: 'x',
                        splitLine: {show: false},
                        data: ['Jan', 'Feb', 'Mar', 'Apr', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec']
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    yAxis: {
                        type: 'log',
                        name: 'y'
                    },
                    series: [
                        {
                            name: '3 index',
                            type: 'line',
                            data: [1, 3, 9, 27, 81, 247, 741, 2223, 6669]
                        },
                        {
                            name: '2 index',
                            type: 'line',
                            data: [1, 2, 4, 8, 16, 32, 64, 128, 256]
                        },
                        {
                            name: '1 index',
                            type: 'line',
                            data: [1/2, 1/4, 1/8, 1/16, 1/32, 1/64, 1/128, 1/256, 1/512]
                        }
                    ]
                };

                chart.setOption(option);
            }
        },
    };

    Charts.chart1();

    Charts.chart2();

    Charts.chart3();

    Charts.chart4();

    Charts.chart5();

    Charts.chart6();

    Charts.chart7();

    Charts.chart8();

    Charts.chart9();

    Charts.chart10();

    Charts.chart11();

    Charts.chart12();

});