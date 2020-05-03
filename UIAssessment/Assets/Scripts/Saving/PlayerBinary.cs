using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class PlayerBinary
{
    public static void SavePlayerData(PlayerHandler player)
    {
        //reference binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //location to save
        string path = Application.persistentDataPath + "/" + "bruhmoment" + ".jpeg";
        //create file at file path
        FileStream stream = new FileStream(path, FileMode.Create);
        //what data to write to the file
        PlayerData data = new PlayerData(player);
        //write it and convert it to bytes for writing to binary
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayerData(PlayerHandler player)
    {
        //Location to save
        string path = Application.persistentDataPath + "/" + "bruhmoment" + ".jpeg";

        //if we have a file in that path
        if (File.Exists(path))
        {
            //get binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //read the data from the path
            FileStream stream = new FileStream(path, FileMode.Open);
            //set the data from what it is back to the usable variables
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            //send data back to the PlayerDataToSave script
            return data;
        }
        return null;
    }
}
