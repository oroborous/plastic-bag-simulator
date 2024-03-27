using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Room : MonoBehaviour {
    public SpriteRenderer gooseSprite;
    private Rigidbody2D goose;

	// Use this for initialization
	void Start () {
        transform.Find("Box Fan").gameObject.SetActive(Random.value < 0.4);

        transform.Find("Desk Fan").gameObject.SetActive(Random.value< 0.4);

        transform.Find("Street Vent").gameObject.SetActive(Random.value< 0.4);

        transform.Find("Tabby Cat").gameObject.SetActive(Random.value < 0.3);

        transform.Find("Point Cat").gameObject.SetActive(Random.value< 0.3);

        goose = transform.Find("Goose").GetComponent<Rigidbody2D>();
        goose.velocity = new Vector2(-1.0f, 0);

        float gooseY = goose.position.y;
        goose.position = new Vector2(goose.position.x, Random.Range(gooseY - 3.0f, gooseY + 3.0f));
    }
	
	// Update is called once per frame
	void Update () {
        if (Random.value < 0.01)
        {
            goose.velocity = new Vector2(-1 * goose.velocity.x, 0);

            if (goose.velocity.x > 0 && gooseSprite.flipX == false)
            {
                gooseSprite.flipX = true;
            }

            if (goose.velocity.x < 0 && gooseSprite.flipX == true)
            {
                gooseSprite.flipX = false;
            }
        }

    }
}
