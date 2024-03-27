using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scr_PlayerMovement : MonoBehaviour {
    public GameObject whooshHorizontal, whooshVertical;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public Text receiptText;
    public string sceneName;

    public Vector2 xDirection;
    public Vector2 yDirection;

    private Animator myAnimator;
    private static int receiptScore;

    public AudioClip fanGust;
    public AudioClip Hitsound;
    public AudioClip PickupBlip;
    public AudioClip Ptoasty;
    public AudioClip honk;
    public AudioClip meow;
    public AudioClip standardMusic;
    public AudioClip altMusic;

    public AudioSource source;
    public AudioSource musicSource;
    public AudioSource musicSourceAlt;

    private float lowPitchRange = .75F;
    private float highPitchRange = 1.25F;
    private bool isDead = false;

    public static int getReceipts()
    {
        return receiptScore;
    }

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();

        receiptScore = 0;
        if (PlayerPrefs.GetInt("Ptoasty") == 1)
        {
            myAnimator.SetBool("Ptoasty", true);
        }

        if (PlayerPrefs.GetInt("Alternate") == 0)
        {
            musicSource.Play();
            musicSourceAlt.Stop();

        }
        else if (PlayerPrefs.GetInt("Alternate") == 1 )
        {
            musicSource.Stop();
            musicSourceAlt.Play();
        }

        rb.AddForce(xDirection * 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        // Velocity must remain 0 after player dies
        if (!isDead && Input.GetAxis("Horizontal") > 0)
        {
            if (rb.velocity.magnitude < 50f)
            {
                rb.AddForce(xDirection * Time.deltaTime);
            }
        }
        if (!isDead && Input.GetAxis("Horizontal") < 0) {
            rb.AddForce(- xDirection * Time.deltaTime);
        }
        if (!isDead && Input.GetAxis("Vertical") > 0)
        {
            if (rb.position.y < 5)
            {
                rb.AddForce(yDirection * Time.deltaTime);
            }
        }
        if (!isDead && Input.GetAxis("Vertical") < 0)
        {
            rb.AddForce(-yDirection * Time.deltaTime);
        }

        // Caps the x velocity
        if (rb.velocity.x > 5)
        {
            // Slowly reduce velocity back to 5 to allow for gust boost
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }
        else if (rb.velocity.x < -5)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y);
        }
       // Debug.Log("Velocity: " + rb.velocity.x);

        // ----- animation control -----
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            myAnimator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        else
        {
            myAnimator.SetFloat("Speed", Mathf.Abs(rb.velocity.y));
        }

        // flipping the sprite to face the correct direction
        if (rb.velocity.x < 0 && sprite.flipX == false)
        {
            sprite.flipX = true;
        }

        if (rb.velocity.x > 0 && sprite.flipX == true)
        {
            sprite.flipX = false;
        }

    }


    // ----- Interactions -----
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && PlayerPrefs.GetInt("Ptoasty") == 1)
        {
            //Debug.Log("Oof.mp3");
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(Ptoasty, 2.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Take Damage 
        if (collision.gameObject.tag == "Enemy")
        {
            //source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(Hitsound, 2.0f);

            StartCoroutine("GameOver");
        }

        // Goose
        if (collision.gameObject.tag == "Goose")
        {
            //source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(honk, 2.0f);

            StartCoroutine("GameOver");
        }

        // Collect Receipts
        else if (collision.gameObject.tag == "Pickup")
        {
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(PickupBlip, 2.0f);

            Destroy(collision.gameObject);
            receiptScore++;
            // Debug.Log(receiptScore);
            receiptText.text = "Receipts: " + receiptScore.ToString("n0");
        }

        else if (collision.gameObject.tag == "GoldPickup")
        {
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(PickupBlip, 2.0f);

            Destroy(collision.gameObject);
            receiptScore += 5;
            // Debug.Log(receiptScore);
            receiptText.text = "Receipts: " + receiptScore.ToString("n0");
        }

        // Interact with fans
        else if (collision.gameObject.tag == "GustVertical")
        {
            float direction = 1;
            if (rb.velocity.x < 0)
            {
                direction = -direction;
            }

            source.PlayOneShot(fanGust, 2.0f);

            rb.AddForce(xDirection * direction * Random.Range(20, 40) * Time.deltaTime);
            rb.AddForce(yDirection * Random.Range(60, 70) * Time.deltaTime);

            whooshVertical.transform.position = new Vector2(collision.gameObject.transform.position.x + .15f, collision.gameObject.transform.position.y + 0.5f);
            whooshVertical.SetActive(true);

            StartCoroutine("HideVentAnimation");
        }
        // Interact with vents
        else if (collision.gameObject.tag == "GustHorizontal")
        {
            float direction = 1;
            if (rb.velocity.x < 0)
            {
                direction = -direction;
            }

            source.PlayOneShot(fanGust, 2.0f);

            rb.AddForce(xDirection * direction * Random.Range(60, 80) * Time.deltaTime);
            rb.AddForce(yDirection * Random.Range(-10, 30) * Time.deltaTime);

            float distance = 1.25f * direction;
            whooshHorizontal.transform.position = new Vector2(collision.gameObject.transform.position.x + distance, collision.gameObject.transform.position.y);
            whooshHorizontal.GetComponent<SpriteRenderer>().flipX = direction < 0;
            whooshHorizontal.SetActive(true);

            StartCoroutine("HideFanAnimation");
        }
        // Trigger cats jumping
        else if (collision.gameObject.tag == "Cat" || collision.gameObject.tag == "Catenemy")
        {
            source.PlayOneShot(meow, 2.0f);

            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("Bat");

            if (collision.gameObject.tag == "Catenemy")
            {
                source.pitch = Random.Range(lowPitchRange, highPitchRange);

                IEnumerator coroutine = OnCompleteBatAnimation(anim);
                StartCoroutine(coroutine);
            }
        }
    }


    IEnumerator OnCompleteBatAnimation(Animator anim)
    {
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
             yield return null;
        }

        StartCoroutine("GameOver");
    }

    IEnumerator HideFanAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        whooshHorizontal.SetActive(false);
    }

    IEnumerator HideVentAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        whooshVertical.SetActive(false);
    }

    // Called on collision with an enemy to restart the level
    IEnumerator GameOver()
    {
        isDead = true;
        rb.velocity = Vector3.zero;
        sprite.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }
}
