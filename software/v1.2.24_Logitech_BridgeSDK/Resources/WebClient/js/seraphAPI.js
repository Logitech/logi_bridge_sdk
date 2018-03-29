
    var portNumber = 58960;
    var ghostSegmentationThreshold = 0.3;

    function SeraphAPICall(url,params)
    {
        if ("WebSocket" in window)
        {
                // Let us open a web socket
                var ws = new WebSocket("ws://localhost:"+portNumber+url);

                ws.onopen = function()
                {
                    // Web Socket is connected, send data using send()
                    ws.send(params);
                };

                ws.onmessage = function (evt)
                {
                    var received_msg = evt.data;
                    console.log("Call result: "+received_msg);
                    ws.close();
                };

                ws.onclose = function()
                {
                    // websocket is closed.
                    console.log("Connection is closed...");
                };
            }

            else
            {
                // The browser doesn't support WebSocket
                alert("WebSocket NOT supported by your Browser!");
            }
        }

        function HandsSegmentationThresholdAPICall(){
            handsSegmentationThreshold = document.getElementById('HANDS_SEGMENTATION_THRESHOLD').value;
            SeraphAPICall("/setHandsSegmentationThreshold", '{\"hands_mode\":\"hands_segmentation\", \"segmentation_threshold\":' + handsSegmentationThreshold + '}');
            //SeraphAPICall("/setHandsSegmentationThreshold", '{\"hands_mode\":\"alternative_segmentation\", \"segmentation_threshold\":' + handsSegmentationThreshold + '}');
        }

        function AltSegmentationHueOffsetAPICall(){
            hueOffset = document.getElementById('ALT_SEGMENTATION_HUE_OFFSET').value;
            SeraphAPICall("/setAlternativeHandTintOffset", '{\"hand_tint_offset\":' + hueOffset + '}');
        }

        function SetPort()
        {
            portNumber = document.getElementById('PORT_NUMBER_BOX').value;
        }

        function SetTrackerId()
        {
            tracker = document.getElementById('TRACKER_ID').value;
            SeraphAPICall("/setKbdTrackerDeviceId", '{\"id\":\"' + tracker + '\"}');
        }

        function TestNotificationChanel(url,params)
        {
            if ("WebSocket" in window)
            {
                // Let us open a web socket
                var ws = new WebSocket("ws://localhost:3000" + url);

                ws.onopen = function()
                {
                    // Web Socket is connected, send data using send()
                    ws.send(params);
                };

                ws.onmessage = function (evt)
                {
                    var received_msg = evt.data;
                    console.log("Call result: " + received_msg);
                };
            }

            else
            {
                // The browser doesn't support WebSocket
                alert("WebSocket NOT supported by your Browser!");
            }
        }

        function GetSupportedKeyboards()
        {
            SeraphAPICall("/getSupportedKeyboards","{}");
        }

        function ChangeSkin(skinName)
        {
            var json = {"skin":skinName};
            SeraphAPICall("/setKeyboardSkin",JSON.stringify(json));
        }

        function SendMsgToImgProcessor(messageValue)
        {
            var json = {"image_processor_message":messageValue};
            SeraphAPICall("/sendHandsSegmentationMessage",JSON.stringify(json));
        }

        function SetCameraLatency(latency)
        {
            document.getElementById("latencyText").innerHTML = latency;
            var json = {"cameraLatency":Number(latency)};
            SeraphAPICall("/setCameraLatency",JSON.stringify(json));
        }

        function SetVelocityEffectAmplitude(amplitude)
        {
            document.getElementById("amplitudeText").innerHTML = amplitude;
            var json = {"effectAmplitude":Number(amplitude)};
            SeraphAPICall("/setVelocityEffectAmplitude",JSON.stringify(json));
        }

        function InitInVrUi(){
            windowTitle = document.getElementById('WINDOW_TITLE').value;
            SeraphAPICall("/initInVrUi", '{\"windowTitle\":\"' + windowTitle + '\"}');

        }

        function SetAllKeysLED(){
            red = document.getElementById('LED_R').value;
            green = document.getElementById('LED_G').value;
            blue = document.getElementById('LED_B').value;
            alpha = document.getElementById('LED_A').value;

            SeraphAPICall("/setAllKeysLEDColor", '{\"rgba\": [' + red + ',' + green + ',' + blue + ',' + alpha + ']}');
        }

        function SetKeyLED(){
            red = document.getElementById('LED_R').value;
            green = document.getElementById('LED_G').value;
            blue = document.getElementById('LED_B').value;
            alpha = document.getElementById('LED_A').value;
            code = document.getElementById('LED_CODE').value;

            SeraphAPICall("/setKeyLEDColor", '{\"keyCode\": ' + code + ', \"rgba\": [' + red + ',' + green + ',' + blue + ',' + alpha + ']}');
        }

        function SetVRUICrop(){
            cropTop = document.getElementById('CROP_TOP').value;
            cropLeft = document.getElementById('CROP_LEFT').value;
            cropBottom = document.getElementById('CROP_BOTTOM').value;
            cropRight = document.getElementById('CROP_RIGHT').value;

            SeraphAPICall("/setImageCrop", '{\"crop\": [' + Number(cropTop) + ',' + Number(cropLeft) + ',' + Number(cropBottom) + ',' + Number(cropRight) + ']}');
        }


        function SetGenericType(name, value){
            SeraphAPICall("/setParameter", '{\"name\":\"' + name + '\",\"value\":\"' + value + '\"}');
        }
