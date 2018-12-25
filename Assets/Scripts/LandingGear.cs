using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour {

    [SerializeField]
    private AudioSource aud;
    [SerializeField]
    private Rigidbody rb;

    public BalloonMovement balloon;

    void OnTriggerEnter(Collider other) {

        if (!other.isTrigger) {

            aud.Play();
            rb.isKinematic = true;
            balloon.AdjustMovement(false);

        }

    }

    void OnTriggerStay(Collider collision)
    {

        if (!collision.isTrigger) {

            rb.isKinematic = true;
            balloon.AdjustMovement(false);

        }
    }

    void OnTriggerExit(Collider collision)
    {
        rb.isKinematic = false;
        balloon.AdjustMovement(true);
    }
}
