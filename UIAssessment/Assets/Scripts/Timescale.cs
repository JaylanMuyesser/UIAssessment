using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timescale : MonoBehaviour
{

    public void TogglePause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;

    }

    public void Unpause()
    {
        Time.timeScale = 1;

    }
}
