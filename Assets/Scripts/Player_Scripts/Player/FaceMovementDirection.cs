using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class FaceMovementDirection : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;
	
	private bool useVCR;
	private InputVCR vcr;
	CharacterMotorCS motor;

	void Awake (){
		motor = GetComponent<CharacterMotorCS>();
	}

	void Update ()
	{
		if (motor != null && !motor.GetControllable ()) {
			return;
		}
		float moveHorizontal;
		float moveVertical;

		if ( useVCR )
		{
			moveHorizontal = vcr.GetAxis ("Horizontal");
			moveVertical = vcr.GetAxis ("Vertical");
		}
		else
		{
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");
		}


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		if(Mathf.Abs(moveHorizontal) >= 0.2 || Mathf.Abs(moveVertical) >= 0.2)
		{
			transform.rotation = Quaternion.LookRotation(movement);
		}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		
		Transform root = transform;
		while ( root.parent != null )
			root = root.parent;
		vcr = root.GetComponent<InputVCR>();
		useVCR = vcr != null;
	}
}