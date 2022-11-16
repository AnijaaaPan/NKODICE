using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOutImage : MonoBehaviour
{
    static public FadeInOutImage instance;
    public Image Image;

    private Color Color = Color.black;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(FadeInOut(true, 0.005f));
    }


    public IEnumerator FadeInOut(bool IsIn, float ChangeValue)
    {
        for (int i = 0; i < 20; i++)
        {
            Color.a += IsIn == true ? -ChangeValue : ChangeValue;
            Image.color = Color;
            yield return new WaitForSeconds(0.025f);
        }
        yield break;
    }
}
