using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	[SerializeField]
	GameObject UIElements;

	[SerializeField]
	GameObject npcBalloon;

	[SerializeField]
	GameObject burner;

	private SteamVR_TrackedObject trackedObj;
    //2
    private SteamVR_Controller.Device control
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void StartGame() {
		StartCoroutine("Burn");
		npcBalloon.SetActive(true);
	}

	IEnumerator Burn() {
		burner.SetActive(true);
		yield return new WaitForSeconds(5f);
		UIElements.SetActive(false);
	}

	private void OnTriggerStay(Collider other) {
		if (control.GetHairTriggerDown()) {
			StartGame();
		}
	}
}
