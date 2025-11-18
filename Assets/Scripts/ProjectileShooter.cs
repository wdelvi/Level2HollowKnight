using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Tooltip("A prefab with a projectile controller. Which projectile we shoot.")]
    public GameObject projectilePrefab;

    [Tooltip("A child object placed where the projectile will spawn from")]
    public Transform spawnPoint;

    //Which direction the projectile should go
    private Vector3 direction;

    //A setter function to tell the shooter which direction to shoot
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void Fire()
    {
        //Instantiate (or spawn) a new projectile based on our prefab
        GameObject newProjectile = Instantiate(projectilePrefab) as GameObject;

        //Set the position of the spawned projectile to the spawn point
        newProjectile.transform.position = spawnPoint.position;

        //Check the spawned projectile for a projectile controller
        ProjectileController newProjectileController = newProjectile.GetComponent<ProjectileController>();

        //If we have one...
        if (newProjectileController != null)
        {
            //Set it up
            newProjectileController.Setup(direction);
        }
        else
        {
            //If we don't, let the Game Dev know they're missing one
            Debug.LogWarning("Projectile is missing a projectile controller!");
        }
    }
}
