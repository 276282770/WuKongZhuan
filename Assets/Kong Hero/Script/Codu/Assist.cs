using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Assist : MonoBehaviour {

    public static Assist Instance;

    Sprite loadSprite;

    string configPath;
    string adLogoPath;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        configPath = Application.dataPath + "/config.dat";
        adLogoPath = Application.dataPath + "/ADLogo/AdLogo.png";
        loadSprite = GetAdLogoSprite();
    }
    void Start () {
        
        
    }
    private void OnEnable()
    {
        //Invoke("ChangeBlockSprite", 1);
        Debug.Log("Assist Enable");
    }

    // Update is called once per frame
    void Update () {
		
	}
    //加载adLogo
    void ChangeBlockSprite()
    {
        if (loadSprite == null)
            return;
        GameObject block = GameObject.Find("Envaironment/Block");
        if (block == null)
            return;
        Transform blockParent = block.transform;
        for (int i = 0; i < blockParent.childCount; i++)
        {
            Transform t = blockParent.GetChild(i);
            if (t.name.Contains("Box Random"))
            {
                t.Find("Box Random").GetComponent<SpriteRenderer>().sprite = loadSprite;
            }
            else if (t.name.Contains("Block"))
            {
                for (int j = 0; j < t.childCount; j++)
                {
                    Transform tr = t.GetChild(j);
                    if (tr.name.Contains("Box Random"))
                    {
                        tr.Find("Box Random").GetComponent<SpriteRenderer>().sprite = loadSprite;
                    }
                }
            }
        }
    }
    public void ChangeBlockSpriteWrap()
    {
        Invoke("ChangeBlockSprite", 1);
    }

    Dictionary<string, string> ReadConfigFile()
    {
        FileInfo fi = new FileInfo(configPath);
        Dictionary<string, string> result = new Dictionary<string, string>();
        if (!fi.Exists)
            return null;
        StreamReader sr = new StreamReader(fi.OpenRead());
        string line = sr.ReadLine();
        while (line != null)
        {
            string[] keyAndValue = line.Split('=');
            result[keyAndValue[0].Trim()] = keyAndValue[1].Trim();
            line = sr.ReadLine();
        }
        return result;
    }
    public void ReadParam(out int health, out int addHealth, out float runTime)
    {
        Dictionary<string, string> config = ReadConfigFile();
        health = int.Parse(config["MaxHealth"]);
        addHealth = int.Parse(config["FoodValue"]);
        runTime = float.Parse(config["Inertia"]);
    }
    public Sprite GetAdLogoSprite()
    {
        FileInfo fi = new FileInfo(adLogoPath);
        if (!fi.Exists)
            return null;
        FileStream fs = fi.OpenRead();
        byte[] buffer = new byte[fs.Length];
        fs.Read(buffer, 0, (int)fs.Length);
        Texture2D texture = new Texture2D(62, 63);
        Debug.Log(texture.width + " " + texture.height);
        texture.LoadImage(buffer);
        Sprite result = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return result;
    }
}
