using Sirenix.OdinInspector;
using UnityEngine;

namespace Deslab.Level
{
    public class LevelSettings : MonoBehaviour
    {
        public Transform startLevelPoint;
        public Transform endLevelPoint;
        public Transform leftBorder;
        public Transform rightBorder;
        public int levelID;
        //public Transform flyStartPoint;

        //public bool applyColorScheme = false;

        //[ShowIf("applyColorScheme")]
        [HideInInspector]
        public ColorSchemeObject colorScheme;

        //private void Start()
        //{
        //    if (applyColorScheme)
        //        colorScheme.ApplyScheme();
        //}

        private void Update()
        {
            //if (StaticManager.instance.debugMode)
            //{
            //    if (Input.GetKeyDown(KeyCode.Space))
            //        colorScheme.ApplyScheme();
            //}
        }
    }
}
