using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {

    [SerializeField]
    private float speed;

    [HideInInspector] public Vector3 dest;

	public IEnumerator Go() {

        Vector3 start = transform.position;
        Vector3 diff = dest - start;
        Invoke("Destruction", diff.magnitude / speed);

        while (true) {

            transform.position = transform.position + diff.normalized * Time.deltaTime * speed;
            yield return null;

        }

    }

    void Destruction () {

        Destroy(this.gameObject);

    }
}
