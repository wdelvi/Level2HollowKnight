using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a mover to work. This will automatically add one if there isn't one
[RequireComponent(typeof(Mover))]

public class MoveTowardsPlayerHorizontal: MonoBehaviour
{
    [Tooltip("How far ahead the enemy looks to potentially attack")]
    public float looksAheadDistance = 2f;

    //Reference Variables
    private SpriteRenderer spriteRenderer;
    private Mover controlledMover;

    // Start is called before the first frame update
    void Start()
    {
        //Set reference vars
        spriteRenderer = GetComponent<SpriteRenderer>();
        controlledMover = GetComponent<Mover>();
    }

    //Draw gizmos is 100% a debug function. Players WILL NOT see it. It is only to help us design
    public void OnDrawGizmos()
    {
        //Calulcate the left endpoint
        Vector3 leftEndpoint = transform.position;
        leftEndpoint.x -= looksAheadDistance;

        //Calulcate the difference from left to where it should end on the right
        Vector3 rightEndpoint = new Vector3(looksAheadDistance * 2f, 0f, 0f);

        //Actually draw a ray from left to right
        Gizmos.color = Color.red;
        Gizmos.DrawRay(leftEndpoint, rightEndpoint);
    }

    // Update is called once per frame
    void Update()
    {
        //Cast a ray towards the LEFT and see if it hits something. Return an array of all hits
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, new Vector2(-1f,0f), looksAheadDistance);

        //Check entire array of hits
        foreach(RaycastHit2D hit in hitsLeft)
        {
            //If any of the hits have a player controller, AKA are the player...
            if (hit.rigidbody != null && hit.rigidbody.GetComponent<PlayerController>())
            {
                controlledMover.AccelerateInDirection(new Vector3(-1f, 0f, 0f));

                //If we have a sprite renderer, turn that around too
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }

        //Cast a ray towards the RIGHT and see if it hits something. Return an array of all hits
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, new Vector2(1f, 0f), looksAheadDistance);

        //Check entire array of hits
        foreach (RaycastHit2D hit in hitsRight)
        {
            //If any of the hits have a player controller, AKA are the player...
            if (hit.rigidbody != null && hit.rigidbody.GetComponent<PlayerController>())
            {
                controlledMover.AccelerateInDirection(new Vector3(1f, 0f, 0f));

                //If we have a sprite renderer, turn that around too
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
    }
}
