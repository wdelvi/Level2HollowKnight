using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    DoubleJump,
    Dash,
    NewWeapon
}

public class Powerup : MonoBehaviour
{
    [Tooltip("The type of powerup this is for the player")]
    public PowerupType collectibleType;

    [Tooltip("Optional, only used if this is a New Weapon")]
    public GameObject projectilePrefab;

    [Tooltip("The sound to play on pickup, if any")]
    public AudioClip pickupSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //check if what touched us was actually the player
        PlayerController player =
            collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            //check which powerup this is, act accordibly
            if (collectibleType == PowerupType.DoubleJump)
            {
                player.UnlockDoubleJump();
            }
            else if (collectibleType == PowerupType.Dash)
            {
                player.UnlockDash();
            }
            else if (collectibleType == PowerupType.NewWeapon)
            {
                player.UnlockWeapon(projectilePrefab);
            }

            //If we have an audio source and a sound...
            if (GetComponent<AudioSource>() && pickupSound)
            {
                //Play the sound!
                GetComponent<AudioSource>().PlayOneShot(pickupSound);
            }

            //Turn off our sprite renderer
            if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }

            //Destroy ourself after 5 seconds (so there is time for the sound to play)
            Invoke("Die", 5f);
        }
    }

    public void Die()
    {
        //On die we just destroy (AKA get deleted)
        Destroy(gameObject);
    }
}
