using System;
using System.Collections.Generic;
using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Graphics;

namespace VionheartScarlet.Actions;

public class ABarrageVolley : AVolleyAttackFromAllCannons
{
    public new required ABarrageAttack attack;
	public override void Begin(G g, State s, Combat c)
	{
		timer = 0.0;
		attack.multiCannonVolley = true;
		attack.fast = true;
		List<ABarrageAttack> list = new List<ABarrageAttack>();
		int num = 0;
		foreach (Part part in s.ship.parts)
		{
			if (part.type == PType.cannon && part.active)
			{
				attack.fromX = num;
                list.Add(Mutil.DeepCopy(attack));
			}
            num++;
		}
		c.QueueImmediate(new ABarrageJupiter
		{
			attackCopy = Mutil.DeepCopy(attack)
		}
        );
		c.QueueImmediate(list);
	}
}