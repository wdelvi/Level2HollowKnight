using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a Jumper to work. This will automatically add one if there isn't one
[RequireComponent(typeof(Jumper))]

public class JumpOverTime: MonoBehaviour
{
    [Tooltip("How many seconds in between each jump")]
    public float timeBetweenJumps = 5f;

    //a timer that counts up until it's time to jump
    private float jumpTimer;

    //the jumper attached to the same game object as this script
    private Jumper jumper;

    // Start is called before the first frame update
    void Start()
    {
        //set the jump timer to zero before we start counting upwards
        jumpTimer = 0f;

        //get our reference to the jumper
        jumper = gameObject.GetComponent<Jumper>();
    }

    // Update is called once per frame
    void Update()
    {
        //increment jump timer by how many seconds since the last update
        jumpTimer += Time.deltaTime;

        //if it's been long enough, jump!
        if( jumpTimer >= timeBetweenJumps )
        {
            //Tell the jumper to jump
            jumper.Jump();

            //reset the jump timer
            jumpTimer = 0f;
        }
    }
}
