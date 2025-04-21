using System;
using Microsoft.Extensions.Logging;
using Nickel;

namespace XyrilP.VionheartScarlet.Dialogue;
internal static class Dialogue
{
	internal static void Inject()
	{
		StoryDialogue.Inject();
		CombatDialogue.Inject();
		CardDialogue.Inject();
		ArtifactDialogue.Inject();
		
	}

	public static void ApplyInjections()
	{
		try
		{
			if (!VionheartScarlet.Instance.modDialogueInited)
			{
				VionheartScarlet.Instance.modDialogueInited = true;
				VionheartScarlet.Instance.Logger.LogInformation("I have a mouth... and I can now speak!");
			}
		}
		catch (Exception exception)
		{
			VionheartScarlet.Instance.Logger.LogError(exception, "Failed to inject dialogue for modded stuff");
		}
	}
}
