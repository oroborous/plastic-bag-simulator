using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TrashMan : MonoBehaviour {

    public float moveSpeed;

    public Rigidbody2D myRigidBody;

	void Update () {
        if (moveSpeed < 4.7)
        {
            moveSpeed = moveSpeed + (scr_DistanceUpdater.getDistance() * .0001f);
        }
        
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
    }


}
