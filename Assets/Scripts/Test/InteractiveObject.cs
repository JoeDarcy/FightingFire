using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    //fire prefab
    [SerializeField] private GameObject fire = null;

    //object basic properties
    [SerializeField] public bool flammable = false;
    [SerializeField] public bool canFireSpread = false; //sometime later, change to false and change the texture to a burnt variation
    private bool isSpreading = false;
    private bool burntOut = false;
    private CapsuleCollider normalSpreadColliderHorizontalX = null;
    private CapsuleCollider normalSpreadColliderHorizontalZ = null;
    private CapsuleCollider normalSpreadColliderVertical = null;
    private CapsuleCollider closeSpreadCollider = null;

    //normal fire spreading properties
    [SerializeField] private float spreadRangeHorizontal = 5.0f;
    [SerializeField] private float timeToSpreadHorizontal = 5.0f;
    [SerializeField] private float spreadRangeVertical = 5.0f;
    [SerializeField] private float timeToSpreadVertical = 5.0f;

    //close fire spreading properties
    [SerializeField] private bool closeSpreading = false;
    [SerializeField] private float closeSpreadRange = 5.0f;
    [SerializeField] private float timeToCloseSpread = 5.0f;

    private float timerStart = 0.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (flammable)
        {
            gameObject.tag = "Flammable";

            normalSpreadColliderHorizontalX = gameObject.AddComponent<CapsuleCollider>();
            normalSpreadColliderHorizontalX.radius = gameObject.transform.localScale.x * 1.5f; ;
            normalSpreadColliderHorizontalX.height = spreadRangeHorizontal * normalSpreadColliderHorizontalX.radius * 2;
            normalSpreadColliderHorizontalX.direction = 0;

            normalSpreadColliderHorizontalZ = gameObject.AddComponent<CapsuleCollider>();
            normalSpreadColliderHorizontalZ.radius = gameObject.transform.localScale.z * 1.5f;
            normalSpreadColliderHorizontalZ.height = spreadRangeHorizontal * normalSpreadColliderHorizontalZ.radius * 2;
            normalSpreadColliderHorizontalZ.direction = 2;

            normalSpreadColliderVertical = gameObject.AddComponent<CapsuleCollider>();
            normalSpreadColliderVertical.radius = gameObject.transform.localScale.y * 1.5f;
            normalSpreadColliderVertical.height = spreadRangeVertical * normalSpreadColliderVertical.radius * 2;
            normalSpreadColliderVertical.direction = 1;

            closeSpreadCollider = gameObject.AddComponent<CapsuleCollider>();
            closeSpreadCollider.radius = closeSpreadRange;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
