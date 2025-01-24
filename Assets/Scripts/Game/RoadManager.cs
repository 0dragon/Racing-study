using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public Transform player;
    public float spawnDistance = 30.0f;
    public float roadLength = 20.0f;
    
    private List<GameObject> roads = new List<GameObject>();
    private float lastSpawnPosition = 0.0f;
    
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnRoad();
        }
    }
    
    void Update()
    {
        if (player.position.z + spawnDistance > lastSpawnPosition)
        {
            SpawnRoad();
        }
        RemoveRoad();
    }

    void SpawnRoad()
    {
        Vector3 spawnPosition = new Vector3(0, 0, lastSpawnPosition + roadLength);
        GameObject newRoad = Instantiate(roadPrefab, spawnPosition, Quaternion.identity);
        roads.Add(newRoad);
        lastSpawnPosition += roadLength;
    }

    void RemoveRoad()
    {
        if (roads.Count > 4)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }
}
