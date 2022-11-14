using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[System.Serializable]
public class ObjectPair {
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

	public RectTransform Title;

	public ObjectPair ObjectPair;
	public ObjectPair[] ObjectPairs;

	private float time;
	readonly float FlashingInterval = 0.0001f;

	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine("ObjectFlashing");
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (eventData.pointerEnter == Quit)
			{
				QuitGame();
			}
		}
	}

	private IEnumerator ObjectFlashing()
	{
		for (int i = 0; i < 10; i++)
		{
			ObjectPair.TextMeshProUGUI.color = GetAlphaColor(ObjectPair.TextMeshProUGUI.color);
			ObjectPair.Image.color = ObjectPair.TextMeshProUGUI.color;
			yield return new WaitForSeconds(FlashingInterval);
		}

		ObjectPair.TextMeshProUGUI.color = new Color(0, 0, 0, 0);
		ObjectPair.Image.color = ObjectPair.TextMeshProUGUI.color;

		Title.localPosition = new Vector3(0, 0, -10);
		yield return new WaitForSeconds(FlashingInterval);
		Title.localPosition = new Vector3(0, 0, -20);
		yield return new WaitForSeconds(FlashingInterval);
		Title.localPosition = new Vector3(0, 0, -30);
		yield return new WaitForSeconds(FlashingInterval);
		Title.localPosition = new Vector3(0, 0, -40);
		yield return new WaitForSeconds(FlashingInterval);
		Title.localPosition = new Vector3(0, 0, -60);
		yield return new WaitForSeconds(FlashingInterval);
		Title.localPosition = new Vector3(0, 0, -80);
		yield return new WaitForSeconds(FlashingInterval);
		Title.localPosition = new Vector3(0, 0, -100);
	}

	private Color GetAlphaColor(Color color)
	{
		time += Time.deltaTime * 20;
		color.a = Mathf.Sin(time) * 0.5f + 0.5f;
		return color;
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
