using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_KioskMode : MonoBehaviour {

    public GameObject button;

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown("k"))
        {
            button.SetActive(!button.activeSelf);
        }
	}

    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
}
