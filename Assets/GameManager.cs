using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Text ColorText;
    public Text CorrectAnswer;
    public GameObject[] btns = {};
    public ArrayList colors = new ArrayList();
    public Text HighScore;
    public Text CurrentScore;
    public Slider TimeBar;
    private int[] counter = { 0, 0 };
    private int rnd;
    private int ans_num;
    private int rnd_btn;
    int[] ans_options = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    float point;
    float score;
    float high_score;
    float timer;

    // color dictionary and color getting function for visual color
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
    
    // set arraylist for random take and addlistener for buttons
    private void Start()
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
        for (int j = 0; j < 4; j++)
        {
            btns[j].GetComponent<Button>().onClick.AddListener(CheckAnswer);
            btns[j].SetActive(false);
        }
        btns[4].GetComponent<Button>().onClick.AddListener(GameStart);
        btns[5].GetComponent<Button>().onClick.AddListener(title);
        TimeBar.gameObject.SetActive(false);
        timer = -999999999;
    }

    void title()
    {
        for (int j = 0; j < 4; j++)
        {
            btns[j].SetActive(false);
        }
        TimeBar.gameObject.SetActive(false);
        timer = -999999999;
        ColorText.fontSize = 80;
        ColorText.text = "Color Game";
        btns[4].GetComponentInChildren<Text>().text = "Start";
        btns[4].SetActive(true);
    }
    // Game starts, reset all parameters needed and set the first question
    void GameStart()
    {
        ColorText.fontSize = 100;
        timer = 0;
        counter[0] = 0;
        counter[1] = 0;
        score = 0f;
        CurrentScore.text = string.Format("Score: {0}", score.ToString("F0"));
        btns[4].SetActive(false);
        CorrectAnswer.text = "";
        for (int j = 0; j < 4; j++)
        {
            btns[j].SetActive(true);
        }
        TimeBar.gameObject.SetActive(true);
        NextQuestion();
    }
    
    // Player has 3 seconds to answer, if player doesn't answer then go to next question without calculating score
    // if 20 questions are played, then show the final score and restart button
    private void Update()
    {
        if (counter[0] == 20)
        {
            for (int j = 0; j < 4; j++)
            {
                btns[j].SetActive(false);
            }
            ColorText.fontSize = 50;
            ColorText.text = "Scores:" + score.ToString("F0");
            CorrectAnswer.text = "Correct Answer:" + counter[1].ToString() + "/20";
            btns[4].SetActive(true);
            btns[4].GetComponentInChildren<Text>().text = "Restart";
            TimeBar.gameObject.SetActive(false);
            timer = -999999999;
        }
        else if (timer <= 3)
        {
            timer += Time.deltaTime;
            TimeBar.value = timer / 3f;
        }
        else if (timer > 3)
        {
            timer = 0;
            NextQuestion();
        }
    }

    // reset question text and buttons
    // store the ans_num to retrieve the answer from colors array and prevent duplicate texts for buttons, for further details see SetButton()
    void NextQuestion()
    {
        ColorText.text = colors[Random.Range(0, 10)].ToString();
        ans_num = (Random.Range(0, 100) % 10);
        ChangeColor(colors[ans_num].ToString());
        SetButton();
        counter[0]++;
    }

    // highest points for each question is 100, the points decays exponentially, the lowest point for correct answer is 100/9 (around 11) point
    // if correct answer in selected in 1 second, then player get 100 point
    void CountScore()
    {
        point = Mathf.Pow(((4f - timer) / 3f), 2f) * 100;
        score += Mathf.Min(point, 100f);
        CurrentScore.text = string.Format("Score: {0}", score.ToString("F0"));
        high_score = Mathf.Max(high_score, score);
        HighScore.text = string.Format("High Score: {0}", high_score.ToString("F0"));
    }
    void ChangeColor(string Ans_color)
    {
        ColorText.color = GetColor(Ans_color);
    }

    // buttons are stored in an array, each time the correct answer are shown in the random button
    // the rest buttons will get none duplicate color text and also not the color of answer
    // ans_options are used to ensure the text in buttons are not duplicated
    void SetButton()
    {
        rnd = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            ans_options[ans_num] = -1;
            if (i == rnd)
            {
                btns[i].GetComponentInChildren<Text>().text = colors[ans_num].ToString();
            }
            else
            {
                rnd_btn = Random.Range(0, 10);
                while (ans_options[rnd_btn] == -1)
                {
                    rnd_btn = Random.Range(0, 10);
                }
                ans_options[rnd_btn] = -1;
                btns[i].GetComponentInChildren<Text>().text = colors[rnd_btn].ToString();
            }
        }
        ans_options = new int[ ] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    // check the string of button hit is the same with answer or not
    void CheckAnswer()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text == colors[ans_num].ToString())
        {
            CountScore();
            counter[1]++;
        }
        timer = 0;
        NextQuestion();
    }
}

   
