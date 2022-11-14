using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHoverEscapeImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Image CircleImage;
	public Image EscapeImage;

	public void OnPointerEnter(PointerEventData eventData)
	{
		CircleImage.color = new Color(0.75f, 0.75f, 0.75f);
		EscapeImage.color = new Color(0.75f, 0.75f, 0.75f);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		CircleImage.color = new Color(0.25f, 0.25f, 0.25f);
		EscapeImage.color = new Color(0.25f, 0.25f, 0.25f);
	}
}
