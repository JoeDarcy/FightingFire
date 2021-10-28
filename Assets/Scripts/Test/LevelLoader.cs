using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject cube = null;

    public int levelSizeX, levelSizeY, levelSizeZ = 0;

    private const int maxObjects = 500;

    private int currentObjectNum = 0;

    SpawnedObjects spawnedObjects;
    private struct SpawnedObjects
    {
        public GameObject[] gameObject;
        public Vector3[] location;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects.gameObject = new GameObject[maxObjects];
        currentObjectNum = 0;

        for (int x = 0; x < levelSizeX; ++x)
        {
            for (int y = 0; y < levelSizeY; ++y)
            {
                for (int z = 0; z < levelSizeZ; ++z)
                {
                    if (x == levelSizeX - 1  || x == 0 ||
                        z == levelSizeZ - 1  || z == 0 ||
                        y == levelSizeY - 1  || y == 0)
                    {
                        Vector3 newPos = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                        spawnedObjects.gameObject[currentObjectNum] = Instantiate(cube, newPos, Quaternion.identity);
                        currentObjectNum++;

                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
