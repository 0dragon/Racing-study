using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject startPanel;
    public GameObject ingamePanel;
    public GameObject gameOverPanel;
    public Button startButton;
    
    public GameObject gasItemPrefab; // 가스 아이템 프리팹
    public float spawnInterval = 5.0f; // 가스 아이템 스폰 간격
    public float spawnRangeX = 4.0f; // 도로 위에서의 스폰 범위 X
    public float spawnRangeZ = 30.0f;
    
    private Car carController;
    
    void Start()
    {
        carController = player.GetComponent<Car>();
        
        carController.enabled = false;
        ingamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        // 게임 시작 시 플레이어 움직임을 활성화합니다.
        carController.enabled = true;
        // 시작 버튼을 비활성화합니다.
        startPanel.SetActive(false);
        ingamePanel.SetActive(true);
        
        StartCoroutine(SpawnGasItems());
    }

    public void GameOver()
    {
        carController.enabled = false;
        ingamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    
    private IEnumerator SpawnGasItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnGasItem();
        }
    }

    private void SpawnGasItem()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = player.transform.position.z + spawnRangeZ;
        Vector3 spawnPosition = new Vector3(spawnPosX, 1.0f, spawnPosZ);
        Instantiate(gasItemPrefab, spawnPosition, Quaternion.identity);
    }
}
