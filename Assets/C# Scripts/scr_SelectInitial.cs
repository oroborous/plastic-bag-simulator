using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_SelectInitial : MonoBehaviour {

    public Text[] Initials;
    public Text score;
    public scr_HighScoreController highScoreController;
    public AudioSource source;
    private KeyCode[] keyCodes;

    private bool editable = false;
    private string alphabet =  "ABCDEFGHIJKLMNOPQRSTUVWXYZ.-1234567890 " ;

    private int[] selectedIndex = { 0, 0, 0 };
    private int currentPosition = 0;

    private bool readyToMove = true;
    private float moveDelay = 0.35f;
    private Color inactiveColor = new Color(.2f, .2f, .2f, 1);
    private Color activeColor = new Color(.7f, .2f, .2f, 1);

	void Start () {
        keyCodes = new KeyCode[]{ KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,  KeyCode.U, KeyCode.V,
         KeyCode.W,  KeyCode.X,  KeyCode.Y,  KeyCode.Z,  KeyCode.Period,  KeyCode.Minus,  KeyCode.Alpha1,  KeyCode.Alpha2,  KeyCode.Alpha3,  KeyCode.Alpha4,  KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Space,
        KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8,KeyCode.Keypad9, KeyCode.Keypad0, KeyCode.KeypadPeriod, KeyCode.KeypadMinus};
    }

    public bool IsEditable()
    {
        return editable;
    }

    public string GetInitials()
    {
        return Initials[0].text + Initials[1].text + Initials[2].text;
    }

    public void SetHighScore(HighScore highScore)
    {
        for (int i = 0; i < Initials.Length; i++)
        {
            Initials[i].text = highScore.initials[i].ToString();
            selectedIndex[i] = alphabet.IndexOf(highScore.initials[i].ToString());
        }

        score.text = highScore.highScore.ToString("n0");
    }

    public void SetEditable(bool editable)
    {
        this.editable = editable;

        //Debug.Log("Setting " + GetInitials() + " editable " + editable);

        if (editable)
        {
            Initials[currentPosition].color = activeColor;
        }
    }
	
	void Update () {
        if (!readyToMove || !editable)
        {
            return;
        }

        foreach (KeyCode kcode in keyCodes)
        {
            if (Input.GetKey(kcode))
            {
                ChangeInitial(kcode.ToString());
                Invoke("ResetReadyToMove", moveDelay);
               // Debug.Log("KeyCode down: " + kcode);
                return;
            }
        }

        if (Input.GetAxis("Vertical") < 0) {
            int indexModifier = 1;
            ChangeInitial(indexModifier);
            Invoke("ResetReadyToMove", moveDelay);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            int indexModifier = -1;
            ChangeInitial(indexModifier);
            Invoke("ResetReadyToMove", moveDelay);
        }
        else if (Input.GetAxis("Horizontal") > 0 || Input.GetButton("Fire1"))
        {
            int positionModifier = 1;
            ChangePosition(positionModifier);
            Invoke("ResetReadyToMove", moveDelay);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            int positionModifier = -1;
            ChangePosition(positionModifier);
            Invoke("ResetReadyToMove", moveDelay);
        }
    }

    private void ChangeInitial(string initial)
    {
        //readyToMove = false;

        if (initial.StartsWith("Alpha"))
        {
            initial = initial.Substring(5);
        } else if (initial.StartsWith("Keypad"))
        {
            initial = initial.Substring(6);
        } else if (initial.Equals("Space"))
        {
            initial = " ";
        }

        if (initial.StartsWith("Period"))
        {
            initial = ".";
        } else if (initial.StartsWith("Minus"))
        {
            initial = "-";
        } 

        Initials[currentPosition].text = initial;
        source.Play();

        selectedIndex[currentPosition] = alphabet.IndexOf(initial);
        ChangePosition(1);
    }

    private void ChangeInitial(int indexModifier)
    {
        readyToMove = false;

        //Debug.Log("ChangeInitial " + indexModifier);

        selectedIndex[currentPosition] += indexModifier;

        if (selectedIndex[currentPosition] < 0)
        {
            selectedIndex[currentPosition] = alphabet.Length - 1;
        }
        else if (selectedIndex[currentPosition] >= alphabet.Length)
        {
            selectedIndex[currentPosition] = 0;
        }
        Initials[currentPosition].text = alphabet[selectedIndex[currentPosition]].ToString();
        source.Play();
    }

    private void ChangePosition(int positionModifier)
    {
        readyToMove = false;

        //Debug.Log("ChangePosition " + positionModifier);

        Initials[currentPosition].color = inactiveColor;

        currentPosition += positionModifier;

        // If they've selected the last initial, save and exit the screen
        if (currentPosition > 2)
        {
            highScoreController.SaveAndExit();
            this.editable = false;
            return;
        }
        else if (currentPosition < 0)
        {
            currentPosition = 0;
        }

        Initials[currentPosition].color = activeColor;

    }

    private void ResetReadyToMove()
    {
        readyToMove = true;
        //Debug.Log("I'm ready to move!");
    }
}
