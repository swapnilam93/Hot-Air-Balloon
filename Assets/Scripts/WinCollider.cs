using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour {

	GameObject winBox;
	
	// Use this for initialization
	void Start () {
		winBox = GameObject.Find("WinBox");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "Guest's Balloon") {
			winBox.SetActive(true);
		}
	}

}
