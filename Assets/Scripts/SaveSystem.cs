using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (InventoryToken player)
    {
        Debug.Log(Application.persistentDataPath);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fart";
        Debug.Log(Application.persistentDataPath);
        FileStream stream = new FileStream(path, FileMode.Create);


        //PlayerData data = new PlayerData(player);
        Debug.Log(path);
        formatter.Serialize(stream, player);
        stream.Close();
    }

    public static InventoryToken LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.fart";
        if (File.Exists(path))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryToken data = formatter.Deserialize(stream) as InventoryToken;
            stream.Close();

            return data;

        } else
        {
            Debug.Log("No save file in " + path);
            return null;
        }

    }
}
