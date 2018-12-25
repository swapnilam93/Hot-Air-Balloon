using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

	GameObject startObject;

	[SerializeField]
	GameObject[] targetPoints;
	[SerializeField]
	float randomRange;
	[SerializeField]
	float speed;
	[SerializeField]
	private float upTopHeight;
	public bool upTop;
	bool goDown;
	[SerializeField]
	float downHeight;

	// Use this for initialization
	void Start () {
		startObject = this.gameObject;
		upTop = false;
		goDown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.y >= upTopHeight) {
            //flying = false;
            //to activate NPC movement towards final destination
            if(!upTop)
                MoveToDestination();
            upTop = true;
        }
		if (goDown && startObject.transform.position.y >= downHeight) {
			startObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			startObject.transform.position =
                new Vector3(startObject.transform.position.x,
                startObject.transform.position.y - speed/4 * Time.deltaTime,
                startObject.transform.position.z);
		}
	}

	public void MoveToDestination() {
		Vector3[] path = new Vector3[targetPoints.Length+1];
        //make possible balloon paths visible in editor
		int i = 0;
		path[0] = this.gameObject.transform.position;
		i++;
		foreach (GameObject endPos in targetPoints) {	
			//Debug.Log(endPos.transform.position);
			//randomly offset target points
			float randomOffset = Random.Range(-randomRange, randomRange);
			Vector3 targetPoint = endPos.transform.position;
			Vector3 targetPosition = new Vector3(targetPoint.x + randomOffset, targetPoint.y + randomOffset, 
			targetPoint.z + randomOffset);
			if(i==1) {
				Debug.DrawLine(startObject.transform.position, targetPosition, Color.magenta, Mathf.Infinity);
			} else {
				Debug.DrawLine(path[i-1], targetPosition, Color.magenta, Mathf.Infinity);
			}
			//populate path points array
			path[i] = targetPosition;
			//Debug.Log(path[i]);
			i++;
		}
		Debug.Log(this.gameObject.name);
		//move the balloon along the path
		iTween.MoveTo(startObject, iTween.Hash("path", path, "speed", speed, "moveToPath", false, "orienttopath", true,
			"easetype", iTween.EaseType.easeInOutSine, "lookahead", 0.05f, "looktime", 0.1f, "oncomplete", "getDown"));
	}

	public void getDown() {
		goDown = true;
	}

}
