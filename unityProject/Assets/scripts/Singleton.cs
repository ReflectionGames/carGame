using UnityEngine;

public class Singleton : MonoBehaviour {
	private static Singleton instance = null;
	public static Singleton Instance {
		get { return instance; }
	}
	public void Awake() {
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		} 
		else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}