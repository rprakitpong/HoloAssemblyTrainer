using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animateSteps : MonoBehaviour
{
    //assume all steps has same parent

    List<GameObject> steps;
    List<string> description;
    List<string> orientation;

    GameObject descriptionBox;

    List<Vector3> assembledPosition;
    List<Vector3> unassembledPosition;

    float timeToAnimate = 3f;
    float offset = 1f;
    int currentstep = 0;

    // Start is called before the first frame update
    void Start()
    {
        steps = this.GetComponent<getObjectsAndDescriptions>().GetGameObjects();
        description = this.GetComponent<getObjectsAndDescriptions>().GetDescriptions();
        orientation = this.GetComponent<getObjectsAndDescriptions>().GetOrientations();

        descriptionBox = GameObject.Find("descriptionBox");

        initializePositions();
        initializeRenderer();

        nextStep();
        GameObject.Find("nextButton").GetComponent<Button>().onClick.AddListener(nextStep);

        Debug.Log("initialized animation");
    }

    private void initializeRenderer()
    {
        foreach (GameObject GO in steps)
        {
            GO.GetComponent<Renderer>().enabled = false;
        }
    }

    private void initializePositions()
    {
        assembledPosition = new List<Vector3>();
        unassembledPosition = new List<Vector3>();

        for (int i = 0; i < steps.Count; i++)
        {
            assembledPosition.Add(steps[i].transform.localPosition);
            
            switch (orientation[i])
            {
                case "init":
                    unassembledPosition.Add(steps[i].transform.localPosition);
                    break;
                case "+x":
                    {
                        Vector3 v = steps[i].transform.localPosition;
                        v.x = v.x + offset * steps[i].GetComponent<Collider>().bounds.size.x;
                        unassembledPosition.Add(v);
                    }
                    break;
                case "-x":
                    {
                        Vector3 v = steps[i].transform.localPosition;
                        v.x = v.x - offset * steps[i].GetComponent<Collider>().bounds.size.x;
                        unassembledPosition.Add(v);
                    }
                    break;
                case "+y":
                    {
                        Vector3 v = steps[i].transform.localPosition;
                        v.y = v.y + offset * steps[i].GetComponent<Collider>().bounds.size.y;
                        unassembledPosition.Add(v);
                    }
                    break;
                case "-y":
                    {
                        Vector3 v = steps[i].transform.localPosition;
                        v.y = v.y - offset * steps[i].GetComponent<Collider>().bounds.size.y;
                        unassembledPosition.Add(v);
                    }
                    break;
                case "+z":
                    {
                        Vector3 v = steps[i].transform.localPosition;
                        v.z = v.z + offset * steps[i].GetComponent<Collider>().bounds.size.z;
                        unassembledPosition.Add(v);
                    }
                    break;
                case "-z":
                    {
                        Vector3 v = steps[i].transform.localPosition;
                        v.z = v.z - offset * steps[i].GetComponent<Collider>().bounds.size.z;
                        unassembledPosition.Add(v);
                    }
                    break;
            }

            steps[i].transform.localPosition = unassembledPosition[i];
        }
    }

    private void nextStep()
    {
        steps[currentstep].GetComponent<Renderer>().enabled = true;
        descriptionBox.GetComponent<Text>().text = description[currentstep];
        StartCoroutine("MoveToPosition");

        if (currentstep + 1 >= steps.Count)
        {
            currentstep = 0;
        } else
        {
            currentstep++;
        }
    }

    IEnumerator MoveToPosition()
    {
        float time = timeToAnimate;
        float elapsedTime = 0;
        Vector3 newPosition = assembledPosition[currentstep];
        Vector3 startingPos = unassembledPosition[currentstep];
        while (elapsedTime < time)
        {
            transform.localPosition = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
