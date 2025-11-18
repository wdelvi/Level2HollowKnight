using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [Tooltip("The follower of the main camera")]
    public Follower mainCameraFollower;
    [Tooltip("The Rigidbody of the player to stop movement upon win")]
    public Rigidbody2D playerRigidbody;
    [Tooltip("The win screen to turn on when the game is won")]
    public GameObject winScreen;
    [Tooltip("Which level is unlocked upon touching this goal")]
    public int levelUnlocked;
    [Tooltip("The name of the save file for this game")]
    public string saveFileName = "highestLevelUnlocked";

    //When we touch this object...
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If we have a win screen...
        if (winScreen != null)
        {
            //Show it
            winScreen.SetActive(true);
        }
        else
        {
            //If not, just print a message
            Debug.Log("Won Game");
        }

        //If we have a follower...
        if (mainCameraFollower != null)
        {
            //Stop it from following
            mainCameraFollower.allowFollowing = false;
        }

        //If we have a player rigidbody...
        if (playerRigidbody != null)
        {
            //Stop it from moving
            playerRigidbody.simulated = false;
        }

        //If we have a save game...
        if (PlayerPrefs.HasKey(saveFileName))
        {
            //Load the currently unlocked level
            int currentLevel = PlayerPrefs.GetInt(saveFileName);

            //If our level is higher...
            if(levelUnlocked > currentLevel)
            {
                //Set that as the new highest level unlocked
                PlayerPrefs.SetInt(saveFileName, levelUnlocked);
                PlayerPrefs.Save();
            }
        }
    }
}
