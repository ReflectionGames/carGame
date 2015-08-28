#pragma strict

import UnityStandardAssets.CrossPlatformInput;

var InputName: String;
var lights: Light[];
private var lightsOn: boolean = false;
private var val: boolean = false;

function Update () {
	//test for user input
	if(CrossPlatformInputManager.GetButtonDown(InputName)){
		//toggle lights
		lightsOn = !val;
	}
	else if(val != lightsOn){
		val = lightsOn;
	}
	
	toggleLights(lightsOn);
}

function toggleLights(enabled: boolean){
	for(var i = 0; i < lights.Length; i++){
		var light: Light = lights[i];
		light.enabled = enabled;
	}
}