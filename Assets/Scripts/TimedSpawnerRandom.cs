﻿using System.Collections;
using UnityEngine;

/**
 * This component instantiates a given prefab at random time intervals and random bias from its object position.
 */
public class TimedSpawnerRandom: MonoBehaviour {
    [SerializeField] Mover prefabToSpawn;
    [SerializeField] Vector3 velocityOfSpawnedObject;
    [Tooltip("Minimum time between consecutive spawns, in seconds")] [SerializeField] float minTimeBetweenSpawns = 1f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")] [SerializeField] float maxTimeBetweenSpawns = 3f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")] [SerializeField] float maxXDistance = 0.5f;
    [Tooltip("Maximum distance in Y between spawner and spawned objects, in meters")] [SerializeField] float maxYDistance = 0.5f;
    [SerializeField] Mover SuperPrefabToSpawn; // super Lion
    [Tooltip("choose probability 1 out of x")] [SerializeField] int SuperProbability=4; // if '5' so 1 out of '5'
    [SerializeField] bool IsBoss; // true for boss
    void Start() {
        this.StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine() {
        while (true) {
            float timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawns);
            Vector3 positionOfSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                transform.position.y + Random.Range(-maxYDistance, +maxYDistance),
                transform.position.z);
            int Chance = Random.Range(1, SuperProbability);
            if (IsBoss & Chance == 1)
            {
                GameObject newObject = Instantiate(SuperPrefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
                newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
            }
            else
            {
                GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
                newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
            }
        }
    }
}
