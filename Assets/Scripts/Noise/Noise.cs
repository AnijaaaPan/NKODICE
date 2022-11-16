using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour
{
    private float MoveY;
    private float IntervalTime;

    private RectTransform RectTransform;

    void Start()
    {
        RectTransform = GetComponent<RectTransform>();

        InitMove();
        StartCoroutine("MoveLineY");
    }

    private void InitMove()
    {
        MoveY = Random.Range(-5f, 5f);
        IntervalTime = Random.Range(0.01f, 0.1f);
    }

    private IEnumerator MoveLineY()
    {
        while (true)
        {
            float PosY = RectTransform.localPosition.y;
            if (PosY <= -540f || 540f <= PosY)
            {
                InitMove();
                RectTransform.localPosition = new Vector3(0, GetRandomYPos(), 0);
            };

            PosY += MoveY;
            RectTransform.localPosition = new Vector3(0, PosY, 0);
            yield return new WaitForSeconds(IntervalTime);
        }
    }

    private float GetRandomYPos()
    {
        return Random.Range(-540f, 540f);
    }
}
