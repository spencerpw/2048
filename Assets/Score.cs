using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI scoreText;

	private int points;

	private void Start() {
		Messenger.AddListener<int>("AddPoints",AddPoints);
		Messenger.AddListener<int>("SetPoints",SetPoints);

	}

	public void AddPoints(int p) {
		points += p;
		scoreText.text = points.ToString();
	}

	public void SetPoints(int p) {
		points = p;
		scoreText.text = points.ToString();
	}
}
