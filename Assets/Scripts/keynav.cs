using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keynav : MonoBehaviour
{
    public KeyCode key;
    public GameObject thisbtn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            thisbtn.GetComponent<Button>().onClick.Invoke();
        }
    }
}
