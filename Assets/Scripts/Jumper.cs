using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [Tooltip("Number indicating how much force we jump with")]
    public float jumpImpulse = 5f;

    [Tooltip("Number indicating how much higher we'll jump with the jump bonus. 1.0 = regular")]
    public float jumpBonusModifier = 1f;

    [Tooltip("Whether or not the player currently has Double Jump unlocked")]
    public bool doubleJumpAllowed = false;

    [Tooltip("What sound to play when we jump if we have an Audio Source")]
    public AudioClip jumpSound;

    //A boolean that detects whether or not we are touching something. Allowing us to jump again.
    private bool isOnGround;
    private bool hasDoubleJumped;

    //Reference variable to attached Rigidbody2D
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //Store attached Rigidbody2D
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    //Function that gets called whenever we need to jump
    public void Jump()
    {
        //If we are touching something... OR we are allowed to double jump AND have yet to do so
        if (isOnGround == true || (doubleJumpAllowed == true && hasDoubleJumped == false))
        {
            //If we managed to jump while in the air, make sure to mark that we've now double jumped
            if(isOnGround == false)
            {
                hasDoubleJumped = true;
            }

            //Change my velocity upwards by the jump impulse times the bonus multiplier
            myRigidbody.linearVelocity =
                new Vector2(myRigidbody.linearVelocity.x, jumpImpulse * jumpBonusModifier);

            //If we have an audio source and a jump sound, play it
            if( GetComponent<AudioSource>() != null && jumpSound != null)
            {
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
        }
    }

    //Whenever I run into something I know I can jump again
    public void OnCollisionStay2D(Collision2D collision)
    {
        isOnGround = true;
        hasDoubleJumped = false;
    }

    //Whenever I stop runnung into something I know I'm in space and cannot jump again
    public void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }

    public bool GetIsOnGround()
    {
        return isOnGround;
    }

    //Function to begin the jump bonus
    public void StartJumpBonus( float newJumpModifier, float bonusLength )
    {
        jumpBonusModifier = newJumpModifier;
        CancelInvoke();
        Invoke("EndJumpBonus", bonusLength);
    }

    //Function to end the jump bonus
    public void EndJumpBonus()
    {
        jumpBonusModifier = 1f;
    }
}
