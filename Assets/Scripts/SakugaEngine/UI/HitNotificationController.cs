using UnityEngine;
using SakugaEngine.Global;
using System;
using Unity.VisualScripting;

namespace SakugaEngine.UI
{
	public partial class HitNotificationController : MonoBehaviour
	{
		[SerializeField] private HitNotificationWidget[] Widgets;

		public void View(HitNotificationPiece[] elements)
		{
			for (int i = 0; i < Widgets.Length; i++)
			{
				Widgets[i].View(elements[i]);
			}
		}
	}
}
