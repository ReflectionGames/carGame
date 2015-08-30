#pragma strict

var Sounds: sound[];
private var audioSources: Hashtable = new Hashtable();

function Start () {
	for(var i: int = 0; i < Sounds.Length; i++){
		var source: AudioSource = gameObject.AddComponent.<AudioSource>();
		source.loop = false;
		source.clip = Sounds[i].sound;
		audioSources.Add(Sounds[i].name,source);
	}
}

function PlaySound (sound: String) {
	if(audioSources.ContainsKey(sound)){
		var audio: AudioSource = audioSources[sound] as AudioSource;
		audio.Play();
	}
}

class sound{
	public var name: String;
	public var sound: AudioClip;
}