using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CSharp;

public class Setting : SingletonMonoBehaviour<Setting>
{
    public static SaveData<float> masterVolume;
    
    private void Start()
    {
        masterVolume = new SaveData<float>("MasterVolume");
        LoadSetting();
    }

    public void SaveSetting()
    {

    }

    public void LoadSetting()
    {
        AudioListener.volume = masterVolume.Load(0.5f);
    }
}

/// <summary>
/// ジェネリック型はint,float,stringの何れかで宣言すること<br/>
/// インスタンス時にSaveDataの名前を入れる
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable] public class SaveData<T>
{
    private string keyName;

    public SaveData(string key)
    {
        keyName = key;
    }


    public void Save(T value)
    {
        if (typeof(T).Equals(typeof(int)))
        {
            PlayerPrefs.SetInt(keyName, (int)(object)value);
        }
        else if (typeof(T).Equals(typeof(float)))
        {
            PlayerPrefs.SetFloat(keyName, (float)(object)value);
        }
        else if (typeof(T).Equals(typeof(string)))
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }

        PlayerPrefs.Save();
    }

    /// <summary>
    /// 引数にはデータが無かった場合の初期値を記述
    /// </summary>
    /// <param name="initialValue"></param>
    /// <returns></returns>
    public dynamic Load(float initialValue)
    {
        if (typeof(T).Equals(typeof(int)))
        {
            return PlayerPrefs.GetInt(keyName, (int)initialValue);
            
        }
        else if (typeof(T).Equals(typeof(float)))
        {
            return PlayerPrefs.GetFloat(keyName, initialValue);

        }
        else if (typeof(T).Equals(typeof(string)))
        {
            return PlayerPrefs.GetString(keyName, initialValue.ToString());
        }
        return null;
    }
}