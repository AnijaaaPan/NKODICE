using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AwakeTitle : MonoBehaviour
{
    public Image Image;

    private Color Color = Color.black;

    void Start()
    {
        StartCoroutine("ChangeColorAlpha");
    }

    private IEnumerator ChangeColorAlpha()
    {
        for (int i = 0; i < 5; i++)
        {
            Color.a -= 0.02f;
            Image.color = Color;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
