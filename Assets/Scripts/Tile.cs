using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField]
    Sprite tileSprite;
    [SerializeField]
    Sprite bombSprite;
    [SerializeField]
    Sprite flagSprite;
    [SerializeField]
    Sprite safeSprite;
    [SerializeField]
    Sprite oneSprite;
    [SerializeField]
    Sprite twoSprite;
    [SerializeField]
    Sprite threeSprite;
    [SerializeField]
    Sprite fourSprite;
    [SerializeField]
    Sprite fiveSprite;
    [SerializeField]
    Sprite sixSprite;
    [SerializeField]
    Sprite sevenSprite;
    [SerializeField]
    Sprite eightSprite;

    TileManager manager;

    bool isFlag = false;
    bool clicked = false;

    public enum TileType
    {
        None,
        Number,
        Bomb,
    };

    private TileType type = TileType.None;

    public TileType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public void Click(bool rightClick = false)
    {
        if (clicked)
            return;


        if (isFlag && !rightClick)
            return;

        if (isFlag && rightClick)
        {
            transform.GetComponent<SpriteRenderer>().sprite = tileSprite;
            isFlag = false;
            return;
        }

        if (!isFlag && rightClick)
        {
            transform.GetComponent<SpriteRenderer>().sprite = flagSprite;
            isFlag = true;
            return;
        }

        clicked = true;

        if (manager == null)
            manager = GetComponentInParent<TileManager>();

        if (type == TileType.Bomb)
        {
            transform.GetComponent<SpriteRenderer>().sprite = bombSprite;
        }

        if (type == TileType.Number)
        {
            int surroundingBombs = 0;

            Vector2 posCheck;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    posCheck = new Vector2(transform.position.x + x, transform.position.y + y);

                    if (manager.tileTable.ContainsKey(posCheck))
                    {
                        if (manager.tileTable[posCheck].Type == TileType.Bomb)
                        {
                            surroundingBombs++;
                        }
                    }
                }
            }
            switch (surroundingBombs)
            {
                case 0:
                    transform.GetComponent<SpriteRenderer>().sprite = safeSprite;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0)
                                continue;

                            posCheck = new Vector2(transform.position.x + x, transform.position.y + y);

                            if (manager.tileTable.ContainsKey(posCheck))
                            {
                                manager.tileTable[posCheck].Click();
                            }
                        }
                    }
                    break;
                case 1:
                    transform.GetComponent<SpriteRenderer>().sprite = oneSprite;
                    break;
                case 2:
                    transform.GetComponent<SpriteRenderer>().sprite = twoSprite;
                    break;
                case 3:
                    transform.GetComponent<SpriteRenderer>().sprite = threeSprite;
                    break;
                case 4:
                    transform.GetComponent<SpriteRenderer>().sprite = fourSprite;
                    break;
                case 5:
                    transform.GetComponent<SpriteRenderer>().sprite = fiveSprite;
                    break;
                case 6:
                    transform.GetComponent<SpriteRenderer>().sprite = sixSprite;
                    break;
                case 7:
                    transform.GetComponent<SpriteRenderer>().sprite = sevenSprite;
                    break;
                case 8:
                    transform.GetComponent<SpriteRenderer>().sprite = eightSprite;
                    break;
                default:
                    break;
            }

        }
    }
}
