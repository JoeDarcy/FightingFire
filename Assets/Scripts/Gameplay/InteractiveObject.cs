using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class InteractiveObject : MonoBehaviour
{
    //fire manager
    [SerializeField] private GameObject fireManager = null;

    //fire prefab
    [SerializeField] private GameObject fire = null;
    private GameObject fireInstance = null;
    VisualEffect fireVFX = null;
    // Temporary hits taken variable
    private int hitTemp = 0;
    // Maximum hits the fire can take
    private int hitMax = 10;
    // Fire sound effects
    [SerializeField] private GameObject fireSFX_01 = null;
    [SerializeField] private GameObject fireSFX_02 = null;

    //object basic properties
    [SerializeField] public bool flammable = false;
    [SerializeField] public bool canSpeadFire = false; //sometime later, change to false and change the texture to a burnt variation
    [SerializeField] private bool isBurning = false;
    private string flammableTag = "Flammable";
    private bool isSpreading = false; //might not need
    private bool burntOut = false;
    private List<GameObject> collidedObjects = new List<GameObject>();// = null;
    private bool doOnce = true;
    private CapsuleCollider normalSpreadColliderHorizontalX = null;
    private CapsuleCollider normalSpreadColliderHorizontalZ = null;
    private CapsuleCollider normalSpreadColliderVertical = null;
    private CapsuleCollider closeSpreadCollider = null;
    private Rigidbody rigidbody = null;

    //normal fire spreading properties
    [SerializeField] private float spreadRangeHorizontal = 2.0f;
    [SerializeField] private float timeToSpreadHorizontal = 8.0f;
    [SerializeField] private float spreadRangeVertical = 1.5f;
    [SerializeField] private float timeToSpreadVertical = 4.0f;

    //close fire spreading properties
    [SerializeField] private bool closeSpreading = false;
    [SerializeField] private float closeSpreadRange = 1.0f;
    [SerializeField] private float timeToCloseSpread = 1.5f;

    private float timerHorizontal = 0.0f;
    private float timerVertical = 0.0f;
    private float timerClose = 0.0f;

    public static int totalFiresInScene = 0;
    public static bool fireCountStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        fireCountStarted = false;

        if (flammable)
        {
            SetupFlammableObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Output total fire count
        Debug.Log("Active fires: " + totalFiresInScene);

        if (isBurning && canSpeadFire)
	    {
		    timerHorizontal += Time.deltaTime;
		    timerVertical += Time.deltaTime;
		    timerClose += Time.deltaTime;
		    if (timerClose >= timeToCloseSpread)
		    {
			    timerClose = 0.0f;
			    if (collidedObjects.Count > 0)
			    {
				    foreach (GameObject obj in collidedObjects)
				    {
					    if (!obj.GetComponent<InteractiveObject>().isBurning)
					    {
						    obj.GetComponent<InteractiveObject>().isBurning = true;
					    }
				    }
			    }
		    }
	    }

	    if (isBurning && doOnce)
	    {
		    doOnce = false;

            // Instantiate fire
		    fireInstance = Instantiate(fire, transform.position, Quaternion.identity);

            // Only count the fire you can see with normal vision
		    if (gameObject.transform.parent.CompareTag("Normal_Vision"))
		    {
			    totalFiresInScene += 1;
			    fireCountStarted = true;
            }

		    // Instantiate Fire sound effect
		    if (fireSFX_01 != null)
		    {
			    Instantiate(fireSFX_01, transform.position, Quaternion.identity);
		    }

		    fireInstance.transform.parent = gameObject.transform;
		    fireManager.GetComponent<FireManager>().AddFire(gameObject);
	    }

	    // Control the size of fireInstance as it is hit by player
	    if (fireInstance != null)
	    {
		    if (fireVFX == null)
		    {
			    // Set fire VFX
			    fireVFX = fireInstance.GetComponentInChildren<VisualEffect>();
		    }

		    // Check for an exposed attribute in the Visual Effect component
		    //bool hasString = fireVFX.HasFloat("Flame_Size");

		    // Reduce size as damage is taken
		    if (GetComponentInChildren<CollisionTest>().hitCounter > hitTemp &&
		        GetComponentInChildren<CollisionTest>().hitCounter < hitMax)
		    {
			    // Set the size of the flames in the VFX graph
			    fireVFX.SetFloat("Flame_Size", fireVFX.GetFloat("Flame_Size") - 0.2f);
		    }

		    // Store current hits taken to check against the updated value next time round
		    hitTemp = GetComponentInChildren<CollisionTest>().hitCounter;

		    // Destroy fire VFX when fire is dead (set size to 0)
		    if (GetComponentInChildren<CollisionTest>().hitCounter >= hitMax)
		    {
                // Decrement totalFiresInScene
                if (fireVFX.GetFloat("Flame_Size") > 0.0f && gameObject.transform.parent.CompareTag("Normal_Vision"))
                {
	                totalFiresInScene -= 1;
                }

                // Turn off the fire sound effect
                fireSFX_01.SetActive(false);
                fireSFX_02.SetActive(false);

                // Set the size of the flames in the VFX graph
                fireVFX.SetFloat("Flame_Size", 0.0f);
			    // Set the size of the embers in the VFX graph
			    fireVFX.SetFloat("Ember_Size", 0.0f);
			    // Set the size of the smoke in the VFX graph
			    fireVFX.SetFloat("Smoke_Size", 1.0f);
		    }
	    }
    }


    void SetupFlammableObject()
    {
        gameObject.tag = flammableTag;

        normalSpreadColliderHorizontalX = gameObject.AddComponent<CapsuleCollider>();
        normalSpreadColliderHorizontalX.isTrigger = true;
        normalSpreadColliderHorizontalX.radius = gameObject.transform.localScale.x * 1.5f; ;
        normalSpreadColliderHorizontalX.height = spreadRangeHorizontal * normalSpreadColliderHorizontalX.radius * 2;
        normalSpreadColliderHorizontalX.direction = 0; //0 is the X axis index

        normalSpreadColliderHorizontalZ = gameObject.AddComponent<CapsuleCollider>();
        normalSpreadColliderHorizontalZ.isTrigger = true;
        normalSpreadColliderHorizontalZ.radius = gameObject.transform.localScale.z * 1.5f;
        normalSpreadColliderHorizontalZ.height = spreadRangeHorizontal * normalSpreadColliderHorizontalZ.radius * 2;
        normalSpreadColliderHorizontalZ.direction = 2; //2 is the Z axis index

        normalSpreadColliderVertical = gameObject.AddComponent<CapsuleCollider>();
        normalSpreadColliderVertical.isTrigger = true;
        normalSpreadColliderVertical.radius = gameObject.transform.localScale.y * 1.5f;
        normalSpreadColliderVertical.height = spreadRangeVertical * normalSpreadColliderVertical.radius * 2;
        normalSpreadColliderVertical.direction = 1; //1 is the Y axis index

        closeSpreadCollider = gameObject.AddComponent<CapsuleCollider>();
        closeSpreadCollider.isTrigger = true;
        closeSpreadCollider.radius = closeSpreadRange;

        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.angularDrag = 0.0f;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //**************************************************************************************************************
        if (other.CompareTag(flammableTag)) //will have to do lots of different checks for if its the close
                                  // collider (sphere), etc as currently close, horizontal
                                  // and vertical colliders will start the close spread collider
        //**************************************************************************************************************
        {
            if (other.gameObject.GetComponent<CapsuleCollider>().radius == other.gameObject.GetComponent<InteractiveObject>().closeSpreadRange)
            {
                //do a sphere overlap test 
                
            }

            if (isBurning) //ontriggerenter will only call at the start when it shouldnt spread here
            {
                bool burning = other.gameObject.GetComponent<InteractiveObject>().isBurning;
                bool hasBurntOut = other.gameObject.GetComponent<InteractiveObject>().burntOut;
                if (collidedObjects.Count > 0)
                {
                    if (!collidedObjects.Contains(other.gameObject) && !burning && !hasBurntOut)
                    {
                        collidedObjects.Add(other.gameObject);
                    }
                }
                else if (other.CompareTag(flammableTag))
                {
                    collidedObjects.Add(other.gameObject);
                }
            }
        }
    }
}
