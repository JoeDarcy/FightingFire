using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    //temp
    [SerializeField] private bool scriptEnabled = false;
    [SerializeField] private GameObject cubePrefab = null;

    public int levelSizeX, levelSizeY, levelSizeZ = 0;
    private int currentObjectNum = 0;

    private const int maxObjects = 500;

    SpawnedObjects spawnedObjects;
    private struct SpawnedObjects
    {
        public GameObject[] gameObject;
        public Vector3[] location;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (scriptEnabled)
        {
            spawnedObjects.gameObject = new GameObject[maxObjects];
            spawnedObjects.location = new Vector3[maxObjects];

            currentObjectNum = 0;

            for (int x = 0; x < levelSizeX; ++x)
            {
                for (int y = 0; y < levelSizeY; ++y)
                {
                    for (int z = 0; z < levelSizeZ; ++z)
                    {
                        if (x == levelSizeX - 1 || x == 0 ||
                            z == levelSizeZ - 1 || z == 0 ||
                            y == levelSizeY - 1 || y == 0)
                        {
                            Vector3 newPos = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                            spawnedObjects.gameObject[currentObjectNum] = Instantiate(cubePrefab, newPos, Quaternion.identity);
                            spawnedObjects.location[currentObjectNum] = newPos;
                            currentObjectNum++;
                        }
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
