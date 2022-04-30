using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;


public class MenuUIHandlers : MonoBehaviour
{
    // Start is called before the first frame update
    public Text FirstRankPlayer;
    private static int BestScore;
    private static string BestPlayer;

    [SerializeField] Text PlayerNameInput;

    private void Awake()
    {
        LoadGameRank();
    }
    private void Start()
    {
        FirstRankPlayer.text = "#1 - " + BestScore + " - " + BestPlayer;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPlayerName()
    {
       DataManager.Instance.PlayerName = PlayerNameInput.text;
        Debug.Log("Data manager Player name: " + DataManager.Instance.PlayerName + "player Input name: " + PlayerNameInput.text);
    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayer = data.TheBestPlayer;
            BestScore = data.HighiestScore;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int HighiestScore;
        public string TheBestPlayer;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
