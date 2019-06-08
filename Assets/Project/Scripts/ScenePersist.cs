using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    int startSceneIndex;

    private void Awake()
    {
        int numberOfScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numberOfScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        startSceneIndex = FindObjectOfType<LevelLoader>().GetSceneIndex();
    }

    private void Update()
    {
        int currentSceneIndex = FindObjectOfType<LevelLoader>().GetSceneIndex();
        if (startSceneIndex != currentSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
