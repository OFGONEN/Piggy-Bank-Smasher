/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FFStudio;
using TMPro;
using Sirenix.OdinInspector;

public class UIEconomyButton : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] SystemEconomy system_economy;
    [ SerializeField ] Currency notif_currency;

  [ Title( "Components" ) ]
    [ SerializeField ] Button _button;
    [ SerializeField ] TextMeshProUGUI _textRenderer;
#endregion

#region Properties
#endregion

#region Unity API
	private void OnDisable()
	{
		OnLevelFinished();
	}

	private void Start()
	{
		if( system_economy.IsMaxed )
		{
			_button.interactable = false;
			_textRenderer.text = "Maxed";
		}
		else
			_textRenderer.text = system_economy.UnlockCost.ToString();
	}
#endregion

#region API
    public void OnLevelStarted()
    {
        if( system_economy.IsMaxed )
		{
			_button.interactable = false;
			_textRenderer.text   = "Maxed";
		}
        else
        {
			OnCurrencyChange();
			notif_currency.Subscribe( OnCurrencyChange );
		}
	}

	public void OnLevelFinished()
	{
		notif_currency.Unsubscribe( OnCurrencyChange );
		_button.interactable = false;
	}

    public void UpgradeEconomy()
    {
		system_economy.Unlock();

        if( system_economy.IsMaxed )
        {
			_button.interactable = false;
			_textRenderer.text   = "Maxed";

			notif_currency.Unsubscribe( OnCurrencyChange );
		}
	}
#endregion

#region Implementation
    void OnCurrencyChange()
    {
		var unlockCost = system_economy.UnlockCost;

		_button.interactable = notif_currency.sharedValue >= unlockCost;
		_textRenderer.text   = unlockCost.ToString();
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}