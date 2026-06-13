using UnityEngine;
using TMPro;

namespace SakugaEngine.UI
{
	public partial class HitNotificationWidget : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI NotifLabel;

		public void View(HitNotificationPiece element)
		{
			gameObject.SetActive(element.Active);
			NotifLabel.text = element.NotifName;
		}
	}
}
