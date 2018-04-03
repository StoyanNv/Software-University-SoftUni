function attachEvents() {
    $('#submit').on('click', submit);
    let icons = {
        Sunny: '&#x2600',// ☀
        'Partly sunny': '&#x26C5', // ⛅
        Overcast: '&#x2601', // ☁
        Rain: '&#x2614', // ☂
        Degrees: '&#176'  // °
    };

    function submit() {
        let cityVal = $('#location').val();
        $.ajax({
            method: 'GET',
            url: 'https://judgetests.firebaseio.com/locations.json'
        }).then(handleSuccess).catch(handleError);

        function handleSuccess(res) {
            $('span').remove();
            let locationCode = res.filter(e => e['name'] === cityVal)[0];
            if (locationCode !== undefined) {
                let code = locationCode.code;
                $('#forecast').css('display', 'block');
                $.ajax({
                    method: 'GET',
                    url: `https://judgetests.firebaseio.com/forecast/today/${code}.json`
                }).then(handleSuccessForecast).catch(handleError);

                function handleSuccessForecast(res) {
                    $('#current').find('span').empty();
                    $('#current').append($(`<span>${icons[res.forecast.condition]}</span>`).addClass(`condition symbol`))
                        .append($(`<span>`).addClass('condition')
                            .append($(`<span class="forecast-data">${res.name}</span>`))
                            .append($(`<span class="forecast-data">${res.forecast.low}${icons.Degrees}/${res.forecast.high}${icons.Degrees}</span>`))
                            .append($(`<span class="forecast-data">${res.forecast.condition}</span>`)));
                }
                $.ajax({
                    method: 'GET',
                    url: `https://judgetests.firebaseio.com/forecast/upcoming/${code}/forecast.json`
                }).then(handleSuccessUpcoming).catch(handleError);

                function handleSuccessUpcoming(res) {
                    $('#upcoming').find('.upcoming').remove();
                    for (let obj of res) {
                        $('#upcoming').append($(`<span>`).addClass('upcoming')
                            .append($(`<span class="symbol">${icons[obj.condition]}</span>`))
                            .append($(`<span class="forecast-data">${obj.low}${icons.Degrees}/${obj.high}${icons.Degrees}</span>`))
                            .append($(`<span class="forecast-data">${obj.condition}</span>`)))
                    }
                }
                cityVal = $('#location').val('');
            }
            else {
                $('#forecast').css('display', 'block');
                $('#current').find('span').empty();
                $('#upcoming').find('span').empty();
                $('#current').append($(`<span>Error</span>`));
            }
        }
    }

    function handleError(err) {
        $('#current').find('span').empty();
        $('#upcoming').find('span').empty();
        $('#current').append($(`<span>${err.message}</span>`));
    }
}