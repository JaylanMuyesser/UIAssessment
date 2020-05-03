using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApprovalDialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDlg;
    //index for our current line of dialogue and an index for a set question marker of the dialogue 
    public int index, optionIndex;
    public Vector2 scr;
    //object reference to the player
    public Player.MouseLook playerMouseLook;
    //mouselook script reference for the maincamera
    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public new string name;
    //array for text for our dialogue
    public string[] dialogueText;
    //APPROVAL VARIABLES
    public string[] negText, neuText, posText;
    public int approval;
    public string response1, response2;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        dialogueText = new string[neuText.Length];
        dialogueText = neuText;

    }

    private void OnGUI()
    {
        if (showDlg)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, 3 * scr.y), name + " : " + dialogueText[index]);
            if (approval < -1)
            {
                dialogueText = negText;
            }
            if (approval == 0)
            {
                dialogueText = neuText;
            }
            if (approval > 1)
            {
                dialogueText = posText;
            }


            if (!(index >= dialogueText.Length - 1 || index == optionIndex))
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Next"))
                {
                    index++;
                }
            }
            else if (index == optionIndex)
            {
                if (GUI.Button(new Rect(14 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Accept"))
                {
                    index++;
                    if (approval < 1)
                    {
                        approval++;
                    }
                }
                if (GUI.Button(new Rect(12 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Decline"))
                {
                    index = dialogueText.Length - 1;
                    approval--;
                }
            }
            else
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                {
                    showDlg = false;
                    index = 0;
                    Camera.main.GetComponent<Player.MouseLook>().enabled = true;
                    playerMouseLook.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}

