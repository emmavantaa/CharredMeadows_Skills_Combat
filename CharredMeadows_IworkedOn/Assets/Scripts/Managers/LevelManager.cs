using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [System.Serializable]
    public class LevelData
    {
        
        public string levelName;
        public SceneReference SceneRef;
       

    }

    public SceneReference BattleSceneRef;
    public Transform player;

    private Dictionary<int, Vector3> startingPosition = new Dictionary<int, Vector3>();

    [SerializeField] private string loadSceneWithName;



    [BoxGroup("TestingGround")]
    [SerializeField] private LevelData Test;
    [ReorderableList]
    [SerializeField] private LevelData[] Levels;
    private GameObject worldReference;
    private EnemyController enemyControllerRef;
    private Vector3 enemyPosition;

    public GameObject WorldRoot
    {
        get
        {
            return worldReference;
        }
        set
        {
            worldReference = value;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(loadSceneWithName);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public void LoadLevel(string name)
    {
        foreach(LevelData data  in Levels)
        {
            if (data.levelName.Equals(name))
            {
                SceneManager.LoadScene(data.SceneRef, LoadSceneMode.Single);
                return;
            }
        } 
    }

   public  void  OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.path == Levels[2].SceneRef.ScenePath)
        {
            BattleManager.Instance.InitializeBattle();
        }
    }

    public void BattleScene(string name)
    {
      
        WorldRoot.SetActive(false);
        SceneManager.LoadScene(BattleSceneRef, LoadSceneMode.Additive);

    }

    public void OnLevelWasLoaded(int level)
    {
        if (startingPosition.ContainsKey(level)) player.position = startingPosition[level];
    }

}
