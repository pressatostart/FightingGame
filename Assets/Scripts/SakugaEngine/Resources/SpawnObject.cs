using System;
using UnityEngine;

namespace SakugaEngine.Resources
{
    [Serializable]
    public class SpawnObject
    {
        public string Key;
        public GameObject Instance;
        public int PoolSize;
    }
}