using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a Jumper to work. This will automatically add one if there isn't one
[RequireComponent(typeof(Jumper))]
public class ProximityShootEnemyController: MonoBehaviour
{
    [Tooltip("How far ahead the enemy looks to potentially attack")]
    public float looksAheadDistance = 2f;
    [Tooltip("How many seconds in between each shot")]
    public float timeBetweenShots = 1f;
    [Tooltip("Which direction this enemy should always shoot")]
    public Vector3 shootDiretion = new Vector3(1f,0f,0f);
    [Tooltip("The distance at which this enemy can detect the player")]
    public float detectionDistance = 1f;

    //A timer that increements up until it's time to shoot
    private float shootTimer;
    //A reference variable that stores the projectile shooter
    private ProjectileShooter projectileShooter;

    // Start is called before the first frame update
    void Start()
    {
        //Start the shoot timer at 0 so we can increment
        shootTimer = 0f;
        //Get a reference to the projectile shooter
        projectileShooter = gameObject.GetComponent<ProjectileShooter>();

        //If we have a shooter
        if(projectileShooter != null)
        {
            //Set it's direction correctly
            projectileShooter.SetDirection(shootDiretion);
        }
    }

    //Draw gizmos is 100% a debug function. Players WILL NOT see it. It is only to help us design
    public void OnDrawGizmos()
    {
        //Draw a ray from our position towards our raycast direction
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shootDiretion * detectionDistance);
    }

    // Update is called once per frame
    void Update()
    {
        //Increment the shoot timer by how many seconds have passed since the last frame
        shootTimer += Time.deltaTime;

        //If it's been long enough and we have a shooter...
        if(shootTimer >= timeBetweenShots && projectileShooter != null)
        {
            //Before shooting we check if the player is in range

            //Cast a ray and see if it hits something. Return an array of all hits
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, shootDiretion, detectionDistance);

            //Check entire array of hits
            foreach(RaycastHit2D hit in hits)
            {
                //If any of the hits have a player controller, AKA are the player...
                if (hit.rigidbody != null && hit.rigidbody.GetComponent<PlayerController>())
                {

                    projectileShooter.SetDirection(shootDiretion);
                    //Shoot!
                    projectileShooter.Fire();

                    //And restart our timer
                    shootTimer = 0f;
                }
            }
        }
    }
}
