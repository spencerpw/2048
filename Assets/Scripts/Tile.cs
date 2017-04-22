using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Tile : MonoBehaviour {
	const float TWEEN_DURATION = 0.2f;

	[SerializeField] private TextMeshProUGUI numberText;
	[SerializeField] private Image image;
	[SerializeField] private Color startColour;
	[SerializeField] private Color endColour;

	public RectTransform rectTransform;
	public int row;
	public int col;
	public bool merged;

	private int number;

	public int Number {
		get {
			return number;
		}
		set {
			number = value;
			numberText.text = name = number.ToString();
			SetColour();
		}
	}

	private void Awake() {
		Number = Random.Range(0,2) == 0 ? 2 : 4;
	}

	public void SetColour() {
		float power = Mathf.Log(Number) / Mathf.Log(2);
		image.color = Color.Lerp(startColour,endColour, (power-1)/11f);

	}

	public void SpawnTween() {
		transform.localScale = Vector3.zero;
		transform.DOScale(Vector3.one,TWEEN_DURATION)
			.SetEase(Ease.OutQuad)
			.OnComplete( () => {
				Messenger.Broadcast("EnableInput");
			});
	}
}
