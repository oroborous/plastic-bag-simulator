using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GoToLevel : MonoBehaviour {

    public string Name;

    //Changes level
    public void GoToLevel()
    {
        SceneManager.LoadScene(Name);
    }
}
