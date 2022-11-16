using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[System.Serializable]
public class ObjectPair
{
	public Image Image;
	public TextMeshProUGUI TextMeshProUGUI;
}

public class OnClickMenu : MonoBehaviour, IPointerClickHandler
{
	public GameObject Option;
	public GameObject Arcade;
	public GameObject FreeRole;
	public GameObject AutoPlay;
	public GameObject Quit;

	public GameObject TitleObject;
	public RectTransform TitleRectTransform;
	public GameObject OptionObject;

	public ObjectPair ObjectPair;
	public CanvasGroup StartMenuCanvas;

	private float time;
	readonly float FlashingInterval = 0.0001f;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			StartCoroutine(ObjectFlashing(eventData.pointerEnter));
		}
	}

	private IEnumerator ObjectFlashing(GameObject Object)
	{
		for (int i = 0; i < 20; i++)
		{
			ObjectPair.TextMeshProUGUI.color = GetAlphaColor(ObjectPair.TextMeshProUGUI.color);
			ObjectPair.Image.color = ObjectPair.TextMeshProUGUI.color;
			yield return new WaitForSeconds(FlashingInterval);
		}

		ObjectPair.TextMeshProUGUI.color = new Color(0, 0, 0, 0);
		ObjectPair.Image.color = ObjectPair.TextMeshProUGUI.color;

		ZoomIn(-10, 0.9f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-20, 0.8f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-30, 0.7f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-40, 0.6f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-60, 0.4f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-80, 0.2f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-100, 0.1f);
		yield return new WaitForSeconds(FlashingInterval);
		ZoomIn(-150, 0f);

		TitleObject.SetActive(false);

		if (Object == Quit)
		{
			QuitGame();
			yield return null;
		}

		ObjectFunc(Object);
	}

	private Color GetAlphaColor(Color color)
	{
		time += Time.unscaledDeltaTime * 20;
		color.a = Mathf.Sin(time) * 0.5f + 0.5f;
		return color;
	}

	private void ZoomIn(int positionZ, float colorAlpha)
	{
		TitleRectTransform.localPosition = new Vector3(0, 0, positionZ);
		StartMenuCanvas.alpha = colorAlpha;
	}

	private void ObjectFunc(GameObject Object)
    {
		if (Object == Option)
		{
			OptionObject.SetActive(true);
			return;
		}

		TitleObject.SetActive(false);
		if (Object == Arcade)
		{
			GameStart.instance.InitGame(1);
		}
		else if (Object == FreeRole)
		{
			GameStart.instance.InitGame(2);
		} else
		{
			GameStart.instance.InitGame(3);
		}
	}

	private void QuitGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
			UnityEngine.Application.Quit();
		#endif
	}
}
