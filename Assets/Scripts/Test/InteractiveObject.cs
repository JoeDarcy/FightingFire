using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    //fire prefab
    [SerializeField] private GameObject fire = null;

    //object basic properties
    [SerializeField] public bool flammable = false;
    [SerializeField] public bool canSpeadFire = false; //sometime later, change to false and change the texture to a burnt variation
    [SerializeField] private bool isBurning = false;
    private string flammableTag = "Flammable";
    private bool isSpreading = false; //might not need
    private bool burntOut = false;
    private List<GameObject> abc = new List<GameObject>();// = null;
    private bool doOnce = true;
    private CapsuleCollider normalSpreadColliderHorizontalX = null;
    private CapsuleCollider normalSpreadColliderHorizontalZ = null;
    private CapsuleCollider normalSpreadColliderVertical = null;
    private CapsuleCollider closeSpreadCollider = null;
    private Rigidbody rigidbody = null;

    //normal fire spreading properties
    [SerializeField] private float spreadRangeHorizontal = 2.0f;
    [SerializeField] private float timeToSpreadHorizontal = 3.0f;
    [SerializeField] private float spreadRangeVertical = 1.5f;
    [SerializeField] private float timeToSpreadVertical = 2.0f;

    //close fire spreading properties
    [SerializeField] private bool closeSpreading = false;
    [SerializeField] private float closeSpreadRange = 1.0f;
    [SerializeField] private float timeToCloseSpread = 0.5f;

    private float timerHorizontal = 0.0f;
    private float timerVertical = 0.0f;
    private float timerClose = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (flammable)
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
    }

    // Update is called once per frame
    void Update()
    {


        if (isBurning && canSpeadFire)
        {
            timerHorizontal += Time.deltaTime;
            timerVertical += Time.deltaTime;
            timerClose += Time.deltaTime;
            if (timerClose >= timeToCloseSpread * 2)
            {
                timerClose = 0.0f;
                if (abc.Count > 0)
                {
                    foreach (GameObject obj in abc)
                    {
                        if (!obj.GetComponent<InteractiveObject>().isBurning)
                        {
                            obj.GetComponent<InteractiveObject>().isBurning = true;
                            obj.GetComponent<InteractiveObject>().canSpeadFire = true;
                        }
                    }
                }
            }
        }

        if (isBurning && doOnce)
        {
            doOnce = false;
            Instantiate(fire, transform.position, Quaternion.identity);
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
            if (other.CompareTag(flammableTag))// && isBurning) //isBurning should be true...
            {
                bool burning = other.gameObject.GetComponent<InteractiveObject>().isBurning;
                bool hasBurntOut = other.gameObject.GetComponent<InteractiveObject>().burntOut;
                if (abc.Count > 0)
                {
                    if (!abc.Contains(other.gameObject) && !burning && !hasBurntOut)
                    {
                        abc.Add(other.gameObject);
                    }
                }
                else if (other.CompareTag(flammableTag))
                {
                    abc.Add(other.gameObject);
                }
            }
        }

    }
}
