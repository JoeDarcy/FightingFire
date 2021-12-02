using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
	[SerializeField] private GameObject rocket = null;
    [SerializeField] private GameObject rocketInLauncher = null;
    private GameObject rocketInstance = null;

    [SerializeField] private Vector3 rocketSpawnPoint;

    public static bool firing = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (firing && rocketInstance == null)
	    {
            // Hide the rocket in the launcher
            rocketInLauncher.SetActive(false);

            // Set rocket spawn point
            rocketSpawnPoint += transform.position;

            // Instantiate the rocket to fire
            rocketInstance = Instantiate(rocket, rocketSpawnPoint, Quaternion.identity);
	    }
    }
}
