using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
    Rigidbody RB;
    public float rotatespeed = 50f;
    public GameObject Explosion;
    bool hit;
    public float time = 1;

	void Awake() {
        RB = GetComponentInChildren<Rigidbody>();
		RB.velocity = transform.forward * speed;
        Explosion.SetActive(false);
	}


    private void FixedUpdate()
    {
        float turn = speed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(rotatespeed, 0f, 0f);
        RB.MoveRotation(RB.rotation * turnRotation);

        if(hit == true)
        {
            time -= Time.deltaTime;
            Explosion.SetActive(true);
        }
        if (time <= 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
