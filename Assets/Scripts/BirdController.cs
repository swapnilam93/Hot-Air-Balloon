using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	[SerializeField]
	AudioClip[] birdSounds;

	AudioSource birdAudioSource;

	[SerializeField]
	float repeatRate;

	void Awake () {
		birdAudioSource = this.GetComponent<AudioSource>();
	}
	// Use this for initialization
	void Start () {
		float randomDelay = Random.Range(0, 5f);
		InvokeRepeating("BirdChirp", randomDelay, repeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void BirdChirp() {
		birdAudioSource.PlayOneShot(birdSounds[(int)Random.Range(0, birdSounds.Length)]);
	}
}
