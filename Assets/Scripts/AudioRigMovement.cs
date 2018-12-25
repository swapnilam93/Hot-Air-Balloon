using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRigMovement : MonoBehaviour
{

    public float maxHeight, minHeight, radius;

    public GameObject player, topSource, bottomSource;

    // Use this for initialization
    void Start()
    {
        // Get audio sources, fine to do in editor too
        topSource = transform.GetChild(0).gameObject;
        bottomSource = transform.GetChild(1).gameObject;
        // Set distance, edit in editor blah blah
        topSource.GetComponent<AudioSource>().maxDistance = radius;
        bottomSource.GetComponent<AudioSource>().maxDistance = radius;
    }

    // Update is called once per frame
    void Update()
    {
        // Update each source's position to preserve Unity's 3D audio, keep at set distance to let player come closer and drift further away from each.
        topSource.transform.position = new Vector3(player.transform.position.x, maxHeight, player.transform.position.z);
        bottomSource.transform.position = new Vector3(player.transform.position.x, minHeight, player.transform.position.z);
    }
}