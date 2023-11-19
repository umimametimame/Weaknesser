using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �e�V�[���Ɉ�̂ݔz�u<br/>
/// �V�[�����ׂ��ƍ폜
/// </summary>
public class SceneOperator : MonoBehaviour
{
    [field: SerializeField] public bool debugMode { get; private set; }
    [SerializeField] private Instancer canvas;
    [SerializeField] private FadeInstancer fadeObj;

    private void Awake()
    {

        canvas.Initialize();
        canvas.Instance();
    }
    protected virtual void Start()
    {
        SceneCheck();
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneCheck();
    }

    protected virtual void Update()
    {
        canvas.Update();
    }


    /// <summary>
    /// ���݂̃V�[�����擾���AScenes In Build�Ɋ܂܂�Ă��Ȃ���΃f�o�b�O���[�h�Ɉڍs����
    /// </summary>
    private void SceneCheck()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (IsSceneInBuildSettings(currentSceneName) == false)
        {
            InDebugMode();
        }
    }

    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneName == name)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ���b�Z�[�W��\�����A�f�o�b�O���[�h�Ɉڍs����
    /// </summary>
    private void InDebugMode()
    {
        if (debugMode == false)
        {


            debugMode = true;

            Debug.LogWarning("��O�V�[��");
        }
    }

    public T OperatorGet<T>()
    {
        return GameObject.FindWithTag(Tags.SceneOperator).GetComponent<T>();
    }
}

public static class Tags
{
    public static string SceneOperator = nameof(SceneOperator);
    public static string Player01 = nameof(Player01);
    public static string Player02 = nameof(Player02);
}

public static class Anims
{
    public static string AnimIdx = nameof(AnimIdx);
    public static string attack1 = nameof(attack1);
    public static string attack2 = nameof(attack2);
    public static string damege = nameof(damege);
    public static string die = nameof(die);
}