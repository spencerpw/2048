using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {
	[SerializeField] private int rows;
	[SerializeField] private int columns;
	[SerializeField] private float tilePadding;
	[SerializeField] private Image tileBackground;

	private RectTransform rectTransform;
	private Tile[,] tiles;
	private Vector2 tileSize;

	private void Awake() {
		rectTransform = GetComponent<RectTransform>();
		tiles = new Tile[rows,columns];

		CalculateSizes();
	}

	private void Start() {
		DrawEmptyTiles();
	}

	private void CalculateSizes() {
		tileSize = rectTransform.rect.size / rows; //Assume it's always square
		tileSize.x -= tilePadding + tilePadding/rows;
		tileSize.y -= tilePadding + tilePadding/columns;
	}

	private void DrawEmptyTiles() {
		for(int i = 0; i < rows; i++) {
			for(int j = 0; j < columns; j++) {
				Image newBG = GameObject.Instantiate(tileBackground, transform);
				Vector3 pos = Vector3.zero;

				pos.x = j * tileSize.x + (j+1) * (tilePadding);
				pos.y = i * tileSize.y + (i+1) * (tilePadding);

				newBG.name = string.Format("{0},{1}",j,i);
				newBG.rectTransform.sizeDelta = tileSize;
				newBG.rectTransform.localPosition = pos;
			

			}
		}
	}

	#if UNITY_EDITOR

	[ContextMenu("Redraw")]
	public void RedrawBoard() {
		foreach(Transform t in transform) {
			Destroy(t.gameObject);
		}

		CalculateSizes();
		DrawEmptyTiles();
	}

	#endif
}
