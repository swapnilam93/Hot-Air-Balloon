using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBoost : MonoBehaviour {

	//serializable variables
	[SerializeField]
	GameObject balloon;

	[SerializeField]
	GameObject[] sandbags;

	[SerializeField]
	string sandbagInTag;
	[SerializeField]
	string sandbagOutTag;

	[SerializeField]
	float boostSpeed;
	[SerializeField]
	float boostTime;

	//public variables

	//private variables
	private Rigidbody balloonRigidbody;
	private Transform balloonTransform;

	private void Awake() {
		balloonRigidbody = balloon.GetComponent<Rigidbody>();
		balloonTransform = balloon.GetComponent<Transform>();
	}
	private void OnTriggerExit(Collider other) {
		//boost up if sandbag is thrown out
		if(other.tag == sandbagInTag) {
			//+y velocity
			Debug.Log(balloonRigidbody.velocity + " before");
			Debug.Log(balloonRigidbody.velocity + " after");
			//instant jerk
			balloonTransform.SetPositionAndRotation(balloonTransform.position + new Vector3(0f, 0.1f, 0f), balloonTransform.rotation);
			StartCoroutine(WaitForJerk());
			other.tag = sandbagOutTag;
			other.GetComponent<TrackedObject>().enabled = false;
			other.GetComponent<SteamVR_TrackedObject>().enabled = false;
		}
	}

	IEnumerator WaitForJerk() {
		//to make isKinematic switch smooth
		yield return new WaitForSeconds(0.01f);
		//add the boost speed
		balloonRigidbody.velocity += new Vector3(0f, boostSpeed, 0f);
		StartCoroutine(SlowBoostOne());
	}

	private void OnTriggerEnter(Collider other) {
		//change tag of sandbag to in if its back inside
		if(other.tag == sandbagOutTag) {
			other.tag = sandbagInTag;
		}
	}

	IEnumerator SlowBoostOne() {
		Debug.Log(balloonRigidbody.velocity + " midway 1");
		//to boost up for fixed amount of time and slow down after some time
		yield return new WaitForSeconds(boostTime/3);
		Debug.Log(balloonRigidbody.velocity + " midway 1/2/2");
		if(balloonRigidbody.velocity.y > boostSpeed/3)
			balloonRigidbody.velocity -=  new Vector3(0f, boostSpeed/3, 0f);
		Debug.Log(balloonRigidbody.velocity + " midway 1/2");
		StartCoroutine(SlowBoostTwo());
	}

	IEnumerator SlowBoostTwo() {
		Debug.Log(balloonRigidbody.velocity + " midway 2");
		//to boost up for fixed amount of time and slow down after some time
		yield return new WaitForSeconds(boostTime/3);
		if(balloonRigidbody.velocity.y > boostSpeed/3)
			balloonRigidbody.velocity -= new Vector3(0f, boostSpeed/3, 0f);
		StartCoroutine(StopBoost());
	}

	IEnumerator StopBoost() {
		Debug.Log(balloonRigidbody.velocity + " midway 3");
		//to boost up for fixed amount of time and stop
		yield return new WaitForSeconds(boostTime/3);
		if(balloonRigidbody.velocity.y > 0.1f)
			balloonRigidbody.velocity = new Vector3(0f, 0f, 0f);
		Debug.Log(balloonRigidbody.velocity + " final");
	}
}
