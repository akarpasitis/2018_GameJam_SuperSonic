using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_system : MonoBehaviour {

	Animator trapAnim;
	AudioSource trapAs;

	bool trapOn = false;

	// Use this for initialization
	void Start () {
		trapAnim = GetComponent<Animator>();
		trapAs = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider trap){
		if(trap.tag == "Bat"){
		trapAnim.SetTrigger("activateTrap"); 
		trapOn = true; 
		trapAs.Play();
		}
	}
}
