using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_CalcScore : MonoBehaviour {

    public Text scoreText;
    public Text countDownText;

    public float timeUntilContinue;

    private static int finalScore;
    private static bool hasBeenHighScore;
    private bool readyToCountdown;
    private float countDown;
    private float delay = 1.5f;

    void Start()
    {
        finalScore = (int)(scr_DistanceUpdater.getDistance() * (1f + .05f * scr_PlayerMovement.getReceipts()));
        scoreText.text = finalScore.ToString("n0");

        countDown = timeUntilContinue;
        hasBeenHighScore = false;
        readyToCountdown = true;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            countDown = 0;
        }
        else if (!readyToCountdown)
        {
            return;
        }

        if (countDown > 0)
        {
            readyToCountdown = false;
            countDownText.text = "Continuing in " + countDown.ToString("n0") + (countDown == 1 ? " second..." : " seconds...");
            countDown--;
            Invoke("ResetReadyToCountDown", delay);
        }
        else if (countDown <= 0)
        {
            SceneManager.LoadScene("HighScores");
        }
    }

    private void ResetReadyToCountDown()
    {
        readyToCountdown = true;
    }



    public static int GetFinalScore()
    {
        if (hasBeenHighScore)
        {
            return 0;
        }
        else
        {
            hasBeenHighScore = true;
            return finalScore;
        }
    }

}
