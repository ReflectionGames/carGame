using UnityEngine;
using System.Collections;

public class disableOnQualitySetting : MonoBehaviour {

	public level[] levels;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < levels.Length; i++) {
			if(QualitySettings.GetQualityLevel() == levels[i].levelID){
				levels[i].apply ();
				break;
			}
		}
	}
}

[System.Serializable]
public class level{
	public int levelID;
	public Color ambientLight;
	public GameObject[] enabledObjects;
	public GameObject[] disabledObjects;
	public MonoBehaviour[] enabledScripts;
	public MonoBehaviour[] disabledScripts;

	public void apply(){
		for(int i = 0; i < this.enabledObjects.Length; i++){
			this.enabledObjects[i].SetActive(true);
		}
		for(int i = 0; i < this.disabledObjects.Length; i++){
			this.disabledObjects[i].SetActive(false);
		}
		for(int i = 0; i < this.enabledScripts.Length; i++){
			this.enabledScripts[i].enabled = true;
		}
		for(int i = 0; i < this.disabledScripts.Length; i++){
			this.disabledScripts[i].enabled = false;
		}

		RenderSettings.ambientLight = this.ambientLight;
	}
}