using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassController : MonoBehaviour {

	[SerializeField]
	GameObject controller;

	float angle;

	// Use this for initialization
	void Start () {
		angle = this.gameObject.transform.rotation.y;	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.rotation = Quaternion.Euler(0f, 
			controller.transform.rotation.y + angle, 0f);
	}
}
