using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
	[SerializeField] private GameObject rocket = null;
	[SerializeField] private GameObject rocketBurstEffect = null;
    [SerializeField] private GameObject rocketInLauncher = null;
    private GameObject rocketInstance = null;
    [SerializeField] private float rocketSpeed = 0.0f;
    [SerializeField] private Transform rocketSpawnPoint;
    private Vector3 spawnPoint;

    public static bool firing = true;


    // Update is called once per frame
    void Update()
    {
	    if (firing && rocketInstance == null)
	    {
            // Hide the rocket in the launcher
            rocketInLauncher.SetActive(false);

            // Update spawn point
            spawnPoint = new Vector3(rocketSpawnPoint.position.x + 0.4f, rocketSpawnPoint.position.y + 0.4f, rocketSpawnPoint.position.z);
            // Instantiate the rocket to fire
            Instantiate(rocketBurstEffect, spawnPoint, rocketSpawnPoint.rotation);

            // Update spawn point
            spawnPoint = new Vector3(rocketSpawnPoint.position.x, rocketSpawnPoint.position.y, rocketSpawnPoint.position.z);
            // Instantiate the rocket to fire
            rocketInstance = Instantiate(rocket, spawnPoint, rocketSpawnPoint.rotation);

            // Apply velocity to the rocket instance
            rocketInstance.GetComponent<Rigidbody>().velocity = rocketSpeed * rocketSpawnPoint.right;

	    }
    }
}
