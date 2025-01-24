using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 10.0f;
    
    public float maxGas = 100.0f;
    public float gasConsumptionRate = 0.5f; // 이동 거리 당 가스 소모량
    public float scoreIncrementDistance = 10.0f; // 점수 증가량
    private float currentGas;
    public TMP_Text gasText;
    public TMP_Text scoreText;
    
    private Vector3 lastPosition;
    public float horizontalInput;
    private float score = 0;
    private float totalDistanceMoved = 0;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentGas = maxGas;
        lastPosition = transform.position;
        UpdateGasText();
        UpdateScoreText();
    }
    
    void Update()
    {
        if (currentGas > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Translate(Vector3.right * horizontalInput * turnSpeed * Time.deltaTime);
            
            float distanceMoved = Vector3.Distance(transform.position, lastPosition);
            currentGas -= distanceMoved * gasConsumptionRate;
            totalDistanceMoved += distanceMoved;
            lastPosition = transform.position;

            UpdateGasText();
            
            if (totalDistanceMoved >= scoreIncrementDistance)
            {
                score++;
                totalDistanceMoved -= scoreIncrementDistance;
                UpdateScoreText();
            }
            
            if (currentGas <= 0)
            {
                gameManager.GameOver();
            }
        }
    }

    void UpdateGasText()
    {
        gasText.text = "Gas: " + Mathf.Max(currentGas, 0).ToString("F2");
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString("F2");
    }
    
    public void AddGas(float amount)
    {
        currentGas = Mathf.Min(currentGas + amount, maxGas); // 최대 가스를 초과하지 않도록 설정
        UpdateGasText();
    }

    public void SetHorizontalInput(float input)
    {
        horizontalInput = input;
    }

    public void OnPointerDownLeft()
    {
        SetHorizontalInput(-1);
    }

    public void OnPointerDownRight()
    {
        SetHorizontalInput(1);
    }

    public void OnPointerUp()
    {
        SetHorizontalInput(0);
    }
}
