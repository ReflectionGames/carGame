#pragma strict
import UnityStandardAssets.CrossPlatformInput;

private var animator: Animator;
private var pressed: boolean = false;

function Start(){
	animator = gameObject.GetComponent.<Animator>();
}

function CloseMenu(){
	animator.SetBool(Animator.StringToHash("open"),false);
}

function OpenMenu(){
	animator.SetBool(Animator.StringToHash("open"),true);
}

function Update () {
	if(CrossPlatformInputManager.GetButtonDown("Cancel") && pressed == false){
		if(animator.GetBool(Animator.StringToHash("open"))){
			CloseMenu();
		}
		else{
			OpenMenu();
		}
		pressed = true;
	}
	else if(pressed != false) pressed = false;
}