using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerHandler : Character
{
    #region Variables
    [Header("Physics")]
    public CharacterController controller;
    public float gravity = 20f;
    public Vector3 moveDirection;
    public bool isSprinting = false;

    [Header("Level Data")]
    public int level = 0;
    public float currentExp, neededExp, maxExp;

    [Header("Damage Flash and Death")]
    public Image damageImage;
    public Image deathImage;
    public Text deathText;
    public AudioClip deathClip;
    public AudioSource playersAudio;
    public Transform currentCheckpoint;

    public Color flashColour = new Color(1, 0, 0, 0.2f);
    public float flashSpeed = 5;
    public static bool isDead;
    public bool isDamaged;
    public bool canHeal;
    public float healDelayTimer;
    #endregion


    #region Behaviour
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
    }
    public override void Movement()
    {
        //base.Movement();
        if (controller.isGrounded)
        {
            moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprint;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = walkspeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = crouch;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 8f;
        }
    }
    public override void Update()
    {
        base.Update();
        #region Bar Update
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[0].displayImage.fillAmount = Mathf.Clamp01(attributes[0].currentValue / attributes[0].maxValue);

        }
        #endregion
#if     UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamagePlayer(5);
        }
#endif
        #region Damage Flash
        if (isDamaged && !isDead)
        {
            damageImage.color = flashColour;
            isDamaged = false;
        }
        else if (damageImage.color.a > 0)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        #endregion  
        if (!canHeal)
        {
            healDelayTimer += Time.deltaTime;
            if (healDelayTimer >= 2)
            {
                canHeal = true;
            }
            if (canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue > 0)
            {
                HealOverTime();
            }
        }
    }
    public void DamagePlayer(float damage)
    {
        //turn on the red flicker
        isDamaged = true;
        //take damage
        attributes[0].currentValue -= damage;
        //delay regeneration
        canHeal = false;
        healDelayTimer = 0;
        if (attributes[0].currentValue <= 0 && !isDead)
        {
            Death();
        }
    }
    public void HealOverTime()
    {
        attributes[0].currentValue += Time.deltaTime * (attributes[0].regenValue);
    }
    #endregion
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckpoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 8;
            }
            PlayerSaveAndLoad.Save();
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckpoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 8;
            }
        }

    }
   
    #region Death and Respawn
    void Death()
    {
        //set the death flag to dead
        isDead = true;
        //clear existing text just in case
        deathText.text = "";
        //set and play audio clip
        playersAudio.clip = deathClip;
        playersAudio.Play();
        //trigger death screen
        deathImage.GetComponent<Animator>().SetTrigger("isDead");

        //set our death text after 2 seconds
        Invoke("DeathText", 2f);
        //set our death text to respawn text after 6 seconds
        Invoke("RespawnText", 6f);
        //Respawn after 9 seconds
        Invoke("Respawn", 9f);

    }
    void DeathText()
    {
        deathText.text = "Bruh u ded";

    }
    void RespawnText()
    {
        deathText.text = "Jk bro come back";

    }
    void Respawn()
    {
        //reset everything
        deathText.text = "";
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].currentValue = attributes[i].maxValue;
        }
        isDead = false;
        //load position
        this.transform.position = currentCheckpoint.position;
        this.transform.rotation = currentCheckpoint.rotation;
        //respawn
        deathImage.GetComponent<Animator>().SetTrigger("Respawn");
    }
    #endregion
}
