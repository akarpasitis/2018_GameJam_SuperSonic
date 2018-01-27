using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
    Rigidbody RB;
    public float rotatespeed = 50f;

	void Awake() {
        RB = GetComponentInChildren<Rigidbody>();
		RB.velocity = transform.forward * speed;
	}


    private void FixedUpdate()
    {
        float turn = speed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(rotatespeed, 0f, 0f);
        RB.MoveRotation(RB.rotation * turnRotation);
    }

    


}
