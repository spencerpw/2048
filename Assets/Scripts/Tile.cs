using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI numberText;
	[SerializeField] private Image image;
	[SerializeField] private Color startColour;
	[SerializeField] private Color endColour;

	public RectTransform rectTransform;
	public int row;
	public int col;

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
}
