using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (InventoryToken player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fart";
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
