using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class healthManager : MonoBehaviour
{
    [SerializeField] TextAsset file;
    string characterName;
    int hp = 0;
    int at = 0;
    private void Start()
    {
        read("Prince");
        read("Tree");
    }
    public void write()
    {
        string path = "Assets/text/" + file.name + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("\ntest");
        writer.Close();
    }
    public void read(string name)
    {
        /*
        string path = "Assets/text/" + file.name + ".txt";
        StreamReader reader = new StreamReader(path);
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines) 
        {
            if(line.Split(':')[0] == name)
            {
                string health = line.Split(':')[1];
                string attack = line.Split(':')[2];
                hp = System.Int32.Parse(health);
                at = System.Int32.Parse(attack);
                //print(hp);
                //print(at);
            }
        }

        reader.Close();
        */
    }
}
