using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : MonoBehaviour {

    private float lightIntensity;

    public bool lightUp;

    [SerializeField]
    private GameObject energyBeam;

	// Use this for initialization
	void Start () {

        lightIntensity = GetComponent<Light>().intensity;
        GetComponent<Light>().intensity = 0f;
		
	}

    private void Update()
    {
        if (lightUp)
            StartCoroutine(LightUp());
    }

    public IEnumerator LightUp() {

        GetComponent<ParticleSystem>().Play();

        Light firelight = GetComponent<Light>();
        float lightTime = GetComponent<ParticleSystem>().main.startLifetime.constantMax;

        float startTime = Time.time;
        float endTime = startTime + (lightTime * (1 - (firelight.intensity / lightIntensity)));
        float startIntensity = firelight.intensity;

        GetComponent<AudioSource>().Play();

        GameObject.FindGameObjectWithTag("GameController").GetComponent<EnergyShooter>().islands.Remove(transform.parent.parent);

        //create energy beam
        GameObject beam = Instantiate(energyBeam);
        Vector3 end1 = GameObject.FindGameObjectWithTag("GameController").transform.position;
        Vector3 end2 = transform.parent.parent.position;
        Vector3 diff = end2 - end1;
        beam.transform.position = (end1 + end2) * 0.5f;
        beam.transform.localScale = new Vector3(beam.transform.localScale.x, diff.magnitude / 2f, beam.transform.localScale.z);
        //float rotX = Mathf.Atan2(diff.z, diff.y) * Mathf.Rad2Deg;
        //float rotZ = -Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
        //beam.transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y, rotZ);
        beam.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);

        while (Time.time < endTime) {

            firelight.intensity = startIntensity + lightIntensity * (Time.time - startTime) / (endTime - startTime);
            yield return null;

        }

        firelight.intensity = lightIntensity;
        yield return null;

    }

}
