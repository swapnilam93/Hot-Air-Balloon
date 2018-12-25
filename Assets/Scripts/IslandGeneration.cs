using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGeneration : MonoBehaviour {

    [SerializeField]
    private GameObject[] islandPrefabs;

    [SerializeField]
    private int islandCount;

    [SerializeField]
    private Vector2 centerPos;

    [SerializeField]
    private float minDist, maxDist, minHeight, maxHeight, maxAngle, 
        minAngle, islandRad, lateralBuffer, heightBuffer;

    [SerializeField]
    private EnergyShooter energyShooter;

	// Use this for initialization
	void Start () {

        if (islandPrefabs.Length == 0)
            return;

        List<GameObject> islands = new List<GameObject>();

        for (int i = 0; i < islandCount; i++) {

            bool viablePosFound = false;

            Vector3 pos = centerPos;
            while (!viablePosFound) {

                float r = minDist + ((maxDist - minDist) * Mathf.Sqrt(Random.value));
                float angle = Random.Range(maxAngle * Mathf.Deg2Rad, minAngle * Mathf.Deg2Rad);
                pos = new Vector3(r * Mathf.Sin(angle), Random.Range(minHeight, maxHeight), r * Mathf.Cos(angle)) + 
                    new Vector3(centerPos.x, 0f, centerPos.y);

                //viablePosFound = true;

                if (IsViablePoint(pos, islands))
                   viablePosFound = true;

            }

            GameObject island = Instantiate(islandPrefabs[Random.Range(0, islandPrefabs.Length)], pos, Quaternion.identity);
            islands.Add(island);
            energyShooter.islands.Add(island.transform);
            island.transform.eulerAngles = Random.Range(0f, 360f) * Vector3.up; //rotate randomly

        }
		
	}

    bool IsViablePoint(Vector3 pos, List<GameObject> islands) {

        if (islands.Count == 0)
            return true;
        else { //make sure new island isn't conflicting with any existing islands

            foreach (GameObject island in islands) {

                Vector3 islandPos = island.transform.position;
                Vector3 diff = islandPos - pos;
                if (Mathf.Abs(diff.y) < heightBuffer) { //heights close enough for potential conflict

                    float lateralDiff = Mathf.Sqrt(diff.x * diff.x + diff.z * diff.z);
                    if (lateralDiff < islandRad + lateralBuffer) //distance conflict
                        return false;

                }

            }

        }

        return true;

    }

}