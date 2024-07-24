using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/SceneLoader")]
    public class SceneLoaderEffect : EcsEffect
    {
        [SerializeField] private bool loadAsync;
        
        public void LoadScene(string sceneName)
        {
            if (loadAsync) SceneManager.LoadSceneAsync(sceneName);
            else SceneManager.LoadScene(sceneName);
        }
    }
}