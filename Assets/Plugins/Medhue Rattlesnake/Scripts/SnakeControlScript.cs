using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class SnakeControlScript : MonoBehaviour
{
	
	public float animSpeed = 1.0f;				// a public setting for overall animator animation speed
	public float lookSmoother = 3f;				// a smoothing setting for camera motion
	
	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private CapsuleCollider col;					// a reference to the capsule collider of the character
	

	static int idleState = Animator.StringToHash("Base Layer.Idle");	

	void Start ()
	{
		// initialising reference variables

		anim = GetComponent<Animator>();					  
		col = GetComponent<CapsuleCollider>();					
		if(anim.layerCount ==2)
			anim.SetLayerWeight(1, 1);
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation


		if(Input.GetButton("StrifeL")) //
		{
			anim.SetBool("StrifeL", true);
			}	
	    else
						{
			anim.SetBool("StrifeL", false);
			}
		
		if(Input.GetButton("StrifeR"))
		{
			anim.SetBool("StrifeR", true);
			}	
	    else
						{
			anim.SetBool("StrifeR", false);
			}
		
		if(Input.GetButton("Running"))               //Trigger to run, space bar on the keyboard
		{
						anim.SetBool("Running", true);
			}	
	    else
						{
				anim.SetBool("Running", false);
			}
		
		if(Input.GetButton("TurnL"))               //Trigger to run, space bar on the keyboard
		{
			anim.SetBool("TurnL", true);
			}	
	    else
						{
			anim.SetBool("TurnL", false);
			}
		if(Input.GetButton("TurnR"))               //Trigger to run, space bar on the keyboard
		{
			anim.SetBool("TurnR", true);
		}	
		else
		{
			anim.SetBool("TurnR", false);
		}
		
		if(Input.GetButton("Jump1"))               //Trigger to run, space bar on the keyboard
		{
			anim.SetBool("Jump1", true);
			}	
	    else
						{
			anim.SetBool("Jump1", false);
			}

		if(Input.GetButton("Strike"))
		{
			anim.SetBool("Strike", true);

		}	
		else
		{
			anim.SetBool("Strike", false);
		}

		if(Input.GetButton("Flex"))               //Trigger to flex, e on the keyboard
		{
			anim.SetBool("Flex", true);
		}
		else
		{
			anim.SetBool("Flex", false);
		}
	}
}
