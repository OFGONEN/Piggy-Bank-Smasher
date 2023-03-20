/* Created by and for usage of FF Studios (2023). */

using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ScrollTexture : MonoBehaviour
{
#region Fields
    [ SerializeField ] Material material;
    [ SerializeField ] string property_name = "_BaseMap";
    [ SerializeField ] Vector2 initial_value;
    [ SerializeField ] Vector2 target_value;
    [ SerializeField ] SharedFloat duration;
    [ SerializeField ] bool playOnStart;
    
    RecycledTween recycledTween_scroll = new RecycledTween();
#endregion

#region Unity API
    void Start()
    {
        if( playOnStart )
			Play();
	}
#endregion

#region API
    [ Button ]
    public void SetTimeScale( float newTimeScale )
    {
		recycledTween_scroll.Tween.timeScale = newTimeScale;
	}
    
    [ Button ]
    public void Play()
    {
		material.SetTextureOffset( property_name, initial_value );
		recycledTween_scroll.Recycle( material.DOOffset( target_value, property_name, duration.sharedValue ).SetLoops( -1, LoopType.Restart ) );
    }
    
    [ Button ]
    public void Stop()
    {
		recycledTween_scroll.RewindAndKill();
	}
    
    public void Kill()
    {
		recycledTween_scroll.Kill();
	}
#endregion

#region Implementation
#endregion
}
