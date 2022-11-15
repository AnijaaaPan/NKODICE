using UnityEngine;
using UnityEngine.UI;

public class AwakeTitle : MonoBehaviour
{
    public Image Image;

    private float speed = 1.75f;
    private Color Color = Color.black;
    private float colorValue = 1;

    void Update()
    {
        if (Color.a <= 0.4f) return;

        Color.a = colorValue;
        Image.color = Color;
        colorValue -= speed * Time.deltaTime;
    }
}
