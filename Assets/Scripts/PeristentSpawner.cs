using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeristentSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] objectsToSpawn;

    static bool spawned = false;

    private void Awake()
    {
        if (spawned) return;

        SpawnPersistentObjects();

        spawned = true;
    }

    private void SpawnPersistentObjects()
    {
        foreach(GameObject gameObject in objectsToSpawn)
        {
            GameObject persistentObject = Instantiate(gameObject);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
