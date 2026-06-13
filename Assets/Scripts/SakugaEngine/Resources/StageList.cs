using UnityEngine;

namespace SakugaEngine.Resources
{
    [CreateAssetMenu(fileName = "New Stage List", menuName = "Sakuga Engine/Stage List", order = 0)]
    public partial class StageList : ScriptableObject
    {
        public StageElement[] elements;
    }
}
