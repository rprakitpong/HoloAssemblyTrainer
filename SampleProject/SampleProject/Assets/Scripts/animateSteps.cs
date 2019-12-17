using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animateSteps : MonoBehaviour
{
    //assume all steps has same parent

    List<string> stepsName;
    List<GameObject> steps;
    List<string> description;
    List<string> orientation;

    Text descriptionBox;

    List<Vector3> assembledPosition;
    List<Vector3> unassembledPosition;

    float timeToAnimate = 3f;
    float offset = 50f;
    int currentstep = 0;

    // Start is called before the first frame update
    void Start()
    {
        stepsName = this.GetComponent<ParseObjectDirectionText>().getGONames();
        steps = new List<GameObject>();
        description = this.GetComponent<ParseObjectDirectionText>().getGOTexts();
        orientation = this.GetComponent<ParseObjectDirectionText>().getGODirections();

        descriptionBox = this.transform.Find("StepInfo").Find("Text").GetComponent<Text>();

        initializeGOInsideSteps();
        initializePositions();
        initializeAppState();

        this.transform.Find("StepButton").gameObject.GetComponent<Button>().onClick.AddListener(nextStep);
    }

    private void initializeGOInsideSteps()
    {
        List<string> stepsNameWithoutNull = new List<string>();
        for (int i = 0; i < stepsName.Count; i++)
        {
            GameObject go = GameObject.Find(stepsName[i]);
            if (go != null)
            {
                steps.Add(go);
                stepsNameWithoutNull.Add(stepsName[i]);
            } else
            {
                description.RemoveAt(i);
                orientation.RemoveAt(i);
            }
        }
        stepsName = stepsNameWithoutNull;
    }

    private void initializeAppState()
    {
        ResetViewState();
        ResetDescriptionState();
    }

    void ResetViewState()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            if (orientation[i] != "init")
            {
                steps[i].SetActive(false);
            }
        }
    }

    private void ResetDescriptionState()
    {
        for (int i = 0; i < description.Count; i++)
        {
            if (orientation[i] == "init")
            {
                descriptionBox.text = description[i];
                break;
            }
        }
    }

    private void initializePositions()
    {
        assembledPosition = new List<Vector3>();
        unassembledPosition = new List<Vector3>();

        for (int i = 0; i < steps.Count; i++)
        {
            assembledPosition.Add(steps[i].transform.localPosition);
            Vector3 v = steps[i].transform.localPosition;
            switch (orientation[i])
            {
                case "init":
                    break;
                case "+x": v.x = v.x + offset;
                    break;
                case "-x": v.x = v.x - offset;
                    break;
                case "+y": v.y = v.y + offset;
                    break;
                case "-y": v.y = v.y - offset;
                    break;
                case "+z": v.z = v.z + offset;
                    break;
                case "-z": v.z = v.z - offset;
                    break;
            }
            unassembledPosition.Add(v);
            steps[i].transform.localPosition = unassembledPosition[i];
        }
    }

    private void nextStep()
    {
        currentstep++;

        if (currentstep >= steps.Count)
        {
            StopCoroutine("MoveToPosition");
            currentstep = 0;
            ResetViewState();
            ResetDescriptionState();
        }
        else
        {
            steps[currentstep].SetActive(true);
            descriptionBox.text = description[currentstep];
            if (currentstep != 0)
            {
                StopCoroutine("MoveToPosition");
                steps[currentstep - 1].transform.localPosition = assembledPosition[currentstep - 1];
            }
            StartCoroutine("MoveToPosition");
        }


    }

    IEnumerator MoveToPosition()
    {
        Debug.Log(currentstep);
        float time = timeToAnimate;
        float elapsedTime = 0;
        Vector3 newPosition = assembledPosition[currentstep];
        Vector3 startingPos = unassembledPosition[currentstep];
        while (elapsedTime < time)
        {
            steps[currentstep].transform.localPosition = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
