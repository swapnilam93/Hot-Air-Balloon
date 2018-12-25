using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBird : MonoBehaviour {

	[SerializeField]
	GameObject[] startPoints;

	[SerializeField]
	GameObject bird;

	[SerializeField]
	float repeatRate;

	[SerializeField]
	GameObject[] targetPoints;

	[SerializeField]
	float speed;

	[SerializeField]
	float randomRange;

	[SerializeField]
	float randomDelay;

	// Use this for initialization
	void Start () {
		InvokeRepeating("GenerateBirdRandomly", Random.Range(0f, randomDelay), repeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateBirdRandomly() {
		//randomly offset random start point
		int randomStart = Random.Range(0, startPoints.Length);
		float randomOffset = Random.Range(-randomRange, randomRange);
		Vector3 startPoint = startPoints[randomStart].transform.position;
		Vector3 startPosition = new Vector3(startPoint.x + randomOffset, startPoint.y + randomOffset, startPoint.z + randomOffset);
		//instantiate bird
		GameObject birdSpawned = Instantiate(bird, startPosition, Quaternion.Euler(0f, -90f, 0f));
		MoveBird(birdSpawned, startPosition);
	}

	void MoveBird(GameObject birdSpawned, Vector3 startPosition) {
		Vector3[] path = new Vector3[targetPoints.Length+1];
        //make possible bird paths visible in editor
		int i = 0;
		path[0] = startPosition;
		i++;
		foreach (GameObject endPos in targetPoints) {	
			//Debug.Log(endPos.transform.position);
			//randomly offset target points
			float randomOffset = Random.Range(-randomRange, randomRange);
			Vector3 targetPoint = endPos.transform.position;
			Vector3 targetPosition = new Vector3(targetPoint.x + randomOffset, targetPoint.y + randomOffset, 
			targetPoint.z + randomOffset);
			if(i==0) {
				Debug.DrawLine(startPosition, targetPosition, Color.red, Mathf.Infinity);
			} else {
				Debug.DrawLine(path[i-1], targetPosition, Color.red, Mathf.Infinity);
			}
			//populate path points array
			path[i] = targetPosition;
			//Debug.Log(path[i]);
			i++;
		}
		//move the bird along the path
		iTween.MoveTo(birdSpawned, iTween.Hash("path", path, "speed", speed, "moveToPath", false, "orienttopath", true, 
			"easetype", "linear", "lookahead", 0.05f, "looktime", 0.1f));
	}

}
