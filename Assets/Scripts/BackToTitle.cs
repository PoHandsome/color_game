using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour
{
    public Button BackTitle;

    private void Start()
    {
        BackTitle.GetComponent<Button>().onClick.AddListener(LoadTitleScene);
    }
    void LoadTitleScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
