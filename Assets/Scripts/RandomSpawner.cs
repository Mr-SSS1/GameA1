using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GachaItem
{
    public GameObject prefab;
    public float rate;
    public string name;
    public int count;
    public float average;
}

public class RandomSpawner : MonoBehaviour
{
    public int totalCount;
    public int maxCount;
    public GachaItem[] items;
    [SerializeField] Text Text;
    [SerializeField] Transform spawnPoint;

    GameObject currentObject; // 前回のオブジェクトを記憶

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        { 
            SpawnGacha();
        }
        totalCount = items[0].count + items[1].count + items[2].count + items[3].count;

     
        
        for (int i = 0; i < items.Length; i++)
        {
            if (totalCount == 0)
            {
                items[i].average = 0f; // 0 で割れないので適当に代入
            }
            else
            {
                items[i].average = (float)items[i].count / totalCount * 100f;
            }
        }
    }

    public void SpawnGacha()
    {
        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        //maxCount が 499 以上なら item[2] を強制で出す ---所謂天井---
        if (maxCount <= 1)
        {
            currentObject = Instantiate(items[3].prefab, spawnPoint.position, Quaternion.identity);
            currentObject.name = items[3].name;
            items[3].count++;
            Text.text = currentObject.name;
            maxCount = 500;   // ここで500にする

            return; // 抽選を行わず確定終了
        }

        //確率の合計を計算
        float total = 0;
        foreach (var item in items)
        {
            total += item.rate;
        }

        //ランダム抽選
        float rand = Random.Range(0, total);
        foreach (var item in items)
        {
            if (rand < item.rate)
            {
                currentObject = Instantiate(item.prefab, spawnPoint.position, Quaternion.identity);
                currentObject.name = item.name;
                item.count++;
                Text.text = currentObject.name;

                if (currentObject.name == "Mythic")
                {
                    maxCount = 500;
                }
                else
                {
                    maxCount--;
                }
                return;
            }
            rand -= item.rate;
        }
    }

    public void ResetData()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].count = 0;
            totalCount = 0;
            maxCount = 500;
        }
    }
}