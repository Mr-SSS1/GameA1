using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textcount : MonoBehaviour
{
    public Text[] texts;
    public Text totalText;
    public Text cmxText;
    public Text[] aveText;
    public RandomSpawner rs;

    private void Start()
    {
        
    }

    private void Update()
    {
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = rs.items[i].name+ " : " + rs.items[i].count.ToString();
            aveText[i].text = rs.items[i].average.ToString("F2") + " %";
        }
        totalText.text = "Total : " + rs.totalCount;
        cmxText.text = "Mythic guaranteed after " + rs.maxCount + " opens";
    }
}
