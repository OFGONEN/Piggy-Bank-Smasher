/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class SharedData< SharedDataType > : ScriptableObject
    {
#region Fields
        [ ShowInInspector ]
#if UNITY_EDITOR
        [ SuffixLabel( "@Suffix()" ) ]
#endif
        public SharedDataType sharedValue;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
        protected virtual string Suffix() => "";
#endif
#endregion
    }
}