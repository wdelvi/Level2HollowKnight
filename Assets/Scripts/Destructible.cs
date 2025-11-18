using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Tooltip("How much health this Destructible should have in total")]
    public int maximumHitPoints = 3;
    [Tooltip("Which faction this Destructible is. Destructors don't damage Destructibles of the same faction.")]
    public int faction = 1;
    [Tooltip("Which sound to play when this Destructible is damaged")]
    public AudioClip soundOnHit;

    //Variable to store our current hitpoints
    private int hitPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Set our hitpoints to our default when the game starts
        hitPoints = maximumHitPoints;
    }

    //Function to inflict damage on this Destructible
    public void TakeDamage( int damageAmount )
    {
        //Modify hitpoints by the damage amount
        ModifyHitPoints(-damageAmount);

        //If we have a sound and an audio source, play sound
        if(soundOnHit != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(soundOnHit);
        }
    }

    //Function to heal damage on this Destructible
    public void Heal( int healAmount )
    {
        ModifyHitPoints(healAmount);
    }

    //Base function that modifies hitpoints by any value (positive or negative)
    private void ModifyHitPoints( int modAmount )
    {
        //Add the modAmount (which could be positive or negative) to the current hitpoints
        hitPoints += modAmount;

        //Make sure we don't go past our maximum hitpoints
        hitPoints = Mathf.Min(maximumHitPoints, hitPoints);

        //If we are at or below 0, die
        if( hitPoints <= 0 )
        {
            Die();
        }
    }

    //Function called on death
    private void Die()
    {
        //Destroy this game object
        Destroy(gameObject);
    }

    //A Getter function to tell other scripts what our current health is
    public int GetHitPoints()
    {
        return hitPoints;
    }
}
