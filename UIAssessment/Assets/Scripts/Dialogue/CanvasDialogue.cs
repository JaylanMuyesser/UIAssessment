using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogue : MonoBehaviour
{
    public GameObject panel;
    public Text nameText;
    public new string name;
    public string[] dialogueText;
    public Text dialogueTextbox;
    public int currentText;
    public bool isTalking;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking == true)
        {

        }
        if (Input.GetKeyDown(KeyCode.K) && isTalking == true)
        {
            currentText++;
            if(currentText >= dialogueText.Length)
            {
                currentText = 0;
                panel.SetActive(false);
                isTalking = false;
            }
        }
        nameText.text = name;
        dialogueTextbox.text = dialogueText[currentText];
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                //and that hits info is tagged NPC
                if (hitInfo.collider.CompareTag("NPC"))
                {
                    isTalking = true;
                    //Debug that we hit a NPC             
                    Debug.Log("Talk to NPC");
                    panel.SetActive(true);
                
                    
                }
                //and that hits info is tagged Item
                if (hitInfo.collider.CompareTag("Item"))
                {
                    //Debug that we hit an Item               
                    Debug.Log("Interact with item");
                }
            }
        }
    }
}

