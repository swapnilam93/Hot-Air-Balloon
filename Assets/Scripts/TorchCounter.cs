using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCounter : MonoBehaviour {

	[SerializeField]
	private AudioSource[] festivalMusic;
	private float islandCount;
	GameObject[] winLights;
	GameObject[] islandLights;
	int i;
	
	// Use this for initialization
	void Start () {
		winLights = GameObject.FindGameObjectsWithTag("WinLight");
		islandLights = GameObject.FindGameObjectsWithTag("IslandLight");
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAudioTorch(int count) {
		festivalMusic[count - 1].mute = false;
		onIslandLit(count);
	}

	public void onIslandLit(int count) {
		islandLights[count - 1].SetActive(true);
	}

	public void onWin() {
		if (i == 0) {
			GameObject.Find("Flower Fall").SetActive(true);
			Invoke("lightStone", 2f);
			i++;
		}
		if (i > 0 && i < 2) {
			Invoke("lightUp", 2f);

			i++;
		}
	}

	public void lightStone() {
		islandLights[5].SetActive(true);
	}

	public void lightUp() {
		winLights[i - 1].SetActive(true);
	}
}
