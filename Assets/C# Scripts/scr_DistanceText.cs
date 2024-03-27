using UnityEngine;
using UnityEngine.UI;

public class scr_DistanceText : MonoBehaviour {

    public Text distanceText;

    void Start ()
    {
        int count = scr_DistanceUpdater.getDistance();
        distanceText.text = "Distance " + count.ToString("n0");
	}
}
