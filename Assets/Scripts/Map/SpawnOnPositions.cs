using UnityEngine;
 
public class SpawnOnPositions : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform[] spawnPoints;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
 
            GameObject instantiated = GameObject.Instantiate(cubePrefab);
            instantiated.transform.position = randomPoint.position;
        }
    }
}
 