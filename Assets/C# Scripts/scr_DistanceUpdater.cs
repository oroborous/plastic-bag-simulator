using UnityEngine;
using UnityEngine.UI;


public class scr_DistanceUpdater : MonoBehaviour {

    public Transform player;
    public Text scoreText;
    static private int score = 0;
    
    public static int getDistance()
    {
        return score;
    }

    private void Start()
    {
        score = 0;
    }
    // Update is called once per frame 
    void Update () {
        if (player.position.x.CompareTo(score) == 1)
        {
            score = (int)player.transform.position.x;
            scoreText.text = "Distance: " + score.ToString("n0");
        }
	}

}
