using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParseObjectDirectionText : MonoBehaviour
{
    List<string> goName;
    List<string> direction;
    List<string> text;

    // Start is called before the first frame update
    void Start()
    {
        Parse();
    }

    public List<string> getGONames()
    {
        return goName;
    }

    public List<string> getGODirections()
    {
        return direction;
    }

    public List<string> getGOTexts()
    {
        return text;
    }

    private void Parse()
    {
        goName = new List<string>();
        direction = new List<string>();
        text = new List<string>();
        TextAsset RawTextAsset = Resources.Load<TextAsset>("ObjectDirectionTextList");
        string RawText = RawTextAsset.text;
        string[] EachGameObjects = RawText.Split('\n');
        foreach (string gameobject in EachGameObjects)
        {
            string[] GOAttributes = gameobject.Split(',');
            goName.Add(stringTrimmer(GOAttributes[0]));
            direction.Add(stringTrimmer(GOAttributes[1]));
            string text_temp = "";
            for (int i = 2; i < GOAttributes.Length; i++)
            {
                text_temp = text_temp + GOAttributes[i];
            }
            text.Add(text_temp);
        }
    }

    public string stringTrimmer(string str)
    {
        str = new string(str.Where(c => !char.IsControl(c)).ToArray());
        return str;
    }

    public List<string> LOSTrimmer(List<string> los)
    {
        List<string> trimmed_los = new List<string>();
        foreach (string str in los)
        {
            string trimmed_str = stringTrimmer(str);
            trimmed_los.Add(trimmed_str);
        }
        return trimmed_los;
    }
}
