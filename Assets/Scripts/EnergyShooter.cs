using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShooter : MonoBehaviour {

    [HideInInspector] public List<Transform> islands;

    [SerializeField]
    private GameObject energyPrefab;

    [SerializeField]
    private float freq;

	// Use this for initialization
	void Start () {

        StartCoroutine(ShootEnergy());
		
	}
	
	IEnumerator ShootEnergy () {

        while (islands.Count < 1)
            yield return null;

        while(islands.Count > 0) {

            foreach (Transform island in islands) {

                GameObject energy = Instantiate(energyPrefab, transform.position, Quaternion.identity);
                energy.GetComponent<Energy>().dest = island.position;
                StartCoroutine(energy.GetComponent<Energy>().Go());

            }

            yield return new WaitForSeconds(freq);

        }

        yield return null;

    }

}
