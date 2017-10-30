using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    [SerializeField]
    GameObject tile;
    [SerializeField]
    Camera cam;
    Vector2 screenSize;
    public Dictionary<Vector2, Tile> tileTable = new Dictionary<Vector2, Tile>();
    int bombCount = 20;
    Vector2 mousePos;

    // Use this for initialization
    void Start()
    {
        screenSize = cam.ViewportToWorldPoint(new Vector2(1, 1));
        screenSize.x = (int)screenSize.x;
        screenSize.y = (int)screenSize.y;
        GenerateTiles();
        SetTiles();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        mousePos.x = (int)mousePos.x;
        mousePos.y = (int)mousePos.y;
        if(Input.GetMouseButtonUp(0))
        {
            tileTable[mousePos].Click();
        }
        if (Input.GetMouseButtonUp(1))
        {
            tileTable[mousePos].Click(true);
        }
    }

    private void GenerateTiles()
    {
        for (int x = 0; x < screenSize.x; x++)
        {
            for (int y = 0; y < screenSize.y; y++)
            {
                tileTable.Add(new Vector2(x, y), Instantiate(tile, new Vector2(x, y), Quaternion.identity, transform).GetComponent<Tile>());
            }
        }
    }

    private void SetTiles()
    {
        Vector2 randPos;
        for (int i = 0; i < bombCount; i++)
        {
            do
            {
                randPos = new Vector2((int)Random.Range(0, screenSize.x), (int)Random.Range(0, screenSize.y));
            }
            while (tileTable[randPos].Type == Tile.TileType.Bomb);

            tileTable[randPos].Type = Tile.TileType.Bomb;
        }

        Vector2 pos;
        for (int x = 0; x < screenSize.x; x++)
        {
            for (int y = 0; y < screenSize.y; y++)
            {
                pos = new Vector2(x, y);
                if (tileTable[pos].Type != Tile.TileType.Bomb)
                    tileTable[pos].Type = Tile.TileType.Number;
            }
        }
    }
}
