using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    [Tooltip("A list of the possible sounds that can be played")]
    public List<AudioClip> possibleSounds;
    [Tooltip("The minimum time between each sound effect")]
    public float minTimeBetweenSounds;
    [Tooltip("The maximum time between each sound effect")]
    public float maxTimeBetweenSounds;
    [Tooltip("The maximum distance the target can be away to play sounds. If zero sound will auto play")]
    public float maxDistanceFromTarget;
    [Tooltip("The target to check distance from. No target will auto play sounds")]
    public Transform target;

    //The timer to count up until playing the sound
    private float soundTimer;
    //A variable to store how long until the next sound
    private float timeUntilNextSound;
    //A reference variable to store my audio source
    private AudioSource myAudioSource;

    //A gizmo to show how far we can be away and still talk
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistanceFromTarget);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Reset the timer and randomly choose how long until audio plays
        soundTimer = 0f;
        timeUntilNextSound = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
        //Grab my audio source
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we don't have an audio source or any sounds, just exit the function
        if (myAudioSource == null || possibleSounds.Count <= 0)
        {
            return;
        }

        //If we don't have a target OR the target is close enough...
        if (maxDistanceFromTarget <= 0 ||(target && IsWithinDistance(maxDistanceFromTarget)))
        {
            //increment the timer
            soundTimer += Time.deltaTime;
        }

        //if enough time has passed
        if(soundTimer >= timeUntilNextSound)
        {
            //Pick a random sound from the list
            AudioClip soundToPlay = possibleSounds[Random.Range(0, possibleSounds.Count)];

            //Play the sound
            myAudioSource.PlayOneShot(soundToPlay);

            //And reset your sound timer
            soundTimer = 0f;
            timeUntilNextSound = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
        }
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
