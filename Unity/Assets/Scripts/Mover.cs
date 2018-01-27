using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
    Rigidbody RB;

	void Start() {
        RB = GetComponent<Rigidbody>();
		RB.velocity = transform.forward * speed;
	}
}
