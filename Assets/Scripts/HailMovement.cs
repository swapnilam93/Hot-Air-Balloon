using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailMovement : MonoBehaviour {

	[SerializeField]
	GameObject balloon;

	[SerializeField]
	GameObject guest;

	private Vector3 balloonPos;
	private Vector3 guestPos;
	private float basketSize;

	// Use this for initialization
	void Start () {
		basketSize = balloon.GetComponent<BalloonMovement>().basketSize;
	}
	
	// Update is called once per frame
	void Update () {
			balloonPos = balloon.transform.position;
            guestPos = guest.transform.position;
            Vector3 diff = guestPos - balloonPos;
            float x = diff.x / basketSize;
            float z = diff.z / basketSize;

            transform.rotation = Quaternion.Euler(x, this.gameObject.transform.rotation.y, z);
	}
}
