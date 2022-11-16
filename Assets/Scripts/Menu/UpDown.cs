using UnityEngine;

public class UpDown : MonoBehaviour
{
    readonly float speed = 0.5f;
    readonly float height = 1.5f;
    readonly float rotate = 0.1f;

    RectTransform UpDownRectTransform;

    void Start()
    {
        UpDownRectTransform = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time * speed);
        UpDownRectTransform.localPosition = new Vector3(0, sin * height, 0);
        UpDownRectTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, sin * rotate);
    }
}
