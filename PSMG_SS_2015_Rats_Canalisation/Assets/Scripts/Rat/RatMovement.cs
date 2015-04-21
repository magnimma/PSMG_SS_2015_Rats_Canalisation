using UnityEngine;
using System.Collections;

public class RatMovement : MonoBehaviour {

    public float rotationSpeed = 1f;
	public static float generalMovementSpeed = 3f;
    public float movementSpeed = generalMovementSpeed;
    public float jumpSpeed = 20f;
    public float RayCastLength = .1f;
    public bool isGrounded;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded){
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
        }


        checkIfGrounded();
        Move(verticalInput);
        if (isGrounded)
        {
            Turn(horizontalInput);
        }
        
        
    }


    private void checkIfGrounded()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -.1f, 0), RayCastLength))
        {
            isGrounded = true;
        }
        else isGrounded = false;
    }

    private void Move(float inputSignal)
    {
        Vector3 newPosition = transform.forward.normalized * inputSignal * movementSpeed * Time.deltaTime;
        
        GetComponent<Rigidbody>().MovePosition(transform.position + newPosition);
    }

    private void Turn(float inputSignal)
    {
        float angle = inputSignal * rotationSpeed;
        transform.Rotate(transform.up * angle);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float runSpeed = 5f;
		float slowSpeed = 1f;
		//Run
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			movementSpeed = runSpeed;
		} 
		//Slow
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			movementSpeed = slowSpeed;
		}
		//Normalize
		if (Input.GetKeyUp (KeyCode.LeftControl)||Input.GetKeyUp (KeyCode.LeftShift)) {
			movementSpeed = generalMovementSpeed;
		}
	}
}
