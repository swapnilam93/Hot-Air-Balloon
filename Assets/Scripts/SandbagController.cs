using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagController : MonoBehaviour {

	//SteamVR_Controller.Device myController;

	[SerializeField]
	AudioClip pickAudio;
	[SerializeField]
	AudioClip dropAudio;
	[SerializeField]
	AudioClip putDownAudio;

	[SerializeField]
	string sandbagInTag;
	[SerializeField]
	string sandbagOutTag;

	AudioSource sandbagAudioSource;
	Rigidbody sandbagRigidbody; 
	FixedJoint controllerFixedJoint;

	[SerializeField]
	GameObject VRCamera;
	bool isPicked;
	bool isPutDown;
	bool isDropped;

	void Awake () {
		sandbagAudioSource = this.GetComponent<AudioSource>();
		sandbagRigidbody = this.GetComponent<Rigidbody>();
	}
	// Use this for initialization
	void Start () {
		isPicked = false;
		isPutDown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y - VRCamera.transform.position.y > 0.43 && !isPicked && this.tag == sandbagInTag) {
			sandbagAudioSource.PlayOneShot(pickAudio);
			isPicked = true;
			isPutDown = false;
		} else if (this.transform.position.y - VRCamera.transform.position.y <= 0.43 && !isPutDown && this.tag == sandbagInTag) {
			sandbagAudioSource.PlayOneShot(putDownAudio);
			isPutDown = true;
			isPicked = false;
		} else if (this.transform.position.y - VRCamera.transform.position.y <= 0 && !isDropped && this.tag == sandbagOutTag) {
			sandbagAudioSource.PlayOneShot(dropAudio);
			isDropped = true;
		}
	}

	/*private void OnTriggerStay(Collider other) {
		if(myController.GetHairTriggerDown()) {
			
			sandbagRigidbody.useGravity = false;
		}
	}

	private void OnTriggerExit(Collider other) {
		
	}*/
}
