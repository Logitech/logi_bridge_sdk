/* These are functions specific to the client interface */

function SetGenericParameterFromSelect() {
    var e = document.getElementById("parameterName");
    var name = e.options[e.selectedIndex].text;
    var value = document.getElementById("parameterValue").value;
    SetGenericType(name, value);
}

function SendSliderUpdate(name, value) {
    document.getElementById(name+"Display").innerHTML = value;
    document.getElementsByName(name)[0].value = value; // make sure we have the correct value on slider
    SetGenericType(name, value);
}

function ToggleButton(button) {
    if (button.value == "ON")
        button.value = "OFF";
    else
        button.value = "ON";

    if (button.name == "rainbow")
        Rainbow(button.value == "ON");
    else
        SetGenericType(button.name, button.value == "ON");
}

/* Segmentation Presets */
function Preset(rgWeight, lutWeight, posWeight, lumWeight, finalThresh) {
    this.lutWeight = lutWeight;
    this.posWeight = posWeight;
    this.lumWeight = lumWeight;
    this.finalThresh = finalThresh;
}

var presetCork = new Preset(0.7, 1, 0.15, 0.63, 0.24);
var presetBic = new Preset(0.46, 1, 0.52, 0.72, 0.45);
var presetNalisha = new Preset(0.35, 0.75, 0.1, 0.1, 0.26);
var presetBruno = new Preset(0.75, 0.75, 0.1, 0, 0.13);

function UsePreset(preset) {
    SendSliderUpdate("rgWeight", preset.rgWeight);
    SendSliderUpdate("lutWeight", preset.lutWeight);
    SendSliderUpdate("posWeight", preset.posWeight);
    SendSliderUpdate("lumWeight", preset.lumWeight);
    SendSliderUpdate("finalThresh", preset.finalThresh);
}
