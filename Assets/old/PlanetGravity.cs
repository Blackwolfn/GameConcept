using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour {

    public Transform planet;
    public bool AlignToPlanet;
    public float gravity = 9.8f;

    Rigidbody rb;

    void Start() {
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 gravityDirection = (planet.position - transform.position).normalized;

        rb = transform.GetComponent<Rigidbody>();
        Vector3 force = gravityDirection * gravity;
        rb.AddForce(force, ForceMode.Acceleration);
        Debug.DrawRay(transform.position, force, Color.red);

        if (AlignToPlanet) {
            Quaternion q = Quaternion.FromToRotation( transform.up, -gravityDirection );
            q = q * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
        }
    }

    public void setPlanet(Transform planet) {
        this.planet = planet;
    }
	
}
