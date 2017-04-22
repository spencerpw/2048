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

	private Tile[,] tileGrid;
	private List<Tile> tiles; //to have an enumerable to iterate on
	private Vector2 tileSize;

	private void Awake() {
		tileGrid = new Tile[columns,rows];
		tiles = new List<Tile>();

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
		for(int x = 0; x < columns; x++) {
			for(int y = 0; y < rows; y++) {
				Image newBG = GameObject.Instantiate(tileBackground, backgroundRoot);
				newBG.name = string.Format("{0},{1}",x,y);
				newBG.rectTransform.sizeDelta = tileSize;
				newBG.rectTransform.localPosition = GetBoardPosition(x,y);

			}
		}
	}

	private Vector3 GetBoardPosition(int x, int y) {
		Vector3 pos = Vector3.zero;

		pos.x = x * tileSize.x + (x+1) * (tilePadding);
		pos.y = y * tileSize.y + (y+1) * (tilePadding);

		return pos;
	}

	public void SpawnTile() {
		//First check if board is full in a fast to write slow as hell crappy way
		if(tiles.Count() == rows * columns) {
			Debug.LogWarning("Board::SpawnTile A tile could not be spawned. The board is full");
			return;
		}

		//Find an empty space
		//pick a random starting point
		//if it's occupied iterate through until reaches beginning
		//O(n*m) worst case where 
		int r = Random.Range(0,rows);
		int c = Random.Range(0,columns);

		//This is still pretty bad, but good enough
		if(tileGrid[c,r] != null) {
			for(int y = r+1; y != r; y++) {
				if(y >= rows) { 
					y = -1;
					continue;
				}

				if(tileGrid[c,y] == null) {
					r = y;
					break;
				}
			}

			if(tileGrid[c,r] != null) {
				for(int x = c+1; x != c; x++) {
					if(x >= columns) x = 0;

					for(int y = 0; y < rows; y++) {
						if(tileGrid[x,y] == null) {
							c = x;
							r = y;
							break;
						}
					}

					if(tileGrid[c,r] == null) break;
				}
			}
		}


		Tile newTile = GameObject.Instantiate(tilePrefab, tileRoot);
		newTile.rectTransform.sizeDelta = tileSize;
		newTile.transform.localPosition = GetBoardPosition(c,r);
		newTile.row = r;
		newTile.col = c;
		tileGrid[c,r] = newTile;
		tiles.Add(newTile);
	}

	public void SlideUp() {
		foreach(Tile t in tiles.OrderByDescending(ti => ti.row)) {
			
		}

		SpawnTile();
	}

	public void SlideDown() {

		SpawnTile();
	}

	public void SlideLeft() {

		SpawnTile();
	}

	public void SlideRight() {

		SpawnTile();
	}

	#if UNITY_EDITOR

	[ContextMenu("Spawn Tile")]
	public void TestSpawn() {
		SpawnTile();
	}

	[ContextMenu("Redraw")]
	public void RedrawBoard() {
		foreach(Tile t in tiles) {
			Destroy(t.gameObject);
		}


		Awake();
		Start();
	}

	#endif
}
