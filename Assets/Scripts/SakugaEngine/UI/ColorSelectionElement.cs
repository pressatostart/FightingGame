using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SakugaEngine.UI
{
    public partial class ColorSelectionElement : MonoBehaviour
    {
        [SerializeField] private Image Background;
        [SerializeField] private TextMeshProUGUI ColorName;
        [SerializeField] private Color DefaultColor;
        [SerializeField] private Color SelectedColor;

        public void Set(string colorName)
        {
            ColorName.text = colorName;
            Background.color = DefaultColor;
        }

        public void Select()
        {
            Background.color = SelectedColor;
        }

        public void Deselect()
        {
            Background.color = DefaultColor;
        }
    }
}
