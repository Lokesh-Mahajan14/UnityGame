//using UnityEngine;

//public class SpawnManagerPath : MonoBehaviour
//{
//    public GameObject[] objectsToSpawn; // Assign bomb and fruit prefabs
//    public Transform[] spawnPoints; // Assign the 3 spawn points
//    public float spawnRate = 2.0f;
//    public float objectSpeed = 5.0f;

//    private float nextSpawnTime;

//    void Start()
//    {
//        nextSpawnTime = Time.time + spawnRate;
//    }

//    void Update()
//    {
//        if (ShouldSpawn())
//        {
//            SpawnObject();
//            nextSpawnTime = Time.time + spawnRate;
//        }
//    }

//    private bool ShouldSpawn()
//    {
//        return Time.time >= nextSpawnTime && GameManagerPath.Instance.IsGameActive;
//    }

//    void SpawnObject()
//    {
//        int laneIndex = Random.Range(0, 3);
//        bool spawnFruit = Random.Range(0, 3) == 0;
//        GameObject objectToSpawn = spawnFruit ? objectsToSpawn[0] : objectsToSpawn[1];

//        GameObject spawnedObject = Instantiate(
//            objectToSpawn,
//            spawnPoints[laneIndex].position,
//            Quaternion.identity
//        );

//        spawnedObject.AddComponent<MovingObjectPath>().Initialize(objectSpeed, laneIndex, spawnFruit);
//    }
//}

using UnityEngine;
using System.Collections.Generic;

public class SpawnManagerPath : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Fruit (0) and Bomb (1) prefabs
    public Transform[] spawnPoints; // 3 spawn positions
    public float spawnInterval = 2.0f;
    public float objectSpeed = 5.0f;

    private float nextSpawnTime;
    private List<GameObject> activeObjects = new List<GameObject>();

    private bool isSpawning = false;
    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    //    public void StartSpawning()
    //{
    //    isSpawning = true;
    //    nextSpawnTime = Time.time + spawnInterval; // Immediate first spawn
    //}

    public void StartSpawning()
    {
        isSpawning = true;
        nextSpawnTime = Time.time; // Start immediately
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }


    void Update()
    {
        //if (Time.time >= nextSpawnTime && GameManagerPath.Instance.IsGameActive)
        //{
        //    SpawnWave();
        //    nextSpawnTime = Time.time + spawnInterval;
        //}

        if (isSpawning && GameManagerPath.Instance.IsGameActive)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnWave();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
    }

    void SpawnWave()
    {
        // Spawn 3 objects at once (2 bombs, 1 fruit)
        int fruitIndex = Random.Range(0, 3); // Random lane for fruit

        for (int i = 0; i < 3; i++)
        {
            bool isFruit = (i == fruitIndex);
            SpawnObject(i, isFruit);
        }
    }

    //void SpawnObject(int laneIndex, bool isFruit)
    //{
    //    GameObject prefab = isFruit ? objectsToSpawn[0] : objectsToSpawn[1];
    //    GameObject obj = Instantiate(prefab, spawnPoints[laneIndex].position, Quaternion.identity);
    //    activeObjects.Add(obj);

    //    var mover = obj.AddComponent<MovingObjectPath>();
    //    mover.Initialize(objectSpeed, laneIndex, isFruit);
    //}

    void SpawnObject(int laneIndex, bool isFruit)
    {
        GameObject prefab = isFruit ? objectsToSpawn[0] : objectsToSpawn[1];
        GameObject obj = Instantiate(prefab, spawnPoints[laneIndex].position, Quaternion.identity);
        obj.tag = isFruit ? "Fruit" : "Bomb"; // Set tag based on type

        var mover = obj.AddComponent<MovingObjectPath>();
        mover.Initialize(objectSpeed); // Simplified initialization
    }

    public void ClearAllObjects()
    {
        foreach (var obj in activeObjects)
        {
            if (obj != null) Destroy(obj);
        }
        activeObjects.Clear();
    }
}