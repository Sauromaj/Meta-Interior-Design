using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject chairPrefab; // Assign your chair prefab here
    public Transform canvasTransform; // Assign your Canvas here

    public void SpawnPrefab()
    {
        // Calculate the position 0.5 units along the Canvas's forward vector
        Vector3 spawnPosition = canvasTransform.position + (canvasTransform.forward * -0.5f);
        
        // Instantiate the prefab at that position
        Instantiate(chairPrefab, spawnPosition, canvasTransform.rotation);
    }
}