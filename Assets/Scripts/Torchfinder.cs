using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchfinder : MonoBehaviour {

    public  List<Collider> litTorches;
    public TorchCounter torchCounter;

    private void Start() {

        litTorches = new List<Collider>();
        torchCounter = GameObject.Find("Win").GetComponent<TorchCounter>();

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("naofn");

        if (!litTorches.Contains(other) && other.gameObject.name.StartsWith("Light point")) {

            litTorches.Add(other);
            int count = litTorches.Count;
            torchCounter.PlayAudioTorch(count);
            if(count == 5) {
                torchCounter.onWin();
            }
            StartCoroutine(other.gameObject.GetComponent<Torchlight>().LightUp());

        }

    }
}
