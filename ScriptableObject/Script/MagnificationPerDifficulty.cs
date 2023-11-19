using UnityEngine;
using System;
using My;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "ScriptableObject", fileName = "StrengthByDifficulty")]
public class MagnificationPerDifficulty : ScriptableObject
{
    [field: SerializeField, NonEditable] public DifficultyLevel currentDifficulty { get; set; }
    public float[] parameter;
    public float[] score;

    public void AdjustParameter(Parameter[] param)
    {
        for(int i =0; i < param.Length; ++i)
        {
            param[i].basic *= parameter[index];
        }
    }

    public float adjustScore
    {
        get
        {
            return score[index];
        }
    }

    public int index
    {
        get
        {
            return (int)currentDifficulty;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MagnificationPerDifficulty))]
public class MagnificationPerDifficultyEditor : MyEditor<MagnificationPerDifficulty>
{
    //string parameter = nameof(parameter);
    //SerializedProperty parameterPro;
    //string score = nameof(score);
    //SerializedProperty scorePro;

    string currentDifficulty = nameof(currentDifficulty);
    SerializedProperty dif;
    private void OnEnable()
    {
        Initialize();
    }
    public override void OnInspectorGUI()
    {

        int listSize = Enum.GetValues(typeof(DifficultyLevel)).Length;
        EditorGUILayout.LabelField("Difficulty Type =", listSize.ToString());

        Array.Resize(ref tg.parameter, listSize);
        Array.Resize(ref tg.score, listSize);
        DrawDefaultInspector();
        base.OnInspectorGUI();
    }

}
#endif