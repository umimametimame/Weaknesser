using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEditor_TitleScene : SceneEditor
{
    [SerializeField] private DifficultyLevel difficultyLevel;
    [SerializeField] private string gameSceneName;
    protected override void Start()
    {
        base.Start();
        SceneManager.sceneLoaded += IfGameSceneLoaded;
    }

    /// <summary>
    /// ëJà⁄êÊÇÃÉVÅ[ÉìñºÇ™GameSceneÇ»ÇÁ
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void IfGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameSceneName)
        {
            SceneEditor_GameScene gameScene = EditorGet<SceneEditor_GameScene>();
            gameScene.InitializeByBeforeScene(difficultyLevel);
            Debug.Log("Level");
        }
        Debug.Log("GameSceneLoaded");
    }

    protected override void Update()
    {
        base.Update();
    }
}