using System.Collections;
using System.Collections.Generic; //Dictionary 
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text forward, left, right, back, jump, interact, sprint, crouch, inventory;
    public GameObject currentKey;
    public Color32 changed = new Color32(39, 171, 249, 255);
    public Color32 selected = new Color32(239, 116, 36, 255);

    void Start()
    {
        keys.Add("Forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "Left")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Back", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Back", "S")));

        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Intreract", "E")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        keys.Add("Crouch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl")));
        keys.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "Tab")));

        forward.text = keys["Forward"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        back.text = keys["Back"].ToString();

        jump.text = keys["Jump"].ToString();
        interact.text = keys["Interact"].ToString();
        sprint.text = keys["Sprint"].ToString();
        crouch.text = keys["Crouch"].ToString();
        inventory.text = keys["Inventory"].ToString();
    }
    private void OnGUI()//Events
    {
        if(currentKey != null)
        {
            string newKey = "";
            Event e = Event.current;
            if (e.isKey)
            {
                newKey = e.keyCode.ToString();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (newKey != "")
            {
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                currentKey.GetComponentInChildren<Text>().text = newKey;
                currentKey.GetComponent<Image>().color = changed;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;

        if (currentKey != null)
        {
            //currentKey.GetComponent<Image>().color = changed;
            currentKey.GetComponent<Image>().color = selected;
        }
    }
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            PlayerPrefs.Save();
        }
    }

}
