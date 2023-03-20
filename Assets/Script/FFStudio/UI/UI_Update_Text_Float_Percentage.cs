/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
    public class UI_Update_Text_Float_Percentage : UI_Update_Text< SharedFloatNotifier, float >
    {
		protected override void OnSharedDataChange()
		{
			ui_Text.text = Mathf.RoundToInt( sharedDataNotifier.SharedValue * 100 ).ToString() + "%";
		}
    }
}
