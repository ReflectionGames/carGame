using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour {
	
	//Screen to open automatically at the start of the Scene
	public Animator initiallyOpen;

	public menuSetting[] Menus;
	
	//Currently Open Screen
	private Animator m_Open;
	
	//Hash of the parameter we use to control the transitions.
	private int m_OpenParameterId;
	
	//Animator State and Transition names we need to check against.
	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";
	
	const string closedStateName = "Closed";
	const string openBool = "openBool";
	private int openBoolHash = 0;
	
	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);
		openBoolHash = Animator.StringToHash (openBool);
		
		//If set, open the initial Screen now.
		if (initiallyOpen) {
			OpenPanel(initiallyOpen);
		}
	}
	
	//Closes the currently open panel and opens the provided one.
	//It also takes care of handling the navigation, setting the new Selected element.
	public void OpenPanel (Animator anim)
	{
		if (m_Open == anim)
			return;
		
		//Activate the new Screen hierarchy so we can animate it.
		anim.gameObject.SetActive(true);
		//Move the Screen to front.
		anim.transform.SetAsLastSibling();

		//close
		CloseCurrent ();
		
		//Set the new Screen as then open one.
		m_Open = anim;
		//Start the open animation
		m_Open.SetBool(m_OpenParameterId, true);
		
		//Set an element in the new screen as the new Selected one.
		//GameObject go = FindFirstEnabledSelectable(anim.gameObject);
		//SetSelected(go);
	}

	public void OpenMenu (string name){
		for (int i = 0; i < Menus.Length; i++) {
			if(Menus[i].name == name){
				OpenMenu(Menus[i].menu);
				return;
			}
		}
		Debug.LogError ("Failed to open menu: "+name);
	}

	public void OpenMenu (Animator anim){
		//close all other menus
		closeAllMenusBut (anim);

		//start anim
		anim.gameObject.SetActive (true);
		anim.SetBool (this.openBoolHash,true);
	}

	public void closeAllMenus(){
		for(int i = 0; i < Menus.Length; i++){
			if(isOpen(Menus[i].menu)){
				closeMenu(Menus[i].menu);
			}
		}
	}

	public void closeAllMenusBut(Animator anim){
		for(int i = 0; i < Menus.Length; i++){
			if(isOpen(Menus[i].menu) && Menus[i].menu != anim){
				closeMenu(Menus[i].menu);
			}
		}
	}

	public void closeMenu(Animator anim){
		anim.SetBool (openBoolHash,false);
		StartCoroutine (DisableDeleyed(anim));
	}

	private bool isOpen(Animator anim){
		if (!anim.IsInTransition (0))
			return true;
		return anim.GetCurrentAnimatorStateInfo (0).IsName (closedStateName);
	}

	private bool isClosed(Animator anim){
		return !isOpen (anim);
	}
	
	//Closes the currently open Screen
	//It also takes care of navigation.
	//Reverting selection to the Selectable used before opening the current screen.
	public void CloseCurrent()
	{
		if (m_Open == null)
			return;
		
		//Start the close animation.
		m_Open.SetBool(m_OpenParameterId, false);

		//Start Coroutine to disable the hierarchy when closing animation finishes.
		StartCoroutine(DisableDeleyed(m_Open));
		//No screen open.
		m_Open = null;
	}

	IEnumerator DisableDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);
			
			wantToClose = !anim.GetBool(m_OpenParameterId);
			
			yield return new WaitForEndOfFrame();
		}
		
		if (wantToClose)
			anim.gameObject.SetActive(false);
	}
}

[System.Serializable]
public class menuSetting{
	public string name;
	public Animator menu;
}