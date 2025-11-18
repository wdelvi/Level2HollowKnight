using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [Tooltip("What text to actually print to the console")]
    public string textToPrint = "Hello World!";

    // Start is called before the first frame update
    void Start()
    {
        //Print the contents of the textToPrint variable to the console one time at game start
        Debug.Log(textToPrint);
    }

    // Update is called once per frame (60 times a second or so!)
    void Update()
    {
        //"Call" or activate our custom function
        PrintCustomText();
    }

    private void PrintCustomText()
    {
        //Append some extra text to the end of the textToPrint variable
        string customTextToPrint = textToPrint + "!!!";

        //Print the contents of the new customTextToPrint variable to the console
        Debug.Log(customTextToPrint);
    }
}
