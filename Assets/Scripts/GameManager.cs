using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startPanel;
    public GameObject ingamePanel;
    public Button startButton;
    
    private Car carController;
    
    void Start()
    {
        carController = player.GetComponent<Car>();
        
        carController.enabled = false;
        ingamePanel.SetActive(false);
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        // 게임 시작 시 플레이어 움직임을 활성화합니다.
        carController.enabled = true;
        // 시작 버튼을 비활성화합니다.
        startPanel.SetActive(false);
        ingamePanel.SetActive(true);
        // startButton.gameObject.SetActive(false);
    }
}
