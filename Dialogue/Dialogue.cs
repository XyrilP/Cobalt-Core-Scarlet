using System;
using Microsoft.Extensions.Logging;
using Nickel;

namespace Vionheart.Dialogue;
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
			if (!Vionheart.Instance.modDialogueInited)
			{
				Vionheart.Instance.modDialogueInited = true;
				Vionheart.Instance.Logger.LogInformation("I have a mouth... and I can now speak!");
			}
		}
		catch (Exception exception)
		{
			Vionheart.Instance.Logger.LogError(exception, "Failed to inject dialogue for modded stuff");
		}
	}
}
