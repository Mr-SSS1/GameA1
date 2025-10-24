using System.Collections;
using UnityEngine;

public class Sprites : MonoBehaviour
{
    [SerializeField] GameObject obj;
    SpriteRenderer sr;
    Color col;

    void Start()
    {
        sr = obj.GetComponent<SpriteRenderer>();
        col = sr.color;

        // 初期で透明にしたい場合（任意）
        col.a = 0f;
        sr.color = col;

        // 開始後すぐフェードイン
        StartCoroutine(FadeIn(0.5f));
        StartCoroutine(WaitAndFadeOut(2f, 0.5f)); // 3秒待ってから1秒かけてフェードアウト
    }

    IEnumerator WaitAndFadeOut(float wait, float time)
    {
        yield return new WaitForSeconds(wait);
        StartCoroutine(FadeOut(time));
    }

    // フェードイン（time秒かけて透明→不透明）
    IEnumerator FadeIn(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            col.a = t / time;      // 0 → 1 へ
            sr.color = col;
            yield return null;
        }
        col.a = 1f;
        sr.color = col;
    }

    // フェードアウト（time秒かけて不透明→透明）
    IEnumerator FadeOut(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            col.a = 1f - (t / time); // 1 → 0 へ
            sr.color = col;
            yield return null;
        }
        col.a = 0f;
        sr.color = col;
    }
}