using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
	[SerializeField] private int sceneToLoad = 0;
	[SerializeField] private float timerStart = 0.0f;
	private float timer = 0.0f;
	
	[SerializeField] bool levelComplete = false;
	private bool completeDone = false;
	private Animator liftAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
	    timer = timerStart;

        liftAnimator = GetComponent<Animator>();
    }

	private void Update() 
	{
		// Trigger end of level once all fires are put out
		if (InteractiveObject.totalFiresInScene <= 0)
		{
			levelComplete = true;
		}

		// If the level is complete open the lift doors again
		if (levelComplete == true && completeDone == false)
		{
			liftAnimator.SetBool("levelComplete", true);
			liftAnimator.SetBool("playerEnterLift", false);
			liftAnimator.SetBool("playerExitLift", false);
			completeDone = true;
		}

		// Transition to the next scene
		if (completeDone == true && liftAnimator.GetBool("playerEnterLift") == true)
		{
			timer -= Time.deltaTime;
		}

		if (timer <= 0)
		{
			SceneManager.LoadScene(sceneToLoad);
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
        liftAnimator.SetBool("playerEnterLift", true);
	}

	private void OnTriggerExit(Collider other) 
	{
		liftAnimator.SetBool("playerExitLift", true);
    }
}
