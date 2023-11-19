using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.UIElements;
using My;
using static World;

public class SceneEditor_GameScene : SceneEditor
{
    public static MagnificationPerDifficulty globalPerDif { get; private set; }
    [SerializeField] private MagnificationPerDifficulty perDifficulty;
    public static DifficultyLevel globalDif { get; private set; }   // static�ϐ��͂ǂ��������Ă��C���X�y�N�^�[�ɕ\���ł��Ȃ�
    [SerializeField] private DifficultyLevel difficultyLevel;// ���̌����鉻
    private void Awake()
    {
        if (debugMode == true)
        {
            globalDif = difficultyLevel;
        }
        perDifficulty.currentDifficulty = globalDif;
        globalPerDif = perDifficulty;
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    public void InitializeByBeforeScene(DifficultyLevel level)
    {
        globalDif = difficultyLevel = level;

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneEditor_GameScene))]
public class SceneEditor_GameSceneEditor : MyEditor<SceneEditor_GameScene>
{
    string difficultyLevel = nameof(difficultyLevel);
    SerializedProperty difLevelPro;
    private void OnEnable()
    {
        Initialize();
        serializedObjectUpdate += SO_Sequence;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    void DisableChangeDifficulty()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(difLevelPro);
        EditorGUI.EndDisabledGroup();
    }
    void SO_Sequence()
    {

        DrawPropertiesExcluding(serializedObject, difficultyLevel);   // ��Ɍ��̕ϐ������O���ăC���X�y�N�^�[�ɕ\��

        difLevelPro = serializedObject.FindProperty(difficultyLevel);
        if (tg.debugMode == true)
        {
            if (EditorApplication.isPlaying == true) { DisableChangeDifficulty(); }
            else { EditorGUILayout.PropertyField(difLevelPro); }
        }
        else { DisableChangeDifficulty(); }
    }
}
#endif