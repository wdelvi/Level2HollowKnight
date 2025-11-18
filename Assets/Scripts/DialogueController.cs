using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour 
{
    [Tooltip("How far away talking is allowed to happen")]
    public float minTalkDistance;
    [Tooltip("The object to measure distance against. Usually the player")]
    public Transform target;
    [Tooltip("The actual text for the conversation that happens")]
    public List<string> talkingStrings;
    [Tooltip("The UI element to show when talking")]
    public GameObject dialogueScreen;
    [Tooltip("The text to change into the talking strings")]
    public TMP_Text textToModify;
    [Tooltip("A sound to play on talk (requires audio source)")]
    public AudioClip talkSound;

    //A private int to know how far through the conversation we are
    private int talkTextIndex;

    //A bool for whether or not we're currently talking
    private bool showingDialgoue;

    //The audiosource component to play sound from
    private AudioSource myAudioSoure;

    //A gizmo to show how far we can be away and still talk
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minTalkDistance);
    }

	// Use this for initialization
	void Start () 
    {
        //Set our current index to 0
        talkTextIndex = 0;
        //Set whether or not we're showing dialogue to false
        showingDialgoue = false;

        //Get our audiosource
        myAudioSoure = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //If we have a target (the player)
        if( target )
        {
            //If we are close enough...
            if( IsWithinDistance( minTalkDistance ) )
            {
                //Listen for input
                DetectInput();
            }
            //If not...
            else if( showingDialgoue )
            {
                //Be sure to hide the dialogue
                EndDialogue();
            }
        }
	}

    //Listen for input
    private void DetectInput()
    {
        //If the E key is pressed...
        if( Input.GetKeyUp( KeyCode.E ) )
        {
            //If we aren't showing dialogue yet...
            if( showingDialgoue == false )
            {
                //Start dialogue
                BeginDialogue();
            }
            //If we still have text to show...
            else if( talkTextIndex < talkingStrings.Count - 1 )
            {
                //Show the next piece
                ProgressDialogue();
            }
            //Otherwise end dialogue
            else
            {
                EndDialogue();
            }
        }
    }

    //When dialogue starts...
    private void BeginDialogue()
    {
        //Set showign dialogue to true
        showingDialgoue = true;

        //Set the index to 0
        talkTextIndex = 0;
        //Change the text to the right piece of dialogue
        textToModify.text = talkingStrings[talkTextIndex];

        //Show the screen
        dialogueScreen.SetActive(true);

        //If we have an audio source and a sound, play it
        if(myAudioSoure && talkSound)
        {
            myAudioSoure.PlayOneShot(talkSound);
        }
    }

    private void ProgressDialogue()
    {
        //When progressing dialogue, increment the index...
        talkTextIndex++;
        //Ans change the text to the right piece of dialogue
        textToModify.text = talkingStrings[talkTextIndex];

        //If we have an audio source and a sound, play it
        if (myAudioSoure && talkSound)
        {
            myAudioSoure.PlayOneShot(talkSound);
        }
    }

    private void EndDialogue()
    {
        //When ending dialogue, turn off the dialogue screen
        showingDialgoue = false;
        dialogueScreen.SetActive(false);
    }

    public virtual bool IsWithinDistance(float distance)
    {
        //Check if we're close enough to the target
        return (GetDirection().magnitude < distance);
    }

    public virtual Vector3 GetDirection()
    {
        //Get the direction of the target
        return target.position - transform.position;
    }
}
