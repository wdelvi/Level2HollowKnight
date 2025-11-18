using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Tooltip("Whether or not we should actually follow")]
    public bool allowFollowing = true;

    [Tooltip("The thing we should follow")]
    public Transform target;

    [Tooltip("How much we should dampen or ease our movement when following")]
    public float damping = 0.5f;

    [Tooltip("Don't edit! This just shows our current velocity")]
    public Vector3 velocity;

    [Tooltip("Don't edit! This just shows our current offset from the followed object")]
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //When we start, we store however far we are from the followed object and attempt to keep that offset
        offset = transform.position - target.position;
    }

    // FixedUpdate is called 25 times per second. This is used for physics. We're moving the camera here because the
    // player moves every FixedUpdate
    private void FixedUpdate()
    {
        //If we don't have a target (EX. if the player is dead) return, doing nothing
        if( target == null || allowFollowing == false )
        {
            return;
        }

        //Set a target position based upon the target and adding the offset
        Vector3 targetPosition = target.position + offset;

        //Use the SmoothDamp function to ease towards that position, instead of snapping to it
        transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref velocity, damping, Mathf.Infinity, Time.fixedDeltaTime);
    }
}
