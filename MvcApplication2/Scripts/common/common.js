/**
 * Usage: 
 * getObjects(TestObj, 'id', 'A'); // Returns an array of matching objects
 */
function getObjects(obj, key, val) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(getObjects(obj[i], key, val));
        } else if (i == key && obj[key] == val) {
            objects.push(obj);
        }
    }
    return objects;
}

/**
 * Usage: 
 * findAndRemove(array, 'id', 'A'); // Removes matching object
 */
function findAndRemove(array, property, value) {
    $.each(array, function (index, result) {
        if (result[property] == value) {
            //Remove from array
            array.splice(index, 1);
        }
    });
}

function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
      s4() + '-' + s4() + s4() + s4();
}

function TruncateDecimals(digits) {
    return parseFloat(digits).toFixed(0);
}



function MinutesToHours(minutes) {
    var divided = Math.floor(minutes / 60);
    var modulo = minutes % 60;

    return ("0" + divided).slice(-2) + ":" + ("0" + modulo).slice(-2);
}

function GetInt(a) {
    return +a || 0;
}

function LogOut() {
    
}