using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorExample : MonoBehaviour
{
    public ArrayList colors = new ArrayList();
    public Text[] colorexample;

    private static Dictionary<string, Color> _colors = new Dictionary<string, Color>()
    {
        ["red"] = new Color(0.898039216f, 0, 0),
        ["orange"] = new Color(0.976470588f, 0.450980392f, 0.0235294118f),
        ["yellow"] = new Color(1f, 1f, 0.0784313725f),
        ["brown"] = new Color(0.396078431f, 0.215686275f, 0),
        ["pink"] = new Color(1f, 0.505882353f, 0.752941176f),
        ["blue"] = new Color(0.0117647059f, 0.262745098f, 0.874509804f),
        ["green"] = new Color(0.0823529412f, 0.690196078f, 0.101960784f),
        ["purple"] = new Color(0.494117647f, 0.117647059f, 0.611764706f),
        ["grey"] = new Color(0.57254902f, 0.584313725f, 0.568627451f),
        ["black"] = new Color(0, 0, 0),
    };
    public static Color GetColor(string color)
    {
        color = color.Trim().ToLower();
        if (_colors.ContainsKey(color))
        {
            return (_colors[color]);
        }
        return (Color.black);
    }

    // Start is called before the first frame update
    void Start()
    {
        colors.Add("Red");
        colors.Add("Blue");
        colors.Add("Green");
        colors.Add("Yellow");
        colors.Add("Orange");
        colors.Add("Black");
        colors.Add("Brown");
        colors.Add("Pink");
        colors.Add("Purple");
        colors.Add("Grey");
        for (int i = 0; i < 10; i++)
        {
            colorexample[i].text = colors[i].ToString();
            colorexample[i].color = GetColor(colors[i].ToString());
        }
    }

}
