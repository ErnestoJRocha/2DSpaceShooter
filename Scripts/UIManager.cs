using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

  [SerializeField]  private Text _scoreText;
  [SerializeField] private Text _gameOver;
  [SerializeField] private Text _rKey;

  [SerializeField] private   Sprite[] _lives;

  [SerializeField]  private Image _livesImg;

   private GameManager _gameManager;

    void Start()
    {
       
        _scoreText.text = "Score: " + 0;
         _gameOver.gameObject.SetActive(false);
        _rKey.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }
  
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString(); 
       
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _lives[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.gameOver();
        _gameOver.gameObject.SetActive(true);
        _rKey.gameObject.SetActive(true);
        StartCoroutine(RKey());
    }

    IEnumerator RKey()
    {
        while(true)
        {
            _rKey.text = "Press the R key to restart the level";
            yield return new WaitForSeconds(0.15f);
            _rKey.text = "";
            yield return new WaitForSeconds(0.15f);
        }
        
    }



}
