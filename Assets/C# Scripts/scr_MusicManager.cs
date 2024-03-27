using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageMusic : MonoBehaviour {

    private static bool isDefault;

    public void setMusic(bool isToggled)
    {
        isDefault = isToggled;
        getMusic();
    }

    public static bool getMusic()
    {
        return isDefault;
    }
}
