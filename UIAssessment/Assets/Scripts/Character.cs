using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Stats
{
    #region Variables
    [Header("Character Data")]
    public new string name;
    [Header("Movement Variables")]
    public float speed = 9f;
    public float crouch = 4f;
    public float sprint = 15f;
    public float jumpSpeed = 9f;
    public float walkspeed = 9f;
    #endregion

    #region Behaviour
    public virtual void Movement()
    {
        //Default movements written here
        Debug.Log("Parent Movement");
    }
    #endregion
    public virtual void Update()
    {
        Movement();
    }
}
