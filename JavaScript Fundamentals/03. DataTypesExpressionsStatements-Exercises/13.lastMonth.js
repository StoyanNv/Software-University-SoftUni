function lastMonthDay(date) {
    let day = date[0];
    let month = date[1];
    let year = date[2];

    let newDate=  new Date(year, month - 1 , 0);
    return newDate.getDate();
}