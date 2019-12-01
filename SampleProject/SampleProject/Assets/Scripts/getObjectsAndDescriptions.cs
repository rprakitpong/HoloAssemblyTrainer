using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getObjectsAndDescriptions : MonoBehaviour
{
    List<GameObject> gameObjects;
    List<string> descriptions;
    List<string> orientations; //where to come from
    //choice of "init", "+x", "-x", "+y", "-y", "+z", "-z"

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        gameObjects.Add(GameObject.Find("assemBase-base"));
        gameObjects.Add(GameObject.Find("baseOfAssem"));
        gameObjects.Add(GameObject.Find("wormSpacer"));
        gameObjects.Add(GameObject.Find("gearSpacer"));
        gameObjects.Add(GameObject.Find("worm"));
        gameObjects.Add(GameObject.Find("gear"));

        descriptions = new List<string>();
        descriptions.Add("This demo is a worm gear assembly. This base serves as a workbench for all assembly parts.");
        descriptions.Add("Please place gearing mount here.");
        descriptions.Add("This spacer is used to position worm on its shaft.");
        descriptions.Add("This spacer is used to position gear on its shaft.");
        descriptions.Add("Insert worm into its shaft.");
        descriptions.Add("Insert gear into its shaft.");

        orientations = new List<string>();
        orientations.Add("init");
        orientations.Add("+y");
        orientations.Add("+y");
        orientations.Add("-x");
        orientations.Add("+y");
        orientations.Add("-x");
    }

    public List<GameObject> GetGameObjects()
    {
        return gameObjects;
    }

    public List<string> GetDescriptions()
    {
        return descriptions;
    }
    
    public List<string> GetOrientations()
    {
        return orientations;
    }
}
