using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScore {

    public int highScore;
    public string initials;

    public HighScore(int highScore, string initials)
    {
        this.highScore = highScore;
        this.initials = initials;
    }

    public void SetInitials(string initials)
    {
        this.initials = initials;
    }
}
