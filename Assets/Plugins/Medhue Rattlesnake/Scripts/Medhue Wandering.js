#pragma strict
 
/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
@script RequireComponent(CharacterController)
 
    var anim : Animator;
    var speed:float = 5;
    var time:float = 1;
    var maxHeadingChange:float = 60;
    var lowspeed = -1;
    var highspeed = 7;
    var waitlow = 3;
    var waithi = 9;
    var heading: float=0;
    var targetRotation: Vector3 ;
 
    function Awake (){
 
        // Set random initial rotation
		transform.eulerAngles = Vector3(0, Random.Range(0,360), 0);  // look in a random direction at start of frame.
        anim = GetComponent("Animator");
        //StartCoroutine
		NewHeadingRoutine ();
    }
 
    function Update (){
		var controller : CharacterController = GetComponent(CharacterController);
        anim.SetFloat("Timing", time);
        anim.SetFloat("Speed", speed);
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * time);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);

    }
 
    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
	while (true){
		NewHeadingRoutine();
		yield WaitForSeconds(time);
		
	}
 
    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    function NewHeadingRoutine (){
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
        speed = Random.Range(lowspeed, highspeed);
        time = Random.Range(waitlow, waithi);


    }