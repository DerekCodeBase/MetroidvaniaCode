using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void onContinue()
    {
        SceneManager.LoadScene("UnderShrineScene");
    }

    public void onQuit()
    {
        Application.Quit();
    }
}
