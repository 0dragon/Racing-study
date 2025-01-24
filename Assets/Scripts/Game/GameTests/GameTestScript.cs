using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameTestScript
{
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
        
        // Start 버튼 클릭
        startButton.GetComponent<UnityEngine.UI.Button>.onClick.Invoke();
        
        // 반복
        while(gameManager.GameState == )
        
        yield return null;
    }

    private IEnumerator waitForSceneLoad()
    {
        while (SceneManager.GetActiveScene().buildIndex > 0)
        {
            yield return null;
        }
    }
}
