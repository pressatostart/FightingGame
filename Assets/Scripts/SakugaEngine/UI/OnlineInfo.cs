using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SakugaEngine.Resources;
using Unity.VisualScripting;

namespace SakugaEngine.UI
{
    public partial class OnlineInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI P1Name;
        [SerializeField] private TextMeshProUGUI P2Name;
        [SerializeField] private Image P1Platform;
        [SerializeField] private Image P2Platform;

        public void Set(MatchSettings match)
        {
            P1Name.text = match.OnlineP1Name;
            P2Name.text = match.OnlineP2Name;
            if (match.OnlineP1PlatformIcon != null)
                P1Platform.sprite = match.OnlineP1PlatformIcon;
            if (match.OnlineP2PlatformIcon != null)
                P2Platform.sprite = match.OnlineP2PlatformIcon;
        }
    }
}
