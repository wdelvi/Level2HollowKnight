using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a Jumper to work. This will automatically add one if there isn't one
[RequireComponent(typeof(Jumper))]
public class ShootOverTime : MonoBehaviour
{
    [Tooltip("How many seconds in between each shot")]
    public float timeBetweenShots = 2f;
    [Tooltip("Which direction this enemy should always shoot")]
    public Vector3 shootDiretion;

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

    // Update is called once per frame
    void Update()
    {
        //Increment the shoot timer by how many seconds have passed since the last frame
        shootTimer += Time.deltaTime;

        //If it's been long enough and we have a shooter...
        if(shootTimer >= timeBetweenShots && projectileShooter != null)
        {
            //Shoot!
            projectileShooter.Fire();

            //And restart our timer
            shootTimer = 0f;
        }
    }
}
