using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_RoomGenerator : MonoBehaviour {
    // https://www.raywenderlich.com/5459-how-to-make-a-game-like-jetpack-joyride-in-unity-2d-part-2

    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthInPoints;
    public int[] genQueue = new int[5];

	// Use this for initialization
	void Start () {
        // Just a %!@#$ guess
        screenWidthInPoints = 68.0f;

        StartCoroutine(GeneratorCheck());

    }

    // Update is called once per frame
    void Update () {
		
	}

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }


    void AddRoom(float farthestRoomEndX)
    {
        //1
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        randomRoomIndex = checkList(randomRoomIndex);
        //2
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
        room.SetActive(true);
        //3
        Transform sky = room.transform.Find("Sky Drop");
        float roomWidth = sky.GetComponent<BoxCollider2D>().size.x * sky.localScale.x;
         //4
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;
        //5
        room.transform.position = new Vector3(roomCenter, 0, 0);
        //6
        currentRooms.Add(room);
    }


    private void GenerateRoomIfRequired()
    {
        //1
        List<GameObject> roomsToRemove = new List<GameObject>();
        //2
        bool addRooms = true;
        //3
        float playerX = transform.position.x;
        //4
        float removeRoomX = playerX - screenWidthInPoints;
        //5
        float addRoomX = playerX + screenWidthInPoints;
        //6
        float farthestRoomEndX = 0;
        foreach (var room in currentRooms)
        {
            //7
            Transform sky = room.transform.Find("Sky Drop");
            float roomWidth = sky.GetComponent<BoxCollider2D>().size.x * sky.localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;
            //8
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            //9
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            //10
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        //11
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        //12
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }

    private int checkList(int rand)
    {
        int returnValue = rand;
        bool isIn = false;

        // check to see if it is in the queue already
        for (int i = 0; i < genQueue.Length; i++)
        {
            if(rand == genQueue[i])
            {
                isIn = true;
            }
        }

        // if it is, find a new one
        if (isIn == true)
        {
            int randomRoomIndex = Random.Range(0, availableRooms.Length);
            returnValue = checkList(randomRoomIndex);
        }

        // bump the list
        for (int i = genQueue.Length-1; i > 0; i--)
        {
            genQueue[i] = genQueue[i - 1];
        }
        genQueue[0] = returnValue;

        // return the new room variable
        return returnValue;
    }

}
