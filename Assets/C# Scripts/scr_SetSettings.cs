using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scr_SetSettings : MonoBehaviour {

    public Text Pdisplay;
    public Text Altdisplay;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ptoasty") == 0)
        {
            Pdisplay.text = ": Off";
        }
        else if (PlayerPrefs.GetInt("Ptoasty") == 1)
        {
            Pdisplay.text = ": On";
        }

        if (PlayerPrefs.GetInt("Alternate") == 0)
        {
            Altdisplay.text = ": Off";
        }
        else if (PlayerPrefs.GetInt("Alternate") == 1)
        {
            Altdisplay.text = ": On";
        }
    }

    public void SetPtoasty()
    {
        if (PlayerPrefs.GetInt("Ptoasty") == 0)
        {
            PlayerPrefs.SetInt("Ptoasty", 1);
            Pdisplay.text = ": On";
        }
        else if (PlayerPrefs.GetInt("Ptoasty") == 1)
        {
            PlayerPrefs.SetInt("Ptoasty", 0);
            Pdisplay.text = ": Off";
        }
    }

    public void SetAltMusic()
    {
        if (PlayerPrefs.GetInt("Alternate") == 0)
        {
            PlayerPrefs.SetInt("Alternate", 1);
            Altdisplay.text = ": On";
        }
        else if (PlayerPrefs.GetInt("Alternate") == 1)
        {
            PlayerPrefs.SetInt("Alternate", 0);
            Altdisplay.text = ": Off";
        }
    }
}
