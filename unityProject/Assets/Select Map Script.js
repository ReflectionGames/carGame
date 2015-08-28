var levelToLoad : int;
var soundhover : AudioClip;
var beep : AudioClip;
var QuitButton=false;
function OnMouseEnter(){
GetComponent.<AudioSource>().PlayOneShot(soundhover);
}
function OnMouseUp(){
GetComponent.<AudioSource>().PlayOneShot(beep);
yield new WaitForSeconds(0.35);
if(QuitButton){
Application.Quit();
}
else{
Application.LoadLevel(levelToLoad);
}
}
@script RequireComponent(AudioSource)