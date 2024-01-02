using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteliMapPro;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private InteliMapGenerator generator;
    [SerializeField] private int chunkWidth = 12;
    [SerializeField] private int chunkOverlap = 2;

    private float cameraStart;
    private float lastChunkLocation;

    // Start is called before the first frame update
    private void Start()
    {
        InitializeGenerator();
    }

    // Update is called once per frame
    private void Update()
    {
        GenerateChunks();
    }

    private void InitializeGenerator()
    {
        cameraStart = mainCamera.position.x;

        // Set the x size of the generators boundsToFill to be equal to the chunk width
        generator.boundsToFill.size = new Vector3Int(chunkWidth, generator.boundsToFill.size.y, 1);
    }

    private void GenerateChunks()
    {
        // If the main camera surpasses the chunk bounds
        if (mainCamera.position.x - cameraStart > lastChunkLocation)
        {
            // Generate the next chunk. (Asyncronously so it doesn't cause lag spikes as generation is quite an expensive operation)
            generator.StartGenerationAsync();
            // generator.StartGenerationAsyncWithSeed(1234); can also generate with a seed

            // Move the chunk forward
            generator.boundsToFill.position += Vector3Int.right * (chunkWidth - chunkOverlap);
            lastChunkLocation += chunkWidth - chunkOverlap;
        }
    }
}
