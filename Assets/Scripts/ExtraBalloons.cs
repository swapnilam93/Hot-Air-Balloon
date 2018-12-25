using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBalloons : MonoBehaviour {

	[SerializeField]
	GameObject[] extraBalloons;

    [SerializeField]
    private float speed;

	[SerializeField]
    private float randomDelay;
	[SerializeField]
	private float randomRange;
	[SerializeField]
	private float repeatRate;
	[SerializeField]
	private GameObject startLocation;

	// Use this for initialization
	void Start () {
		InvokeRepeating("GenerateExtraBalloons", Random.Range(5f, randomDelay), repeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateExtraBalloons() {
		float randomBalloon = Random.Range(0, extraBalloons.Length);

		Vector3 startPosition = new Vector3(startLocation.transform.position.x + Random.Range(0f, randomRange), 
			startLocation.transform.position.y+ Random.Range(0f, randomRange), startLocation.transform.position.z + Random.Range(0f, randomRange));
		Instantiate(extraBalloons[(int)randomBalloon], startPosition, Quaternion.Euler(0f, -90f, 0f));
	}
}
