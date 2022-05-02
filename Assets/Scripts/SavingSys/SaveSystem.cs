using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void Save(List<HEvent> history, float latestAmount)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/backup.fun";
        FileStream fs = new FileStream(path, FileMode.Create);

        var data = new DataMono(history, latestAmount);

        formatter.Serialize(fs, history);
        fs.Close();
    }

    public static DataMono Load()
    {
        string path = Application.persistentDataPath + "/backup.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter =new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            DataMono data = formatter.Deserialize(fs) as DataMono;
            fs.Close();

            return data;
        }
        else
        {
            return default(DataMono);
        }
    }
}

public class DataMono
{
    public List<HEvent> history;
    public float latestAmount;

    public DataMono(List<HEvent> history, float latestAmount)
    {
        this.history = history;
        this.latestAmount = latestAmount;
    }
}