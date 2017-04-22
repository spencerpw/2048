using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Tile : MonoBehaviour {
	public const float TWEEN_DURATION = 0.2f;

	[SerializeField] private TextMeshProUGUI numberText;
	[SerializeField] private Image image;
	[SerializeField] private List<Color> colours;

	public RectTransform rectTransform;
	public int row;
	public int col;
	public bool merging;

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
		image.color = colours[(int)power - 1];

		numberText.color = Number > 4 ? Color.white : Color.black;

	}

	public void SpawnTween() {
		transform.localScale = Vector3.zero;
		transform.DOScale(Vector3.one,TWEEN_DURATION)
			.SetEase(Ease.OutQuad);
	}

	public void MergeTween() {
		transform.localScale = Vector3.one / 2f;
		transform.DOScale(Vector3.one,TWEEN_DURATION)
			.SetEase(Ease.OutQuad);
	}

	public void SlideTween(Vector3 target, TweenCallback cb = null) {
		transform.DOLocalMove(target,TWEEN_DURATION)
			.SetEase(Ease.OutQuad)
			.OnComplete(cb);
	}
}
