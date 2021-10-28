using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawnerRandom : MonoBehaviour
{
    [SerializeField] private GameObject fire = null;
    [SerializeField] private GameObject cube = null;
    private GameObject fireInstance = null;
    private GameObject cubeInstance = null;
    [SerializeField] private float timerStart = 0.0f;
    private float timer = 0.0f;
    private Vector3 spawnPosition;
    [SerializeField] private const int maxFires = 10;
    private int currentFires = 0;
    private Vector3[] firePositions = new Vector3[maxFires];

    private bool newPos = false;
    private int temp = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerStart;
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if ((timer <= 0 && (maxFires >= currentFires)) || newPos)
        {
            temp = 0;
            newPos = false;

                // Update spawn position
                spawnPosition.x = (int)(Random.Range(0f, 9f));
            spawnPosition.y = (int)(Random.Range(0f, 0f));//2f));
                spawnPosition.z = (int)(Random.Range(0f, 9f));

                if (firePositions[0] != null)
                {
                    for (int x = 0; x < maxFires; ++x)
                    {
                        if (firePositions[x].x == spawnPosition.x &&
                            firePositions[x].y == spawnPosition.y &&
                            firePositions[x].z == spawnPosition.z)
                        {
                            temp++;
                        }
                    }
                    if (temp == 0)
                    {
                        newPos = true;
                    }
                }
                else
                {
                    // Add fire positions to array
                    firePositions[0] = spawnPosition;
                }

            if (currentFires < firePositions.Length)
            {
                // Add fire positions to array
                firePositions[currentFires] = spawnPosition;
            }


            // Spawn fires
            if (currentFires < maxFires)
            {
                fireInstance = Instantiate(fire, spawnPosition, Quaternion.identity);
                currentFires += 1;
            }
            
        }
    }
}
