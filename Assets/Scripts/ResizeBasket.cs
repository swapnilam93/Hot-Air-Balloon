using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBasket : MonoBehaviour {

    [SerializeField]
    private float length;
    [SerializeField]
    private float width;
    [SerializeField]
    private float height;
    [SerializeField]
    private float thickness;

    // Use this for initialization
    void Start () {

        Transform bottom = transform.GetChild(0);
        bottom.position = new Vector3(0f, -thickness / 2f, 0f);
        bottom.localScale = new Vector3(width + thickness * 2f, thickness, length + thickness * 2f);

        Transform back = transform.GetChild(1);
        back.position = new Vector3(0f, height / 2f, -length / 2f - thickness / 2f);
        back.localScale = new Vector3(width + thickness * 2f, height, thickness);

        Transform front = transform.GetChild(2);
        front.position = new Vector3(0f, height / 2f, length / 2f + thickness / 2f);
        front.localScale = new Vector3(width + thickness * 2f, height, thickness);

        Transform right = transform.GetChild(3);
        right.position = new Vector3(width / 2f + thickness / 2f, height / 2f, 0f);
        right.localScale = new Vector3(thickness, height, length + thickness * 2f);

        Transform left = transform.GetChild(4);
        left.position = new Vector3(-width / 2f - thickness / 2f, height / 2f, 0f);
        left.localScale = new Vector3(thickness, height, length + thickness * 2f);

        Destroy(this);

    }
}
