using UnityEngine;
using System.Collections;

public class MoveNoises : MonoBehaviour
{
    static public MoveNoises instance;

    public GameObject InitNoise;
    public int NoiseCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < NoiseCount; i++)
        {
            GameObject NoiseObject = Instantiate(InitNoise);

            float RandomYPos = GetRandomYPos();

            NoiseObject.SetActive(true);
            NoiseObject.transform.SetParent(transform);

            RectTransform RectTransform = NoiseObject.GetComponent<RectTransform>();
            RectTransform.localPosition = new Vector3(0, RandomYPos, 0);
            RectTransform.localScale = Vector3.one;
        }
    }

    public void ClickPopNoise()
    {
        for (int i = 1; i <= NoiseCount; i++)
        {
            GameObject NoiseObject = transform.GetChild(i).gameObject;
            StartCoroutine(PopNoise(NoiseObject));
        }
    }

    private float GetRandomYPos()
    {
        return Random.Range(-540f, 540f);
    }

    private IEnumerator PopNoise(GameObject NoiseObject)
    {
        LineRenderer LineRenderer = NoiseObject.GetComponent<LineRenderer>();
        float RandomIntervalTime = Random.Range(0.01f, 0.025f);
        float RandomColorAlpha = Random.Range(0.1f, 0.2f);

        float[] ColorAlphaList = new float[11] { 
            0, RandomColorAlpha * 1, RandomColorAlpha * 2, RandomColorAlpha * 3, RandomColorAlpha * 4, RandomColorAlpha * 5,
               RandomColorAlpha * 4, RandomColorAlpha * 3, RandomColorAlpha * 2, RandomColorAlpha * 1, 0
        };
        for (int i = 0; i < ColorAlphaList.Length; i++)
        {
            float ColorAlpha = ColorAlphaList[i];
            LineRenderer.colorGradient = CreateColor(ColorAlpha);
            yield return new WaitForSeconds(RandomIntervalTime);
        }
    }

    private Gradient CreateColor(float alpha)
    {
        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 1.0f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = alpha;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = alpha;
        alphaKey[1].time = 1.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKey, alphaKey);
        return gradient;
    }
}
