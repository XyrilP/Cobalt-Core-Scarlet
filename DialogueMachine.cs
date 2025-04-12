using System;
using XyrilP.VionheartScarlet;
using XyrilP.VionheartScarlet.Dialogue;
using Microsoft.Extensions.Logging;
using Nickel;

public static class DialogueMachine
{
	public static void Apply()
	{
		//StoryDialogue.Inject();
		//EventDialogue.Inject();
		CombatDialogue.Inject();
		CardDialogue.Inject();
		//ArtifactDialogue.Inject();
	}

	public static void ApplyInjections()
	{
		try
		{
			if (!VionheartScarlet.Instance.modDialogueInited)
			{
				VionheartScarlet.Instance.modDialogueInited = true;
			}
		}
		catch (Exception exception)
		{
			VionheartScarlet.Instance.Logger.LogError(exception, "Failed to inject dialogue for modded stuff");
		}
	}
}
