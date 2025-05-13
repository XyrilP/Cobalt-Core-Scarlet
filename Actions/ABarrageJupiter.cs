using System;
using System.Collections.Generic;
using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Graphics;

namespace VionheartScarlet.Actions;

public class ABarrageJupiter : AJupiterShoot
{
	public new required ABarrageAttack attackCopy;
	public override void Begin(G g, State s, Combat c)
	{
		timer = 0.0;
		SortedList<int, CardAction> sortedList = new SortedList<int, CardAction>();
		foreach (KeyValuePair<int, StuffBase> item in c.stuff)
		{
			if (!(item.Value is JupiterDrone))
			{
				continue;
			}
			ABarrageAttack aAttack = Mutil.DeepCopy(attackCopy);
			aAttack.fast = true;
			aAttack.fromX = null;
			aAttack.fromDroneX = item.Value.x;
			aAttack.targetPlayer = item.Value.targetPlayer;
			aAttack.shardcost = 0;
			int damage = aAttack.damage;
			foreach (Artifact item2 in s.EnumerateAllArtifacts())
			{
				aAttack.damage += item2.ModifyBaseJupiterDroneDamage(s, c, item.Value);
				if (aAttack.damage > damage)
				{
					aAttack.artifactPulse = item2.Key();
					damage = aAttack.damage;
				}
			}
			sortedList.Add(item.Value.x, aAttack);
		}
		c.QueueImmediate(sortedList.Values);
	}
}