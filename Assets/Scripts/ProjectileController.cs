using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a rigidbody2d to work. This will automatically add one if there isn't one
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    [Tooltip("How fast this projectile will shoot")]
    public float projectileForce = 10f;
    [Tooltip("If true, the projectile will be pushed one time, like a baseball. If false, the projectile will be pushed continuously, like a rocket.")]
    public bool oneTimeForce = true;
    [Tooltip("How long until this projectile disappears")]
    public float timeUntilDestroy = 3f;

    //Reference variables
    private Rigidbody2D myRigidbody2D;
    private Vector3 direction;

    //A boolean just to let us know if we have finished setting up the projectile yet
    private bool doneSetup = false;

    public void Awake()
    {
        //Set reference variables
        myRigidbody2D = GetComponent<Rigidbody2D>();

        //Start the timer until we disappear
        Destroy(gameObject, timeUntilDestroy);
    }

    public void Setup( Vector3 newDirection )
    {
        //Set which direction the projectile should go
        direction = newDirection;

        //Apply force once if we just want a one time force
        if (oneTimeForce == true)
        {
            myRigidbody2D.AddForce(direction * projectileForce);
        }

        //If we have a Sprite Renderer and our x direction is negative, AKA facing left, turn our spirte around
        if(GetComponent<SpriteRenderer>() != null && newDirection.x < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        //We're done with setup
        doneSetup = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If setup is done, and if we want to apply force continuously, apply force every frame
        if ( doneSetup == true && oneTimeForce == false)
        {
            myRigidbody2D.AddForce(direction * projectileForce);
        }
    }
}
