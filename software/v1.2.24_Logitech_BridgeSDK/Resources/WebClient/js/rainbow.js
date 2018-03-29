var lightingInterval;

var hue = 0;

function Rainbow(activate) {
    if (activate) {
        document.getElementById("LED_A").value = 100;
        lightingInterval = window.setInterval(function () {
            hue = (hue + 1) % 360;
            var rgb = HSVtoRGB(hue / 360, 1, 1);
            document.getElementById("LED_R").value = rgb.r / 255 * 100;
            document.getElementById("LED_G").value = rgb.g / 255 * 100;
            document.getElementById("LED_B").value = rgb.b / 255 * 100;
            SetAllKeysLED();
        }, 20);
    } else {
        window.clearInterval(lightingInterval);
        document.getElementById("LED_R").value = 100;
        document.getElementById("LED_G").value = 100;
        document.getElementById("LED_B").value = 100;
        document.getElementById("LED_A").value = 0;
        SetAllKeysLED();
    }
}

/* Taken from https://stackoverflow.com/a/17243070/9119291 */
function HSVtoRGB(h, s, v) {
    var r, g, b, i, f, p, q, t;
    if (arguments.length === 1) {
        s = h.s, v = h.v, h = h.h;
    }
    i = Math.floor(h * 6);
    f = h * 6 - i;
    p = v * (1 - s);
    q = v * (1 - f * s);
    t = v * (1 - (1 - f) * s);
    switch (i % 6) {
        case 0: r = v, g = t, b = p; break;
        case 1: r = q, g = v, b = p; break;
        case 2: r = p, g = v, b = t; break;
        case 3: r = p, g = q, b = v; break;
        case 4: r = t, g = p, b = v; break;
        case 5: r = v, g = p, b = q; break;
    }
    return {
        r: Math.round(r * 255),
        g: Math.round(g * 255),
        b: Math.round(b * 255)
    };
}
