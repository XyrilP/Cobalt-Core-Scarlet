using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Nanoray.EnumByNameSourceGenerator;

namespace VionheartScarlet.ExternalAPI;

[EnumByName(typeof(UK), EnumByNameParseStrategy.DictionaryCache)]
[GeneratedCode("Nanoray.EnumByNameSourceGenerator.EnumByNameSourceGenerator", "1.0.0")]
internal static class StableUK
{
	private static readonly Dictionary<string, UK> __cache0 = new Dictionary<string, UK>();

	public static UK NO_TARGET => __ObtainEnumValue0("NO_TARGET");

	public static UK artifact => __ObtainEnumValue0("artifact");

	public static UK artifactBrowse_back => __ObtainEnumValue0("artifactBrowse_back");

	public static UK artifactReward_artifact => __ObtainEnumValue0("artifactReward_artifact");

	public static UK artifactReward_skip => __ObtainEnumValue0("artifactReward_skip");

	public static UK btn_moveDrones_left => __ObtainEnumValue0("btn_moveDrones_left");

	public static UK btn_moveDrones_right => __ObtainEnumValue0("btn_moveDrones_right");

	public static UK btn_move_left => __ObtainEnumValue0("btn_move_left");

	public static UK btn_move_right => __ObtainEnumValue0("btn_move_right");

	public static UK card => __ObtainEnumValue0("card");

	public static UK cardRemove_yes => __ObtainEnumValue0("cardRemove_yes");

	public static UK cardRemove_no => __ObtainEnumValue0("cardRemove_no");

	public static UK cardReward_skip => __ObtainEnumValue0("cardReward_skip");

	public static UK card_upgrade => __ObtainEnumValue0("card_upgrade");

	public static UK cardbrowse_back => __ObtainEnumValue0("cardbrowse_back");

	public static UK cardbrowse_cancel => __ObtainEnumValue0("cardbrowse_cancel");

	public static UK cardbrowse_hideUnknown => __ObtainEnumValue0("cardbrowse_hideUnknown");

	public static UK cardbrowse_sort => __ObtainEnumValue0("cardbrowse_sort");

	public static UK char_mini => __ObtainEnumValue0("char_mini");

	public static UK character => __ObtainEnumValue0("character");

	public static UK codex_artifacts => __ObtainEnumValue0("codex_artifacts");

	public static UK codex_back => __ObtainEnumValue0("codex_back");

	public static UK codex_cards => __ObtainEnumValue0("codex_cards");

	public static UK codex_credits => __ObtainEnumValue0("codex_credits");

	public static UK codex_logbook => __ObtainEnumValue0("codex_logbook");

	public static UK codex_progress => __ObtainEnumValue0("codex_progress");

	public static UK codex_progress_back => __ObtainEnumValue0("codex_progress_back");

	public static UK codex_runHistory => __ObtainEnumValue0("codex_runHistory");

	public static UK combat_deck => __ObtainEnumValue0("combat_deck");

	public static UK combat_discard => __ObtainEnumValue0("combat_discard");

	public static UK combat_endTurn => __ObtainEnumValue0("combat_endTurn");

	public static UK combat_energy => __ObtainEnumValue0("combat_energy");

	public static UK combat_exhaust => __ObtainEnumValue0("combat_exhaust");

	public static UK combat_handWarning => __ObtainEnumValue0("combat_handWarning");

	public static UK corner_deck => __ObtainEnumValue0("corner_deck");

	public static UK corner_mainmenu => __ObtainEnumValue0("corner_mainmenu");

	public static UK corner_map => __ObtainEnumValue0("corner_map");

	public static UK combat_root => __ObtainEnumValue0("combat_root");

	public static UK demoEnding_continue => __ObtainEnumValue0("demoEnding_continue");

	public static UK dialogue_box => __ObtainEnumValue0("dialogue_box");

	public static UK dialogue_choice => __ObtainEnumValue0("dialogue_choice");

	public static UK envModifier_warning => __ObtainEnumValue0("envModifier_warning");

	public static UK env_modifier_warning => __ObtainEnumValue0("env_modifier_warning");

	public static UK eyeball => __ObtainEnumValue0("eyeball");

	public static UK eyeball_gamepad_listener => __ObtainEnumValue0("eyeball_gamepad_listener");

	public static UK global => __ObtainEnumValue0("global");

	public static UK handOfCards => __ObtainEnumValue0("handOfCards");

	public static UK healthBar => __ObtainEnumValue0("healthBar");

	public static UK logbook_back => __ObtainEnumValue0("logbook_back");

	public static UK logbook_card => __ObtainEnumValue0("logbook_card");

	public static UK logbook_combo => __ObtainEnumValue0("logbook_combo");

	public static UK map_back => __ObtainEnumValue0("map_back");

	public static UK map_node => __ObtainEnumValue0("map_node");

	public static UK menu_abandon => __ObtainEnumValue0("menu_abandon");

	public static UK menu_codex => __ObtainEnumValue0("menu_codex");

	public static UK menu_discord => __ObtainEnumValue0("menu_discord");

	public static UK menu_play => __ObtainEnumValue0("menu_play");

	public static UK menu_profile => __ObtainEnumValue0("menu_profile");

	public static UK menu_quit => __ObtainEnumValue0("menu_quit");

	public static UK menu_settings => __ObtainEnumValue0("menu_settings");

	public static UK menu_steamstore => __ObtainEnumValue0("menu_steamstore");

	public static UK menu_riggsplush => __ObtainEnumValue0("menu_riggsplush");

	public static UK midrow => __ObtainEnumValue0("midrow");

	public static UK newRun_clear => __ObtainEnumValue0("newRun_clear");

	public static UK newRun_continue => __ObtainEnumValue0("newRun_continue");

	public static UK newRun_crewWarning => __ObtainEnumValue0("newRun_crewWarning");

	public static UK newRun_difficulty => __ObtainEnumValue0("newRun_difficulty");

	public static UK newRun_showVault => __ObtainEnumValue0("newRun_showVault");

	public static UK newRun_randomize => __ObtainEnumValue0("newRun_randomize");

	public static UK newRun_dailyRun => __ObtainEnumValue0("newRun_dailyRun");

	public static UK dailyPreview_continue => __ObtainEnumValue0("dailyPreview_continue");

	public static UK dailyPreview_back => __ObtainEnumValue0("dailyPreview_back");

	public static UK dailyPreview_leaderboard => __ObtainEnumValue0("dailyPreview_leaderboard");

	public static UK dailyWarning_continue => __ObtainEnumValue0("dailyWarning_continue");

	public static UK dailyWarning_back => __ObtainEnumValue0("dailyWarning_back");

	public static UK dailyLeaderboard_global => __ObtainEnumValue0("dailyLeaderboard_global");

	public static UK dailyLeaderboard_friends => __ObtainEnumValue0("dailyLeaderboard_friends");

	public static UK dailyLeaderboard_item => __ObtainEnumValue0("dailyLeaderboard_item");

	public static UK dailyLeaderboard_back => __ObtainEnumValue0("dailyLeaderboard_back");

	public static UK dailyLeaderboard_continue => __ObtainEnumValue0("dailyLeaderboard_continue");

	public static UK dailyLeaderboard_day_next => __ObtainEnumValue0("dailyLeaderboard_day_next");

	public static UK dailyLeaderboard_day_prev => __ObtainEnumValue0("dailyLeaderboard_day_prev");

	public static UK part => __ObtainEnumValue0("part");

	public static UK postRunRewards_continue => __ObtainEnumValue0("postRunRewards_continue");

	public static UK profile_restoreBackup => __ObtainEnumValue0("profile_restoreBackup");

	public static UK profile_showFile => __ObtainEnumValue0("profile_showFile");

	public static UK profile_delete => __ObtainEnumValue0("profile_delete");

	public static UK profile_select => __ObtainEnumValue0("profile_select");

	public static UK root => __ObtainEnumValue0("root");

	public static UK runSummary_continue => __ObtainEnumValue0("runSummary_continue");

	public static UK runSummary_back => __ObtainEnumValue0("runSummary_back");

	public static UK runHistory_item => __ObtainEnumValue0("runHistory_item");

	public static UK runHistory_page_left => __ObtainEnumValue0("runHistory_page_left");

	public static UK runHistory_page_right => __ObtainEnumValue0("runHistory_page_right");

	public static UK select_ship_left => __ObtainEnumValue0("select_ship_left");

	public static UK select_ship_right => __ObtainEnumValue0("select_ship_right");

	public static UK settings_back => __ObtainEnumValue0("settings_back");

	public static UK settings_controllerIcons => __ObtainEnumValue0("settings_controllerIcons");

	public static UK settings_controllerIcons_down => __ObtainEnumValue0("settings_controllerIcons_down");

	public static UK settings_controllerIcons_up => __ObtainEnumValue0("settings_controllerIcons_up");

	public static UK settings_fullscreen => __ObtainEnumValue0("settings_fullscreen");

	public static UK settings_pixelFont => __ObtainEnumValue0("settings_pixelFont");

	public static UK settings_locale => __ObtainEnumValue0("settings_locale");

	public static UK settings_locale_down => __ObtainEnumValue0("settings_locale_down");

	public static UK settings_locale_up => __ObtainEnumValue0("settings_locale_up");

	public static UK settings_keybinding_item => __ObtainEnumValue0("settings_keybinding_item");

	public static UK settings_keybindings => __ObtainEnumValue0("settings_keybindings");

	public static UK settings_keybindings_resetAll => __ObtainEnumValue0("settings_keybindings_resetAll");

	public static UK settings_keybindings_cancel => __ObtainEnumValue0("settings_keybindings_cancel");

	public static UK settings_keybindings_save => __ObtainEnumValue0("settings_keybindings_save");

	public static UK settings_musicVolume => __ObtainEnumValue0("settings_musicVolume");

	public static UK settings_musicVolume_down => __ObtainEnumValue0("settings_musicVolume_down");

	public static UK settings_musicVolume_mute => __ObtainEnumValue0("settings_musicVolume_mute");

	public static UK settings_musicVolume_up => __ObtainEnumValue0("settings_musicVolume_up");

	public static UK settings_reduceMotion => __ObtainEnumValue0("settings_reduceMotion");

	public static UK settings_rumble => __ObtainEnumValue0("settings_rumble");

	public static UK settings_screenshake => __ObtainEnumValue0("settings_screenshake");

	public static UK settings_sfxVolume => __ObtainEnumValue0("settings_sfxVolume");

	public static UK settings_sfxVolume_down => __ObtainEnumValue0("settings_sfxVolume_down");

	public static UK settings_sfxVolume_up => __ObtainEnumValue0("settings_sfxVolume_up");

	public static UK settings_volume => __ObtainEnumValue0("settings_volume");

	public static UK settings_volume_down => __ObtainEnumValue0("settings_volume_down");

	public static UK settings_volume_mute => __ObtainEnumValue0("settings_volume_mute");

	public static UK settings_volume_up => __ObtainEnumValue0("settings_volume_up");

	public static UK shipPreview => __ObtainEnumValue0("shipPreview");

	public static UK shipUpgrades_continue => __ObtainEnumValue0("shipUpgrades_continue");

	public static UK showCard_continue => __ObtainEnumValue0("showCard_continue");

	public static UK splash => __ObtainEnumValue0("splash");

	public static UK status => __ObtainEnumValue0("status");

	public static UK test => __ObtainEnumValue0("test");

	public static UK tooltip_reminder => __ObtainEnumValue0("tooltip_reminder");

	public static UK unlockedDeck_continue => __ObtainEnumValue0("unlockedDeck_continue");

	public static UK upgradeCard_cancel => __ObtainEnumValue0("upgradeCard_cancel");

	public static UK upgradePreview => __ObtainEnumValue0("upgradePreview");

	public static UK vault_continue => __ObtainEnumValue0("vault_continue");

	public static UK vault_memory => __ObtainEnumValue0("vault_memory");

	public static UK wall_left => __ObtainEnumValue0("wall_left");

	public static UK wall_right => __ObtainEnumValue0("wall_right");

	public static UK tractor_left => __ObtainEnumValue0("tractor_left");

	public static UK tractor_right => __ObtainEnumValue0("tractor_right");

	private static UK __ObtainEnumValue0(string name)
	{
		if (!__cache0.TryGetValue(name, out var enumValue))
		{
			enumValue = Enum.Parse<UK>(name);
			__cache0[name] = enumValue;
		}
		return enumValue;
	}
}