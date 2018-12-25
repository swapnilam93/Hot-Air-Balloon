using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachBallast : MonoBehaviour {

    [SerializeField]
    private GameObject balloon;
    [SerializeField]
    private Rigidbody sandbag;
    [SerializeField]
    private float ascentTime;
    [SerializeField]
    private Vector3 sandbagTrajectory;
    [SerializeField]
    private float speed;
    private float upTopHeight;

    private AudioSource aud;
    private bool flying;
    private bool upTop;

    [SerializeField]
    GameObject burner;

    // Use this for initialization
    void Start () {

        aud = GetComponent<AudioSource>();
        Invoke("LightUp", 40f);
        Invoke("ReleaseBallast", aud.clip.length + ascentTime - 9f);
		upTop = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (flying && !balloon.GetComponent<NPCMovement>().upTop)
            balloon.transform.position =
                new Vector3(balloon.transform.position.x,
                balloon.transform.position.y + speed * Time.deltaTime,
                balloon.transform.position.z);
	}

    void ReleaseBallast()
    {

        sandbag.AddForce(sandbagTrajectory);
        Invoke("FlyAway", ascentTime);

    }

    void FlyAway() {

        flying = true;

    }

    void LightUp() {
        burner.SetActive(true);
    }
}
