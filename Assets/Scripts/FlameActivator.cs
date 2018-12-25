using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameActivator : MonoBehaviour {

    private AudioSource aud;

    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private Light firelight;
    private float lightTime;
    private float lightIntensity;
    private float lightSpeed;

//    private SteamVR_TrackedController control;

    //1
    private SteamVR_TrackedObject trackedObj;
    //2
    private SteamVR_Controller.Device control
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    [SerializeField]
    private Rigidbody balloon;
    [SerializeField]
    private float upthrust;
    [SerializeField]
    private float maxVertSpeed;

    [SerializeField]
    private GameObject burner;

    [SerializeField]
    private float lightDist;

    [SerializeField]
    private float maxHeight;

    //how many units away from the maximum height the balloon
    //has to be before it starts slowing down
    [SerializeField]
    private float slowDownSpace;

    private bool lit;

    public bool lighterLit;

    [SerializeField]
    private AudioClip lighterOn;
    [SerializeField]
    private AudioClip lighterOff;

    [SerializeField]
    private GameObject groundCheck;

    // Use this for initialization
    void Start () {

        //        control = GetComponent<SteamVR_TrackedController>();

        aud = GetComponent<AudioSource>();

        lightTime = particles.main.startLifetime.constantMax;
        lightIntensity = firelight.intensity;
        firelight.intensity = 0f;
        lightSpeed = lightIntensity / lightTime;

        lit = false;

    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {

        lighterLit = control.GetHairTrigger();
        Vector3 burnerPos = burner.transform.position;
        Vector3 pos = transform.position;
        float dist = (burnerPos - pos).magnitude;

        if (control.GetHairTriggerDown()) {

            aud.clip = lighterOn;
            aud.Play();

        } else if (control.GetHairTriggerUp()) {

            aud.clip = lighterOff;
            aud.Play();

        }

//        lit = Input.GetKey(KeyCode.A);

//        if (lighterLit) {
        if (lighterLit && !lit && dist <= lightDist) {
//        if (Input.GetKeyDown(KeyCode.A)) { 

            lit = true;
//            StopCoroutine(Extinguish());
            particles.Play();
            //            StartCoroutine(LightUp());
            burner.GetComponent<AudioSource>().Play();

        }

//        else {
        else if (!lighterLit || (lit && dist > lightDist)) {
//        else if (Input.GetKeyUp(KeyCode.A)) { 

            lit = false;
//            StopCoroutine(LightUp());
            particles.Stop();
//            StartCoroutine(Extinguish());

        }

        if (lit)
            firelight.intensity += lightSpeed * Time.deltaTime;
        else
            firelight.intensity -= lightSpeed * Time.deltaTime;
        firelight.intensity = Mathf.Clamp(firelight.intensity, 0, lightIntensity);

        if (lit) {
            if (balloon.isKinematic) {
                groundCheck.SetActive(false);
                Invoke("ReactivateLanding", 2f);
                balloon.isKinematic = false;
            }
            balloon.AddForce(Vector3.up * upthrust);
        } else {
            balloon.AddForce(Vector3.down * upthrust);
        }

        if (!balloon.isKinematic) {
            float verticalSpace = maxHeight - transform.position.y;
            float slowConstant = maxVertSpeed * Mathf.Atan(verticalSpace * 2f / slowDownSpace) / (Mathf.PI / 2);
            balloon.velocity = new Vector3(balloon.velocity.x,
                Mathf.Clamp(balloon.velocity.y, 0f, slowConstant),
                balloon.velocity.z);
        }
		
	}

    IEnumerator LightUp () {

        float startTime = Time.time;
        float endTime = startTime + (lightTime * (1 - (firelight.intensity / lightIntensity)));
        float startIntensity = firelight.intensity;

        while (Time.time < endTime) {

            firelight.intensity = startIntensity + lightIntensity * (Time.time - startTime) / (endTime - startTime);
            yield return null;

        }

        firelight.intensity = lightIntensity;
        yield return null;

    }

    IEnumerator Extinguish() {

        float startTime = Time.time;
        float endTime = startTime + (lightTime * (firelight.intensity / lightIntensity));
        float startIntensity = firelight.intensity;

        while (Time.time < endTime) {

            firelight.intensity = startIntensity - lightIntensity * (Time.time - startTime) / (endTime - startTime);
            yield return null;

        }

        firelight.intensity = 0f;
        yield return null;

    }

    void ReactivateLanding() {

        groundCheck.SetActive(true);

    }


}
