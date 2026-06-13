using UnityEngine;
using SakugaEngine.Global;

namespace SakugaEngine.UI
{
    public partial class InputHistory : MonoBehaviour
    {
        [SerializeField] private InputHistoryElement[] elements;

        private int ElementsAmount => elements.Length;

        public void SetHistoryList(InputManager manager)
        {
            elements[0].SetHistory(manager.InputHistory[manager.CurrentHistory]);

            int el = 0;

            for (int e = ElementsAmount - 1; e >= 1; e--)
            {
                el++;
                int element = el + manager.CurrentHistory;
                elements[e].gameObject.SetActive(manager.InputHistory[element % GlobalVariables.InputHistorySize].duration != 0);

                elements[e].SetHistory(manager.InputHistory[element % GlobalVariables.InputHistorySize]);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < ElementsAmount; i++)
            {
                elements[i].gameObject.SetActive(false);
            }
        }
    }
}
