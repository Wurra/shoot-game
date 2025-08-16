using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Button btnAgain;
    public Button btnBack;
    public override void Init()
    {
        btnAgain.onClick.AddListener(() =>
        {
            MyplayerScore.ResetScore();
            RestartLevel();
            UIManager.Instance.HidePanel<GameOverPanel>();
        });
        btnBack.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BeginScene");
            UIManager.Instance.HidePanel<GameOverPanel>();
        });
    }
    public void RestartLevel()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
