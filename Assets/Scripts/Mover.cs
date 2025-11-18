using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Tooltip("Number indicating how quickly the character will speed up")]
    public float acceleration = 10f;

    [Tooltip("Number indicating maximum movement speed")]
    public float maximumSpeed = 5f;

    //Just a reference variable so we can easily access our attached Rigidbody2D
    private Rigidbody2D myRigidbody;

    public void Start()
    {
        //Set the reference variable to attached Rigidbody2D
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void AccelerateInDirection( Vector2 direction )
    {
        //Take existing velocity and apply additional speed in indicated direction
        Vector2 newVelocity = myRigidbody.linearVelocity + (direction * acceleration * Time.deltaTime);

        //Clamp the x velocty by max (btw this means y speed could still be fast AF)
        newVelocity.x = Mathf.Clamp(newVelocity.x, -maximumSpeed, maximumSpeed);

        //Set my velocity to the new calculated velocity
        myRigidbody.linearVelocity = newVelocity;
    }
}
