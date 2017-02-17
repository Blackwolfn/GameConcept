using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform LookTransform;
    public float planetDistance = 15f;

    public float speed = 5f;
    public float maxVelocityChange = 10.0f;
    public float jumpForce = 5.0f;
    public float groundHeight = 1.1f;
    private float xRotation;
    private float yRotation;

    Rigidbody rb;


    void Start() {
        rb = this.GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void FixedUpdate () {

        

        RaycastHit groudedHit;
        bool grounded = Physics.Raycast( transform.position, -transform.up, out groudedHit, groundHeight );

        if (grounded) {
            Vector3 forward = Vector3.Cross( transform.up, -LookTransform.right ).normalized;
            Vector3 right = Vector3.Cross( transform.up, -LookTransform.forward ).normalized;
            Vector3 targetVelocity = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed;

            Vector3 velocity = transform.InverseTransformDirection(rb.velocity);
            velocity.y = 0;
            velocity = transform.TransformDirection(velocity);
            Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp( velocityChange.x, -maxVelocityChange, maxVelocityChange );
            velocityChange.z = Mathf.Clamp( velocityChange.z, -maxVelocityChange, maxVelocityChange );
            velocityChange.y = 0;
            velocityChange = transform.TransformDirection(velocityChange);

            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (Input.GetButton("Jump")) {
                rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
                RaycastHit planetHit;
                bool newPlanet = Physics.Raycast( transform.position, transform.up, out planetHit, planetDistance );
                if (newPlanet) {
                    Debug.Log( "Switching to "+planetHit.collider.name);
                    transform.GetComponent<PlanetGravity>().setPlanet(planetHit.collider.transform);
                }
            }
        }

        Debug.DrawRay( transform.position, transform.up * planetDistance, Color.blue );

        //Vector3 v = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
        //rb.AddRelativeForce( v * speed);

    }
}
