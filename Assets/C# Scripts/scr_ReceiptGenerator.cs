using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ReceiptGenerator : MonoBehaviour {

    //public GameObject thePlatform;
    public GameObject theReceipt;
    public GameObject theGoldReceipt;
    public Transform generationPoint;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    public float platformWidth;
    public float distanceBetween;

    // Use this for initialization
    void Start () {
        /*
        float spawnChance = Random.Range(0f, 2f);
        while (spawnChance > -1)
        {
            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(theReceipt, pos, transform.rotation);
            spawnChance--;
        }
        */
        platformWidth = 10;//  thePlatform.GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, 0);
            float spawnChance = Random.Range(0f, 1f);
            while (spawnChance > -1)
            {
                Vector2 pos = new Vector2( transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY, maxY));
                float golden = Random.Range(0f, 25f);
                if (golden < 24)
                {
                    Instantiate(theReceipt, pos, transform.rotation);
                }
                else
                {
                    Instantiate(theGoldReceipt, pos, transform.rotation);
                }
                spawnChance--;
            }
        }  
    }
    
}
