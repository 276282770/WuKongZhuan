using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadConfig : MonoBehaviour {

    public static ReadConfig Instance;
    string configPath;
    string adLogoPath;
    void Awake()
    {
        Instance = this;
        configPath = Application.dataPath + "/config.dat";
        adLogoPath = Application.dataPath + "/ADLogo/0.png";
    }

	// Use this for initialization
	void Start () {
        //CreateFile();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreateFile()
    {
        FileInfo fi = new FileInfo(Application.dataPath + "/config.dat");
        fi.Create();
        Debug.Log(fi.FullName);
    }
    public Dictionary<string,string> ReadConfigFile()
    {
        FileInfo fi = new FileInfo(configPath);
        Dictionary<string, string> result = new Dictionary<string, string>();
        if (!fi.Exists)
            return null;
        StreamReader sr = new StreamReader(fi.OpenRead());
        string line = sr.ReadLine();
        while (line!=null)
        {
            Debug.Log("ReadLine="+line);
            string[] keyAndValue = line.Split('=');
            result[keyAndValue[0].Trim()] = keyAndValue[1].Trim();
            line = sr.ReadLine();
        }
        return result;
    }
    public void ReadParam(out int health,out int addHealth,out float runTime)
    {
        Dictionary<string, string> config = ReadConfigFile();
        health = int.Parse(config["MaxHealth"]);
        addHealth = int.Parse(config["FoodValue"]);
        runTime = float.Parse(config["Inertia"]);
    }
    public Sprite GetSprite()
    {
        FileInfo fi = new FileInfo(adLogoPath);
        FileStream fs = fi.OpenRead();
        byte[] buffer=new byte[fs.Length];
        fs.Read(buffer, 0, (int)fs.Length);
        Texture2D texture = new Texture2D(62, 63);
        Debug.Log(texture.width+" "+texture.height);
        texture.LoadImage(buffer);
        Sprite result = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
        
        return result;
    }
    
}
