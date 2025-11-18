using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Tooltip("The first level that the player unlocks upon starting a new game")]
    public int startingLevelUnlocked = 1;
    [Tooltip("An ordered list of the level select buttons")]
    public List<Button> levelSelectButtons;
    [Tooltip("The name of the save file for this game")]
    public string saveFileName = "highestLevelUnlocked";

    //A local variable to store the highest level we have unlocked
    private int highestLevelUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        //If our player prefs have a save file already...
        if( PlayerPrefs.HasKey(saveFileName) )
        {
            //Load it
            highestLevelUnlocked = PlayerPrefs.GetInt(saveFileName);
        }
        else
        {
            //If not, reset it
            ResetSave();
        }

        //Reset which buttons are on or off using our new data
        ResetView();
    }

    // Update is called once per frame
    public void ResetSave()
    {
        //If the reset save button is pressed, delete all save data
        PlayerPrefs.DeleteAll();

        //Set the highest level unlocked to the starting level
        highestLevelUnlocked = startingLevelUnlocked;

        //Save the new data
        PlayerPrefs.SetInt(saveFileName, startingLevelUnlocked);
        PlayerPrefs.Save();

        //Turn the buttons on or off
        ResetView();
    }

    //Load the selected level
    public void LoadLevel( string sceneName )
    {
        SceneManager.LoadScene(sceneName);
    }

    //Turn on and off buttons
    private void ResetView()
    {
        //Run through all of the buttons
        for (int i = 0; i < levelSelectButtons.Count; i++)
        {
            //Turn on those that we have unlocked
            if (highestLevelUnlocked > i)
            {
                levelSelectButtons[i].interactable = true;
            }
            //And turn those off that we haven't unlocked
            else
            {
                levelSelectButtons[i].interactable = false;
            }
        }
    }

}
