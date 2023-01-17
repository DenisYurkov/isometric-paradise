using UnityEngine;

namespace CodeBase.Utility
{
    public class ApplicationHelper : MonoBehaviour
    {
        public RuntimePlatform GetPlatform() =>
            Application.platform;
    
        public void Quit() => 
            Application.Quit();
    }
}