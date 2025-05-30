﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlatformGenerator : MonoBehaviour {

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    public float platformWidth;

	// Use this for initialization
	void Start () {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, thePlatform.transform.position.z);

            Instantiate(thePlatform, transform.position, transform.rotation);
        }

	}
}
