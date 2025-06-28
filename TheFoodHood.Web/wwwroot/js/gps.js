window.obtenerUbicacion = async function () {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject("Geolocalización no soportada");
        }
        navigator.geolocation.getCurrentPosition(
            pos => {
                resolve(pos.coords.latitude + "," + pos.coords.longitude);
            },
            err => {
                reject("Error: " + err.message);
            }
        );
    });
}
