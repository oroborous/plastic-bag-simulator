using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_GooseHonk : MonoBehaviour {

    public AudioClip honk;
    public float dist;

    private AudioSource source;
   // private float lowPitchRange = .75F;
   // private float highPitchRange = 1.25F;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(honk, 1f);
        Debug.Log("HONK");
    }
	
	// Update is called once per frame
	void Update () {
        float rand = Random.Range(1, 100);
        if (rand <= 1)
        {
            source.PlayOneShot(honk, 0.5f);
            Debug.Log("Random HONK");
        }
	}
}
