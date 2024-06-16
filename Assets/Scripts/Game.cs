using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject ChessPiece;

    private bool gameOver = false;
    private string currentPlayer = "white";

    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[,] positionBoard = new GameObject[8, 8];


    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("whiteRook",0,0), Create("whiteKnigth",1,0), Create("whiteBishop",2,0), Create("whiteQueen",3,0),
            Create("whiteKing",4,0), Create("whiteBishop",5,0), Create("whiteKnigth",6,0), Create("whiteRook",7,0),
            Create("whitePawn",0,1), Create("whitePawn",1,1), Create("whitePawn",2,1), Create("whitePawn",3,1),
            Create("whitePawn",4,1), Create("whitePawn",5,1), Create("whitePawn",6,1), Create("whitePawn",7,1)
        };

        playerBlack = new GameObject[]
        {
            Create("blackRook",0,7), Create("blackKnigth",1,7), Create("blackBishop",2,7), Create("blackQueen",3,7),
            Create("blackKing",4,7), Create("blackBishop",5,7), Create("blackKnigth",6,7), Create("blackRook",7,7),
            Create("blackPawn",0,6), Create("blackPawn",1,6), Create("blackPawn",2,6), Create("blackPawn",3,6),
            Create("blackPawn",4,6), Create("blackPawn",5,6), Create("blackPawn",6,6), Create("blackPawn",7,6)
        };

        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        };
    }


    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(ChessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessMan chessMan = obj.GetComponent<ChessMan>();

        chessMan.name = name;
        chessMan.SetPositionBoard(x, y);
        chessMan.Activate();

        return obj;
    }

    public void SetPosition(GameObject gameObject)
    {
        ChessMan chessMan = gameObject.GetComponent<ChessMan>();

        positionBoard[chessMan.GetXBoard(), chessMan.GetYBoard()] = gameObject;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positionBoard[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positionBoard[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positionBoard.GetLength(0) || y >= positionBoard.GetLength(1))
            return false;

        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {

        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TMP_Text>().enabled = true;
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<TMP_Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TMP_Text>().text = playerWinner + " wins!";
    }
}
