using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class scr_HighScoreController : MonoBehaviour
{
    public GameObject[] initialSelectors;
    public HighScore[] highScores;
    public Text countDownText;
    public float timeUntilContinue;
    public float timeUntilContinueNewScore;

    private bool readyToCountdown;
    private bool enteringNewScore;
    private float countDown;
    private float delay = 1.5f;

    private string gameDataFileName = "data.json";

    void Start()
    {
        LoadHighScores();
        countDown = timeUntilContinue;
        readyToCountdown = true;
        enteringNewScore = false;
    }

    public void SaveAndExit()
    {
        SaveHighScores();
        Invoke("ChangeScene", delay);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private void LoadHighScores()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a scr_GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            // Retrieve the highScores property of loadedData
            highScores = loadedData.highScores;

            int newScore = scr_CalcScore.GetFinalScore();

            if (newScore > 0)
            {

                for (int i = 0; i < highScores.Length; i++)
                {
                    if (highScores[i].highScore < newScore)
                    {
                        HighScore[] newHighScores = new HighScore[highScores.Length];

                        // Copy all higher scores
                        for (int j = 0; j < i; j++)
                        {
                            newHighScores[j] = highScores[j];
                        }

                        // Add the new high score
                        newHighScores[i] = new HighScore(newScore, "AAA");
                        initialSelectors[i].GetComponent<scr_SelectInitial>().SetEditable(true);

                        // Copy the next higher scores one position down
                        for (int j = i; j < highScores.Length - 1; j++)
                        {
                            newHighScores[j + 1] = highScores[j];
                        }

                        highScores = newHighScores;
                        timeUntilContinue = timeUntilContinueNewScore;
                        enteringNewScore = true;

                        break;
                    }
                }
            } 

            for (int i = 0; i < highScores.Length; i++)
            {
                initialSelectors[i].GetComponent<scr_SelectInitial>().SetHighScore(highScores[i]);
            }
        }
        else
        {
            Debug.Log("Could not load " + gameDataFileName);
            SceneManager.LoadScene("MainMenu");
        }
    }


    private void SaveHighScores()
    {
        if (highScores != null)
        {

            for (int i = 0; i < highScores.Length; i++)
            {
                scr_SelectInitial initSelect = initialSelectors[i].GetComponent<scr_SelectInitial>();
                if (initSelect.IsEditable())
                {
                    highScores[i].SetInitials(initSelect.GetInitials());
                    break;
                }
            }

            GameData gameData = new GameData();
            gameData.highScores = this.highScores;
            string dataAsJson = JsonUtility.ToJson(gameData);

            string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
            File.WriteAllText(filePath, dataAsJson);

        }
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && !enteringNewScore)
        {
            // They can cancel out of the high score meanu early if it's in read-only mode
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
        else
        {
            SaveAndExit();
        }
    }

    private void ResetReadyToCountDown()
    {
        readyToCountdown = true;
    }
}