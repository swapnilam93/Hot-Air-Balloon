using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour {

    [SerializeField]
    private bool UsingVR;

    private Rigidbody rb;

    public Transform[] guest;

    [SerializeField]
    private float maxSpeed;
    public float maxTiltForce; //the force exerted on the balloon when the player stands at the far end of the gondola
    [SerializeField]
    private float dropSpeed; //how quickly the balloon normally descends
    [SerializeField]
    private float minY;

    private Vector3 balloonPos;
    private Vector3 guestPos;

    [SerializeField]
    private BoxCollider basketFloor;
    [HideInInspector] public float basketSize;

    private bool canMove;

    public float multiplier = 1f;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();

        basketSize = Mathf.Max(basketFloor.size.x * basketFloor.transform.localScale.x, 
            basketFloor.size.z * basketFloor.transform.localScale.z) / 2;

        canMove = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        //if (canMove)
        //{
            //push balloon
            if (UsingVR) {

                balloonPos = transform.position;
                Vector3 guestPos = GetGuestPos();
                Vector3 diff = guestPos - balloonPos;
                float x = maxTiltForce * diff.x / basketSize;
                float z = maxTiltForce * diff.z / basketSize;
                rb.AddForce(new Vector3(x, 0f, z));

            } else {

                Vector3 force = Vector3.zero;
                if (Input.GetKey(KeyCode.A))
                    force += Vector3.left * maxTiltForce;
                if (Input.GetKey(KeyCode.D))
                    force += Vector3.right * maxTiltForce;
                if (Input.GetKey(KeyCode.W))
                    force += Vector3.forward * maxTiltForce;
                if (Input.GetKey(KeyCode.S))
                    force += Vector3.back * maxTiltForce;
                rb.AddForce(force);

            }

            //keep balloon at viable speed
            Vector2 lateralVelo = new Vector2(rb.velocity.x, rb.velocity.z);
            if (lateralVelo.magnitude > maxSpeed * multiplier) {
                lateralVelo = lateralVelo.normalized * maxSpeed * multiplier;
                rb.velocity = new Vector3(lateralVelo.x, rb.velocity.y, lateralVelo.y);
            }

            if (balloonPos.y > minY) {
                //descend balloon
                float posY = balloonPos.y;
                posY -= dropSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
        //}

	}

    Vector3 GetGuestPos() {

        Vector3 guestPos = Vector3.zero;

        foreach (Transform part in guest) {

            guestPos += part.transform.position;

        }

        return (1f / guest.Length) * guestPos;

    }

    public void AdjustMovement (bool allowed) //starts or stops movement depending on if the balloon has landed
    {

        canMove = allowed;

    }
}
