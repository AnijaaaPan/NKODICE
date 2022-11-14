using System.Collections;
using UnityEngine;

public class ActiveOption : MonoBehaviour
{
	public RectTransform OptionTransform;
	public CanvasGroup OptionCanvas;

	readonly float Interval = 0.0001f;

	IEnumerator Start()
    {
		yield return new WaitForSeconds(0.5f);

		for (int i = 1; i <= 10; i++)
		{
			OptionZoomOut(-30 + i * 3, i * 0.1f);
			yield return new WaitForSeconds(Interval);
		}
	}

	private void OptionZoomOut(int positionZ, float colorAlpha)
	{
		OptionTransform.localPosition = new Vector3(0, 0, positionZ);
		OptionCanvas.alpha = colorAlpha;
	}
}
