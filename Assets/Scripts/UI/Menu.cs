using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text Title;
    public Text Win;
    public Text Lose;
    public Text Summary;
    
    public enum MenuType
    {
        Start,
        Lose,
        Win,
    }
    
    public void Init(MenuType type)
    {
        Title.gameObject.SetActive(type == MenuType.Start);
        Win.gameObject.SetActive(type == MenuType.Win);
        Lose.gameObject.SetActive(type == MenuType.Lose);
        
        Summary.gameObject.SetActive(type != MenuType.Start);
        Summary.text = string.Format("You destroyed {0}/{1} enemies!",
            GameManager.Instance.DestroyedEnemies,
            GameManager.Instance.Config.EnemiesToWin);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void DeInit()
    {
        
    }
}
