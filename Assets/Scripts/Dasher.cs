using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : MonoBehaviour
{
    [Tooltip("Whether or not we're allowed to dash at all")]
    public bool dashAllowed = false;


    [Tooltip("Number indicating how much force we dash with")]
    public float dashImpulse = 5f;

    [Tooltip("How many seconds between each dash")]
    public float dashCooldown = 2f;

    [Tooltip("What sound to play when we dash if we have an Audio Source")]
    public AudioClip dashSound;

    //Reference variable to attached Rigidbody2D
    private Rigidbody2D myRigidbody;

    //A timer to count Dash Cooldowns
    private float dashTimer;

    //the direction we're facing
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        //Store attached Rigidbody2D
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();

        //make sure we can immediately dash if we want to
        dashTimer = dashCooldown;

        //set an initial direction towards the right
        direction = new Vector3(1f, 0f, 0f);
    }

    public void Update()
    {
        dashTimer += Time.deltaTime;
    }

    //Update which direction we're facing
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    //Function that gets called whenever we need to jump
    public void Dash()
    {
        //If we are allowed to dash AND we have waited long enough since the last dash...
        if (dashAllowed && dashTimer >= dashCooldown)
        {
            //Reset our dash timer so we have to wait to dash again
            dashTimer = 0f;

            //Store my current x velocity so we can dash additively, aka add to our current speed
            float currentXVelocity = myRigidbody.linearVelocity.x;

            //add the dash impulse in the correct direction to our x velocity, don't change y velocity
            myRigidbody.linearVelocity =
                new Vector2(currentXVelocity + direction.x * dashImpulse, myRigidbody.linearVelocity.y);

            //If we have an audio source and a jump sound, play it
            if( GetComponent<AudioSource>() != null && dashSound != null)
            {
                GetComponent<AudioSource>().PlayOneShot(dashSound);
            }
        }
    }
}
