function GetValidJsDateFromFormattedDate(dateText) {

    const dateSplitted = dateText.split("/")

    const year = dateSplitted[2];
    const month = dateSplitted[1];
    const day = dateSplitted[0];



    const validDateFormatForJs = year + '-' + month + '-' + day;

    return validDateFormatForJs;
}

function GetFormattedDate(date) {
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    return month + '/' + day + '/' + year;
}