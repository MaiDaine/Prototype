using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype
{
    public class TPArea : MonoBehaviour
    {
        public string sceneName;
        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}