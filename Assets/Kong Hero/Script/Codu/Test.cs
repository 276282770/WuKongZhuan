using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {


    public Transform blockParent;
    Sprite loadSprite;
    
	// Use this for initialization
	void Start () {
        loadSprite = ReadConfig.Instance.GetSprite();
        Invoke("ChangeBlockSprite",1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ChangeBlockSprite()
    {
        Sprite sp = loadSprite;
        for (int i = 0; i < blockParent.childCount; i++)
        {
            Transform t = blockParent.GetChild(i);
            if(t.name.Contains("Box Random"))
            {
                t.Find("Box Random").GetComponent<SpriteRenderer>().sprite = sp;
            }
            else if (t.name.Contains("Block"))
            {
                for(int j=0;j<t.childCount;j++)
                {
                    Transform tr = t.GetChild(j);
                    if (tr.name.Contains("Box Random"))
                    {
                        tr.Find("Box Random").GetComponent<SpriteRenderer>().sprite = sp;
                    }
                }
            }
        }
    }
}
