using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {
	public void Stop () {
		Animator anim = gameObject.GetComponent<Animator> ();
		anim.enabled = false;
	}
}