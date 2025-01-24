using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameTestScript
{
    private Car _car;
    private GameObject _leftMoveButton;
    private GameObject _rightMoveButton;
    
    // A Test behaves as an ordinary method
    [Test]
    public void GameTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        // 씬 로드 테스트 
        SceneManager.LoadScene("Scenes/Stage", LoadSceneMode.Single);
        yield return waitForSceneLoad();

        // 필수 오브젝트 확인(ex. 게임 매니저 등)
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Assert.IsNotNull(gameManager, "GameManager is Null");

        var startButton = GameObject.Find("Btn_Start");
        Assert.IsNotNull(startButton, "StartButton is null");
        
        // 게임 실행
        startButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        
        // 플레이어 차 확인
        _car = GameObject.Find("Car").GetComponent<Car>();
        Assert.IsNotNull(_car, "Car is null");
        
        // 게임 제어 관련 버튼 클릭
        _leftMoveButton = GameObject.Find("Btn_Left");
        Assert.IsNotNull(_leftMoveButton, "LeftButton is null");
        _rightMoveButton = GameObject.Find("Btn_Right");
        Assert.IsNotNull(_rightMoveButton, "RightButton is null");
        
        // 가스의 등장 위치 파악
        Vector3 leftPosition = new Vector3(-5f, 0.2f, 0f);
        Vector3 rightPosition = new Vector3(5f, 0.2f, 0f);
        Vector3 centerPosition = new Vector3(0, 0.2f, 0f);

        float rayDistance = 20f;
        Vector3 rayDirection = Vector3.forward;

        var InGamePanel = GameObject.Find("InGamePanel");
        
        // 반복
        while (InGamePanel.activeSelf)
        {
            RaycastHit hit;
            if (Physics.Raycast(leftPosition, rayDirection, out hit, rayDistance, LayerMask.GetMask("Gas")))
            {
                Debug.Log("left" + hit.point);
                MoveCar(hit.point);
            }
            else if (Physics.Raycast(rightPosition, rayDirection, out hit, rayDistance,
                         LayerMask.GetMask("Gas")))
            {
                Debug.Log("right" + hit.point);
                MoveCar(hit.point);
            }
            else if (Physics.Raycast(centerPosition, rayDirection, out hit, rayDistance, LayerMask.GetMask("Gas")))
            {
                Debug.Log("center" + hit.point);
                MoveCar(hit.point);
            }
            else
            {
                Debug.Log("none" + hit.point);
                MoveButtonUp(_leftMoveButton);
                MoveButtonUp(_rightMoveButton);
            }
            
            Debug.DrawRay(leftPosition, rayDirection * rayDistance, Color.red);
            Debug.DrawRay(rightPosition, rayDirection * rayDistance, Color.blue);
            Debug.DrawRay(centerPosition, rayDirection * rayDistance, Color.green);
            
            yield return null;
        }

    yield return null;
    }

    private IEnumerator waitForSceneLoad()
    {
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }
    }

    private void MoveButtonDown(GameObject moveButton)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(moveButton.gameObject, pointerEventData, ExecuteEvents.pointerDownHandler);
    }
    
    private void MoveButtonUp(GameObject moveButton)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(moveButton.gameObject, pointerEventData, ExecuteEvents.pointerUpHandler);
    }

    private void MoveCar(Vector3 targetPosition)
    {
        if (Mathf.Abs(targetPosition.x - _car.transform.position.x) < 0.01f)
        {
            MoveButtonUp(_leftMoveButton);
            MoveButtonUp(_rightMoveButton);
            return;
        }
        
        if (targetPosition.x < _car.transform.position.x)
        {
            // 왼쪽으로 이동
            MoveButtonDown(_leftMoveButton);
            MoveButtonUp(_rightMoveButton);
        }
        else if (targetPosition.x > _car.transform.position.x)
        {
            // 오른쪽으로 이동
            MoveButtonDown(_rightMoveButton);
            MoveButtonUp(_leftMoveButton);
        }
        Debug.Log("Car position after move: " + _car.transform.position);
    }
}
