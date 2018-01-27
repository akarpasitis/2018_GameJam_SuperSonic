using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Instantiate512cubes : MonoBehaviour {
    public GameObject samplecubeprefab;
    GameObject[] sampleCube = new GameObject[512];
    public float maxscale;
    AudioPier AP;

	void Start () {
        AP = FindObjectOfType<AudioPier>();

		for(int i = 0; i < 512; i++)
        {
            GameObject InstanceSampleCube = (GameObject)Instantiate(samplecubeprefab);
            InstanceSampleCube.transform.position = this.transform.position;
            InstanceSampleCube.transform.parent = this.transform;
            InstanceSampleCube.name = "SampleCube" + i;

            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            InstanceSampleCube.transform.position = Vector3.forward * 100;
            sampleCube[i] = InstanceSampleCube;
        }
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i<512; i++)
        {
            if(sampleCube != null)
            {
                sampleCube[i].transform.localScale = new Vector3(10, (AP.samples[i] * maxscale) + 2, 10);
            }
        }

	}
}
