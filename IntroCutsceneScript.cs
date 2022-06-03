using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneScript : MonoBehaviour
{

    private float cutsceneLength = 10f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        
    }

    void Update()
    {
        if(Time.time >= startTime + cutsceneLength)
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
