using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject panel;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Car>().enabled = false;
        
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        // 게임 시작 시 플레이어 움직임을 활성화합니다.
        player.GetComponent<Car>().enabled = true;
        // 시작 버튼을 비활성화합니다.
        panel.SetActive(false);
        // startButton.gameObject.SetActive(false);
    }
}
