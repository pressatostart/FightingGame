using UnityEngine;
using SakugaEngine.Global;
using TMPro;
using UnityEngine.UI;

namespace SakugaEngine.UI
{
    public class InputHistoryElement : MonoBehaviour
    {
        public Image directional;
        public Image button_A;
        public Image button_B;
        public Image button_C;
        public Image button_D;
        public TextMeshProUGUI duration;
        //public bool set;

        [SerializeField] private Sprite[] Frames;
        [SerializeField] private int A_Standard;
        [SerializeField] private int B_Standard;
        [SerializeField] private int C_Standard;
        [SerializeField] private int D_Standard;

        public void SetHistory(InputRegistry reg)
        {
            int h;
            int v;
            int dir = 0;

            if ((reg.rawInput & PlayerInputs.RIGHT) != 0)
                h = 1;
            else if ((reg.rawInput & PlayerInputs.LEFT) != 0)
                h = -1;
            else
                h = 0;

            if ((reg.rawInput & PlayerInputs.DOWN) != 0)
                v = -1;
            else if ((reg.rawInput & PlayerInputs.UP) != 0)
                v = 1;
            else
                v = 0;
            
            if (h < 0 && v < 0) dir = 0;
            else if (h == 0 && v < 0) dir = 1;
            else if (h > 0 && v < 0) dir = 2;
            else if (h < 0 && v == 0) dir = 3;
            else if (h == 0 && v == 0) dir = 4;
            else if (h > 0 && v == 0) dir = 5;
            else if (h < 0 && v > 0) dir = 6;
            else if (h == 0 && v > 0) dir = 7;
            else if (h > 0 && v > 0) dir = 8;
                
            bool a = (reg.rawInput & PlayerInputs.FACE_A) != 0;
            bool b = (reg.rawInput & PlayerInputs.FACE_B) != 0;
            bool c = (reg.rawInput & PlayerInputs.FACE_C) != 0;
            bool d = (reg.rawInput & PlayerInputs.FACE_D) != 0;

            directional.sprite = Frames[dir];
                
            button_A.sprite = a ? Frames[A_Standard + 1] : Frames[A_Standard];
            button_B.sprite = b ? Frames[B_Standard + 1] : Frames[B_Standard];
            button_C.sprite = c ? Frames[C_Standard + 1] : Frames[C_Standard];
            button_D.sprite = d ? Frames[D_Standard + 1] : Frames[D_Standard];

            duration.text = reg.duration.ToString();
        }

        public void TransferFrom(InputHistoryElement other)
        {
            directional.sprite = other.directional.sprite;
                
            button_A.sprite = other.button_A.sprite;
            button_B.sprite = other.button_B.sprite;
            button_C.sprite = other.button_C.sprite;
            button_D.sprite = other.button_D.sprite;

            duration.text = other.duration.text;
        }
    }
}
