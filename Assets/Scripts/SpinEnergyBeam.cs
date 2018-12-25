using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEnergyBeam : MonoBehaviour {

    [SerializeField]
    private float speed;
	
	// Update is called once per frame
	void Update () {

        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, Time.time * speed, transform.eulerAngles.z);
		
	}
}
