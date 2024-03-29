﻿/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class GameSettings : ScriptableObject
    {
#region Fields (Settings)
    // Info: You can use Title() attribute ONCE for every game-specific group of settings.
    [ Title( "Spawn" ) ]
		[ LabelText( "Spawn Duration" ) ] public float spawn_duration;
		[ LabelText( "Spawn Duration Manual" ) ] public float spawn_duration_manual;
		[ LabelText( "Spawn Height" ) ] public float spawn_height;
		[ LabelText( "Spawn Radius" ) ] public float spawn_radius;

    [ Title( "Piggy Bank" ) ]
		[ LabelText( "Spawn Punch Scale" ) ] public PunchScaleTween piggy_spawn_punchScale;
		[ LabelText( "Spawn Punch Scale" ) ] public PunchScaleTween piggy_damage_punchScale;
		[ LabelText( "Merge Jump Duration" ) ] public float piggy_merge_jump_duration;
		[ LabelText( "Merge Jump Power" ) ] public float piggy_merge_jump_power;
		[ LabelText( "Merge Jump Ease" ) ] public Ease piggy_merge_jump_ease;
		[ LabelText( "Merge Lift Duration" ) ] public float piggy_merge_lift_duration;
		[ LabelText( "Merge Lift Ease" ) ] public Ease piggy_merge_lift_ease;
		[ LabelText( "Merge Lift Height" ) ] public float piggy_merge_lift_height;
		[ LabelText( "Damaged Color" ) ] public Color piggy_damaged_color;
		[ LabelText( "Scatter Duration" ) ] public float piggy_scatter_duration;

    [ Title( "Piggy Bank PFX" ) ]
		[ LabelText( "Upgrade Offset" ) ] public Vector3 piggy_pfx_upgrade_offset;
		[ LabelText( "Upgrade Size" ) ] public float piggy_pfx_upgrade_size;
		[ LabelText( "Shatter Size" ) ] public float piggy_pfx_shatter_size;
		[ LabelText( "Damage Size" ) ] public float piggy_pfx_damage_size;

    [ Title( "Piggy Bank PopUp Text" ) ]
		[ LabelText( "Text Size" ) ] public float piggy_popUpUI_size;
		[ LabelText( "Text Color" ) ] public Color piggy_popUpUI_color;
		[ LabelText( "Text Height Offset" ) ] public float piggy_popUpUI_offset_height;

    [ Title( "Cursor" ) ]
		[ LabelText( "Movement Speed" ) ] public float cursor_movement_speed;

    [ Title( "Selection" ) ]
        [ LabelText( "Selection Layer" ), Layer() ] public int selection_layer;
        [ LabelText( "Selection Distance" ) ] public float selection_distance;

    [ Title( "Camera" ) ]
        [ LabelText( "Follow Speed (Z)" ), SuffixLabel( "units/seconds" ), Min( 0 ) ] public float camera_follow_speed_depth = 2.8f;
    
    [ Title( "Project Setup", "These settings should not be edited by Level Designer(s).", TitleAlignments.Centered ) ]
        public int game_level_count_max;
        public int game_level_count_min;
		public LevelData[] game_level_data_array;

		// Info: 3 groups below (coming from template project) are foldout by design: They should remain hidden.
		[ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_GameSettings;
        [ FoldoutGroup( "Remote Config" ) ] public bool useRemoteConfig_Components;

        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for ui element"          ) ] public float ui_Entity_Move_TweenDuration;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the fading for ui element"            ) ] public float ui_Entity_Fade_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the scaling for ui element"           ) ] public float ui_Entity_Scale_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Duration of the movement for floating ui element" ) ] public float ui_Entity_FloatingMove_TweenDuration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Joy Stick"                                        ) ] public float ui_Entity_JoyStick_Gap;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text relative float height"                ) ] public float ui_PopUp_height;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "Pop Up Text float duration"                       ) ] public float ui_PopUp_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Random Spawn Area in Screen" ), SuffixLabel( "percentage" ) ] public float ui_particle_spawn_width; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Duration" ), SuffixLabel( "seconds" ) ] public float ui_particle_spawn_duration; 
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Spawn Ease" ) ] public Ease ui_particle_spawn_ease;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Wait Time Before Target" ) ] public float ui_particle_target_waitTime;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Time" ) ] public float ui_particle_target_duration;
		[ FoldoutGroup( "UI Settings" ), Tooltip( "UI Particle Target Travel Ease" ) ] public Ease ui_particle_target_ease;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Percentage of the screen to register a swipe"     ) ] public int swipeThreshold;
        [ FoldoutGroup( "UI Settings" ), Tooltip( "Safe Area Base Top Offset" ) ] public int ui_safeArea_offset_top = 88;

    [ Title( "UI Particle" ) ]
		[ LabelText( "Random Spawn Area in Screen Witdh Percentage" ) ] public float uiParticle_spawn_width_percentage = 10;
		[ LabelText( "Spawn Movement Duration" ) ] public float uiParticle_spawn_duration = 0.1f;
		[ LabelText( "Spawn Movement Ease" ) ] public DG.Tweening.Ease uiParticle_spawn_ease = DG.Tweening.Ease.Linear;
		[ LabelText( "Target Travel Wait Time" ) ] public float uiParticle_target_waitDuration = 0.16f;
		[ LabelText( "Target Travel Duration" ) ] public float uiParticle_target_duration = 0.4f;
		[ LabelText( "Target Travel Duration (REWARD)" ) ] public float uiParticle_target_duration_reward = 0.85f;
		[ LabelText( "Target Travel Ease" ) ] public DG.Tweening.Ease uiParticle_target_ease = DG.Tweening.Ease.Linear;


        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_height;
        [ FoldoutGroup( "Debug" ) ] public float debug_ui_text_float_duration;
#endregion

#region Fields (Singleton Related)
        static GameSettings instance;

        delegate GameSettings ReturnGameSettings();
        static ReturnGameSettings returnInstance = LoadInstance;

		public static GameSettings Instance => returnInstance();
#endregion

#region Implementation
        static GameSettings LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< GameSettings >( "game_settings" );

			returnInstance = ReturnInstance;

			return instance;
		}

		static GameSettings ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}
