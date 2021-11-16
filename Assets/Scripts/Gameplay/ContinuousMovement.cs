using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
	public float speed = 1.0f;
	public XRNode inputSource;
	public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionHeight = 0.2f;
	private float fallingSpeed;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

	private void FixedUpdate() 
	{
        // Updated our character controller position to match our headset
        CapsuleFollowHeadset();

        // Get head yaw for movement direction
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        // Set movement direction to match head direction
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        // Move character in the direction they are looking
        character.Move(direction * (Time.fixedDeltaTime * speed));
        // Gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
	        fallingSpeed = 0.0f;
        }
        else
        {
	        fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        
        character.Move(Vector3.up * (fallingSpeed * Time.fixedDeltaTime));
	}

    // Make the character controller follow the VR rig
    void CapsuleFollowHeadset()
    {
	    character.height = rig.cameraInRigSpaceHeight + additionHeight;
	    Vector3 capsuleCentre = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCentre.x, character.height / 2 + character.skinWidth, capsuleCentre.z);
    }

	bool CheckIfGrounded()
	{
        // Tells us if the character is grounded or not
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
	}
}
