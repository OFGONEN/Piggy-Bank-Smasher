/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FFStudio;

public class UICurrenyText : MonoBehaviour
{
#region Fields
    [ SerializeField ] Currency notif_currency;
    [ SerializeField ] TextMeshProUGUI _textRenderer;
    [ SerializeField ] float maxValue;
#endregion

#region Properties
#endregion

#region Unity API
    private void OnEnable()
    {
		notif_currency.Subscribe( OnCurrencyChanged );
		OnCurrencyChanged();
	}

    private void OnDisable()
    {
		notif_currency.Unsubscribe( OnCurrencyChanged );
	}
#endregion

#region API
#endregion

#region Implementation
    void OnCurrencyChanged()
    {
        if( notif_currency.sharedValue >= maxValue )
			_textRenderer.text = MathExtensions.FormatBigNumberAANotation( notif_currency.sharedValue );
        else
			_textRenderer.text = Mathf.RoundToInt( notif_currency.sharedValue ).ToString();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}