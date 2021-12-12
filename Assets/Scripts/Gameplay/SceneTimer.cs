using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
	[SerializeField] private int sceneToLoad = 0;
    [SerializeField] private float timerStart = 0.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
	    timer = timerStart;
    }

    // Update is called once per frame
    void Update()
    {
	    timer -= Time.deltaTime;

        // Change scene when timer runs out
        if (timer <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
