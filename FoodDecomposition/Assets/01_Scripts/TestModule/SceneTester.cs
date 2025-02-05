using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM.Test
{
    public class SceneTester : MonoBehaviour
    {
        public string sceneName;

        [ContextMenu("SceneChange")]
        public void SceneChange()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
