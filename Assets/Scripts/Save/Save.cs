using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

public class Save : MonoBehaviour
{
    private static Save _instance;
    
    private static string _saveFile;
    private static FileStream _dataStream;
    private static BinaryFormatter _converter = new();
    private static GameData _gameData;

    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if(this != _instance) {
            Destroy(this.gameObject);
        }
        
        DataController.SetDefaultValues(_gameData);
        
    }

    public static GameData LoadData()
    {
        _saveFile = Application.persistentDataPath + "/gamedata.data";
        var gameData = new GameData();
        
        if (File.Exists(_saveFile))
        {
            _dataStream = new FileStream(_saveFile, FileMode.Open);
            gameData = _converter.Deserialize(_dataStream) as GameData;
            _dataStream.Close();
        }
        else
        {
            _gameData = new GameData();
            SaveData();
        }

        return gameData;
    }

    private static void SaveData()
    {
        _dataStream = new FileStream(_saveFile, FileMode.Create);
        _converter.Serialize(_dataStream, _gameData);
        _dataStream.Close();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        SaveData();
    }

    private void OnApplicationFocus(bool focus)
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
