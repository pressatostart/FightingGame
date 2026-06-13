using UnityEngine;
using SakugaEngine.Resources;
using UnityEngine.UI;

namespace SakugaEngine.UI
{
    public partial class ColorSelectMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuElement;
        //[SerializeField] private ScrollContainer scroll;
        [SerializeField] private Transform contents;
        private ColorSelectionElement[] elements;

        public void Activate(FighterElement fighter)
        {
            gameObject.SetActive(true);
            elements = new ColorSelectionElement[fighter.ColorPalettes.Length];
            for (int i = 0; i < fighter.ColorPalettes.Length; i++)
            {
                ColorSelectionElement element = Instantiate(menuElement, contents).GetComponent<ColorSelectionElement>();
                element.Set($"{i + 1}: {fighter.ColorPalettes[i].PaletteName}");
                elements[i] = element;
            }
            SelectElement(0);
            //scroll.GetVScrollBar().Scale = Vector2.zero;
        }

        public void Deactivate()
        {
            if (contents.childCount > 0)
            {
                for (int i = 0; i < contents.childCount; i++)
                {
                    Destroy(contents.GetChild(i).gameObject);
                }
            }
            elements = new ColorSelectionElement[0];
            gameObject.SetActive(false);
        }

        public void SelectElement(int index)
        {
            foreach (ColorSelectionElement element in elements)
            {
                element.Deselect();
            }
            elements[index].Select();
            //scroll.EnsureControlVisible(elements[index]);
        }
    }
}
