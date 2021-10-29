using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fire = null;
    [SerializeField] private GameObject cube = null;
    private GameObject fireInstance = null;
    private GameObject cubeInstance = null;
    [SerializeField] private float timerStart = 0.0f;
    private float timer = 0.0f;
    private Vector3 spawnPosition;
    private Vector3 startPos = new Vector3(0.0f, 0.0f, 0.0f);
    private int fireCount = 0;
    private Vector3 maxPos;

    private Vector3[] fireLocations = new Vector3[4];

    private bool nextlineX, nextlineY, nextlineZ = false;
    private int xCount, yCount, zCount = 0;

    private bool finished = false;

    bool letsdothisbit = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = timerStart;
        spawnPosition = transform.position;
        maxPos.x = 6;
        maxPos.y = 7;
        maxPos.z = 6;

        fireLocations[0] = new Vector3(0, 0, 0);
        fireLocations[1] = new Vector3(2, 1, 1);
        //fireLocations[2] = new Vector3(1, 3, 1);
        //fireLocations[3] = new Vector3(0, 6, 2);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !finished && letsdothisbit)
        {
            for (int x = 0; x < fireLocations.Length; ++x)
            {
                
                if (fireLocations[x].x == xCount &&
                    fireLocations[x].y == zCount &&
                    fireLocations[x].z == yCount)
                {
                    fireInstance = Instantiate(fire, spawnPosition, Quaternion.identity);
                }
            }
            //cubeInstance = Instantiate(cube, spawnPosition, Quaternion.identity);
            timer = timerStart;
            //spawnPosition.x += 1;
            xCount += 1;

            if (xCount >= maxPos.x)
            {
                nextlineX = true;
                spawnPosition.x = startPos.x;
                //spawnPosition.z += 1;

                //here
                if (zCount + 2 > maxPos.z)
                {
                    nextlineZ = true;
                    spawnPosition.z = startPos.z;
                    //spawnPosition.y += 1;

                    //here 2
                    if (yCount >= maxPos.y)
                    {
                        nextlineY = true;
                        spawnPosition.y = startPos.y;

                    }
                    else
                    {
                        nextlineY = false;
                        spawnPosition.y += 1;
                    }
                }
                else
                {
                    nextlineZ = false;
                    spawnPosition.z += 1;
                }
            }
            else
            {
                nextlineX = false;
                spawnPosition.x += 1;
            }


            if (nextlineX)
            {
                zCount += 1;
                xCount = 0;
            }

            if (nextlineZ)
            {
                yCount += 1;
                zCount = 0;
            }

            if (nextlineY)
            {
                finished = true;
            }

        }
        else if (timer <= 0 && !finished)
        {
            //random fire spawning
            Debug.Log("yep");
        }
    }
}
