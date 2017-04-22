using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Board : MonoBehaviour {
	[SerializeField] private int rows;
	[SerializeField] private int columns;
	[SerializeField] private float tilePadding;
	[SerializeField] private Image tileBackground;
	[SerializeField] private Transform backgroundRoot;
	[SerializeField] private Transform tileRoot;
	[SerializeField] private Tile tilePrefab;

	public RectTransform rectTransform;

	private Tile[,] tiles;
	private Vector2 tileSize;

	private void Awake() {
		tiles = new Tile[rows,columns];

		CalculateSizes();
	}

	private void Start() {
		DrawEmptyTiles();

		//Begin game with two tiles
		SpawnTile();
		SpawnTile();
	}

	private void CalculateSizes() {
		tileSize = rectTransform.rect.size / rows; //Assume it's always square
		tileSize.x -= tilePadding + tilePadding/rows;
		tileSize.y -= tilePadding + tilePadding/columns;
	}

	private void DrawEmptyTiles() {
		for(int i = 0; i < rows; i++) {
			for(int j = 0; j < columns; j++) {
				Image newBG = GameObject.Instantiate(tileBackground, backgroundRoot);
				newBG.name = string.Format("{0},{1}",j,i);
				newBG.rectTransform.sizeDelta = tileSize;
				newBG.rectTransform.localPosition = GetBoardPosition(i,j);

			}
		}
	}

	private Vector3 GetBoardPosition(int row, int col) {
		Vector3 pos = Vector3.zero;

		pos.x = col * tileSize.x + (col+1) * (tilePadding);
		pos.y = row * tileSize.y + (row+1) * (tilePadding);

		return pos;
	}

	public void SpawnTile() {
		//First check if board is full in a fast to write slow as hell crappy way
		if(FindObjectsOfType<Tile>().Count() == rows * columns) {
			Debug.LogWarning("Board::SpawnTile A tile could not be spawned. The board is full");
			return;
		}

		//Find an empty space
		//pick a random starting point
		//if it's occupied iterate through until reaches beginning
		//O(n*m) worst case where 
		int r = Random.Range(0,rows);
		int c = Random.Range(0,columns);

		if(tiles[r,c] != null) {
			for(int i = r+1; i != r; i++) {
				if(i >= rows) i = 0;

				for(int j = 0; j < columns; j++) {
					if(tiles[i,j] == null) {
						r = i;
						c = j;
						break;
					}
				}

				if(tiles[r,c] == null) break;
			}
		}


		Tile newTile = GameObject.Instantiate(tilePrefab, tileRoot);
		newTile.rectTransform.sizeDelta = tileSize;
		newTile.transform.localPosition = GetBoardPosition(r,c);
		tiles[r,c] = newTile;
	}

	#if UNITY_EDITOR

	[ContextMenu("Spawn Tile")]
	public void TestSpawn() {
		SpawnTile();
	}

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
