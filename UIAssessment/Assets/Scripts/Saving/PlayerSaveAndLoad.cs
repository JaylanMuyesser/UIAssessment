using UnityEngine;

public class PlayerSaveAndLoad : MonoBehaviour
{
    public static PlayerHandler player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        if (!PlayerPrefs.HasKey("Loaded"))
        {
            PlayerPrefs.DeleteAll();
            //FirstLoad Function - - - Sets up Player Data
            FirstLoad();
            //Save Data creates first save file in binary
            Save();
            //We now have our first save file
            PlayerPrefs.SetInt("Loaded", 0);
        }
        else
        {
            Load();
        }
    }
    void FirstLoad()
    {
        player.name = "Bruh";
        player.level = 1;
        player.currentCheckpoint = GameObject.Find("First Checkpoint").GetComponent<Transform>();

        player.maxExp = 60;

        for (int i = 0; i < 3; i++)
        {
            player.attributes[i].maxValue = 100;
            player.attributes[i].currentValue = 100;
        }
        for (int i = 0; i < 6; i++)
        {
            player.characterStats[i].value = 10;
        }
    }
    public static void Save()
    {
        PlayerBinary.SavePlayerData(player);
    }
    public static void Load()
    {
        PlayerData data = PlayerBinary.LoadPlayerData(player);
        player.name = data.playerName;
        player.level = data.level;
        player.currentCheckpoint = GameObject.Find(data.checkpoint).GetComponent<Transform>();
        player.currentExp = data.currentExp;
        player.neededExp = data.neededExp;
        player.maxExp = data.maxExp;
        //Arays
        for (int i = 0; i < player.characterStats.Length; i++)
        {
            player.characterStats[i].value = data.stats[i];
        }

        for (int i = 0; i < player.attributes.Length; i++)
        {
            player.attributes[i].maxValue = data.maxAttributes[i];
            player.attributes[i].currentValue = data.currentAttributes[i];
        }
        //Position
        player.transform.position = new Vector3(data.pX, data.pY, data.pZ);
        player.transform.rotation = new Quaternion(data.rX, data.rY, data.rZ, data.rW);
}
    void Start()
    {

    }
}
