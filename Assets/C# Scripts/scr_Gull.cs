using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Gull : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotateSpeed = 5.0f;

    public Rigidbody2D garbageTruck;

    Vector3 newPosition;
    float startY;

    void Start()
    {
        startY = transform.position.y;
        PositionChange();
    }

    void PositionChange()
    {
        float truckX = garbageTruck.position.x;
        // Stay above truck and hover within +/-3 of starting altitude
        newPosition = new Vector2(Random.Range(truckX + 3.0f, truckX +7.0f), Random.Range(startY - 3.0f, startY + 3.0f));
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, newPosition) < 1)
        {
            PositionChange();
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);

        //LookAt2D(newPosition);
    }

    void LookAt2D(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - transform.position.x;
        float distanceY = lookAtPosition.y - transform.position.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;

        Quaternion endRotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * rotateSpeed);
    }

}
