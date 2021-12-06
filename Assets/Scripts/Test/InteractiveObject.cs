using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    //fire manager
    [SerializeField] private GameObject fireManager = null;

    //fire prefab
    [SerializeField] private GameObject fire = null;
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

    // Start is called before the first frame update
    void Start()
    {
        if (flammable)
        {
            SetupFlammableObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                            //obj.GetComponent<InteractiveObject>().canSpeadFire = true;
                        }
                    }
                }
            }
        }

        if (isBurning && doOnce)
        {
            doOnce = false;
            GameObject fireInstance = Instantiate(fire, transform.position, Quaternion.identity);
            fireInstance.AddComponent<BoxCollider>();
            Instantiate(fireSFX_01, transform.position, Quaternion.identity);
            fireInstance.transform.parent = gameObject.transform;
            fireManager.GetComponent<FireManager>().AddFire(fireInstance);
            //change material/texture to burnt texture
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
