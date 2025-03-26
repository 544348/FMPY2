using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    // List of prefabs that can be dragged and dropped in the Inspector
    public List<GameObject> prefabs;

    // Adjustable variables for spawning position
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    // Delay between spawns in seconds
    public float spawnDelay = 1f;

    // Toggle to start and stop the spawning
    private bool isSpawning = true;

    // Start spawning prefabs
    private void Start()
    {
        // Start the spawning process
        if (prefabs.Count > 0)
        {
            StartCoroutine(SpawnPrefabs());
        }
    }

    // Coroutine to handle the repeated spawning
    private IEnumerator SpawnPrefabs()
    {
        while (isSpawning)
        {
            // Randomly select a prefab from the list
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Count)];

            // Instantiate the prefab at the specified position with its original rotation
            Instantiate(prefabToSpawn, spawnPosition, prefabToSpawn.transform.rotation);

            // Wait for the specified delay before spawning again
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // To stop the spawning process manually if needed
    public void StopSpawning()
    {
        isSpawning = false;
    }

    // To restart the spawning process
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnPrefabs());
        }
    }
}