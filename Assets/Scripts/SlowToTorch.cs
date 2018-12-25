using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowToTorch : MonoBehaviour {

    [SerializeField]
    private Transform lightPoint;

    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float minSpeedRadius;

    private float collRad;

    private void Start() {

        collRad = GetComponent<SphereCollider>().radius;
        collRad *= transform.localScale.x * transform.parent.localScale.x;
        minSpeedRadius *= collRad;
        Debug.Log(collRad);

    }

    //adjusts balloon speed as it approaches a torch
    private void OnTriggerStay(Collider other) {

        if (!other.name.StartsWith("Bottom"))
            return;

        GameObject parent = other.transform.parent.gameObject;

        if (parent == null)
            return;

        GameObject grandparent = parent.transform.parent.gameObject;

        if (grandparent == null)
            return;

        BalloonMovement balloon = grandparent.GetComponent<BalloonMovement>();
        Vector3 balloonPos = grandparent.transform.position;
        Vector3 torchPos = lightPoint.position;
        float dist = (balloonPos - torchPos).magnitude;

        Debug.Log("dist = " + dist.ToString());

        if (dist > collRad)
            balloon.multiplier = 1f;

        else if (minSpeedRadius < dist && dist < collRad) {

            float rise = 1f - minSpeed;
            float run = collRad - minSpeedRadius;
            float m = rise / run;
            float y1 = 1f;
            float x = dist;
            float x1 = collRad;
            balloon.multiplier = m * (x - x1 + (y1 / m));

        }

        else if (dist < minSpeedRadius)
            balloon.multiplier = minSpeed;

        Debug.Log("multiplier = " + balloon.multiplier.ToString());

    }

    private void OnTriggerExit(Collider other) {

        if (!other.name.StartsWith("Bottom"))
            return;

        GameObject parent = other.transform.parent.gameObject;

        if (parent == null)
            return;

        GameObject grandparent = parent.transform.parent.gameObject;

        if (grandparent == null)
            return;

        BalloonMovement balloon = grandparent.GetComponent<BalloonMovement>();
        balloon.multiplier = 1f;

    }

}
