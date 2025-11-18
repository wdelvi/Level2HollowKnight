using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    [Tooltip("How much damage this should do to destructibles")]
    public int damage = 1;
    [Tooltip("Which faction this Destructor is. Destructors don't damage Destructibles of the same faction.")]
    public int faction = 1;
    [Tooltip("How hard anything this damages should be pushed back")]
    public float knockbackForce = 0f;

    //OnCollisionEnter2D is a built in Unity function that happens at the start of any collision with this game object
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //See if the thing we ran into has a Destructible
        Destructible destrucible =
            collision.gameObject.GetComponent<Destructible>();

        //If it does and it's a different faction than us...
        if( destrucible && destrucible.faction != faction )
        {
            //Make it take damage
            destrucible.TakeDamage(damage);

            //And push it back if it has a rigidbody2d
            Vector3 knockbackVector = collision.transform.position - transform.position;
            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(knockbackVector * knockbackForce, transform.position);
            }
        }
    }
}
