using System;
using System.Collections.Generic;
using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Graphics;
using VionheartScarlet.Features;

namespace VionheartScarlet.Actions;

public class ABarrageAttack : AAttack
{
	
    public override void Begin(G g, State s, Combat c)
	{
		Ship ship = targetPlayer ? s.ship : c.otherShip;
		Ship ship2 = targetPlayer ? c.otherShip : s.ship;
		var offset = s.rngActions.Next();
		if (offset <= 0.33) offset = -1;
		else if (offset > 0.33 && offset < 0.66) offset = 0;
		else offset = 1;
		int? num = GetFromX(s, c);
		RaycastResult? raycastResult = fromDroneX.HasValue ? CombatUtils.RaycastGlobal(c, ship, fromDrone: true, fromDroneX.Value + (int)offset) : num.HasValue ? CombatUtils.RaycastFromShipLocal(s, c, num.Value + (int)offset, targetPlayer) : null;
		DamageDone dmg = new DamageDone();
		damage = 0;
		fast = true;

		if (ship == null || ship2 == null || ship.hull <= 0 || (fromDroneX.HasValue && !c.stuff.ContainsKey(fromDroneX.Value)))
		{
			return;
		}
		bool flag = ship2.Get(Status.libra) > 0 && !fromDroneX.HasValue;
		if (!targetPlayer && !fromDroneX.HasValue && g.state.ship.GetPartTypeCount(PType.cannon) > 1 && !multiCannonVolley)
		{
			c.QueueImmediate(new ABarrageVolley
			{
				attack = Mutil.DeepCopy(this)
			}
			);
			timer = 0.0;
		}
		else
		{
            /* Reduce Fade if doing Barrage attacks. */
            // Rework: Attacks don't decrement Fade anymore.
            // var fadeValue = ship2.Get(VionheartScarlet.Instance.Fade.Status);
            // if (fadeValue > 0)
            // {
            // 	ship2.Add(VionheartScarlet.Instance.Fade.Status, -1);
            // }
            /* Reduce Fade if doing Barrage attacks. */

            /* Fade animation */
            var fadeValue = ship.Get(VionheartScarlet.Instance.Fade.Status);
			if (fadeValue > 0)
			{
				ship.Add(VionheartScarlet.Instance.Fade.Status, -1); // Reduce target's Fade when barraged.
				if (raycastResult == null)
				{
					return;
				}
				if (fromDroneX.HasValue)
				{
					c.stuff.TryGetValue(fromDroneX.Value, out StuffBase? value);
					if (value is AttackDrone attackDrone)
					{
						attackDrone.pulse = 1.0;
					}
					if (value is EnergyDrone energyDrone)
					{
						energyDrone.pulse = 1.0;
					}
					if (value is ShieldDrone shieldDrone)
					{
						shieldDrone.pulse = 1.0;
					}
					if (value is JupiterDrone jupiterDrone)
					{
						jupiterDrone.pulse = 1.0;
					}
					if (value is DualDrone dualDrone)
					{
						dualDrone.pulse = 1.0;
					}
				}
				else
				{
					Part? partAtLocalX = ship2.GetPartAtLocalX(num.HasValue ? num.Value : 0);
					if (partAtLocalX != null)
					{
						partAtLocalX.pulse = 1.0;
					}
				}
				raycastResult.hitShip = false;
				EffectSpawnerExtension.CannonAngled(g, targetPlayer, fromDroneX.HasValue ? fromDroneX.Value : (num.HasValue ? num.Value : 0), raycastResult, dmg); //Play an animation?
			}
			/* Fade animation */

			if (raycastResult != null && ApplyAutododge(c, ship, raycastResult))
			{
				return;
			}
			if (!targetPlayer && !fromDroneX.HasValue)
			{
				foreach (Artifact item in s.EnumerateAllArtifacts())
				{
					if (item.OnPlayerAttackMakeItPierce(s, c) == true)
					{
						piercing = true;
						item.Pulse();
					}
				}
				foreach (Artifact item2 in s.EnumerateAllArtifacts())
				{
					if (item2.ModifyAttacksToStun(s, c) == true)
					{
						stunEnemy = true;
						item2.Pulse();
					}
				}
			}
			if (!targetPlayer && fromDroneX.HasValue)
			{
				foreach (Artifact item3 in s.EnumerateAllArtifacts())
				{
					if (item3.OnDroneAttackEnemyMakeItPierce(s, c) == true)
					{
						piercing = true;
						item3.Pulse();
					}
				}
			}
			if (!stunEnemy && s.ship.Get(Status.stunCharge) > 0 && !targetPlayer && !fromDroneX.HasValue)
			{
				s.ship.Set(Status.stunCharge, s.ship.Get(Status.stunCharge) - 1);
				stunEnemy = true;
			}
			if (!fromDroneX.HasValue && !targetPlayer && !multiCannonVolley)
			{
				c.QueueImmediate(new ABarrageJupiter
				{
					attackCopy = Mutil.DeepCopy(this)
				}
				);
			}
			if (raycastResult == null)
			{
				timer = 0.0;
				if (flag)
				{
					DoLibraEffect(c, ship2);
				}
				{
					foreach (Artifact item4 in s.EnumerateAllArtifacts())
					{
						item4.OnPlayerAttack(s, c);
					}
					return;
				}
			}
			if (raycastResult.hitDrone)
			{
				bool flag2 = c.stuff[raycastResult.worldX].Invincible();
				foreach (Artifact item5 in s.EnumerateAllArtifacts())
				{
					if (item5.ModifyDroneInvincibility(s, c, c.stuff[raycastResult.worldX]) == true)
					{
						flag2 = true;
						item5.Pulse();
					}
				}
				if (c.stuff[raycastResult.worldX].bubbleShield && !piercing)
				{
					c.stuff[raycastResult.worldX].bubbleShield = false;
				}
				else if (flag2)
				{
					c.QueueImmediate(c.stuff[raycastResult.worldX].GetActionsOnShotWhileInvincible(s, c, !targetPlayer, damage));
				}
				else
				{
					c.DestroyDroneAt(s, raycastResult.worldX, !targetPlayer);
				}
			}
			timer = 0.1;
			if (raycastResult.hitShip)
			{
				dmg = (isBeam ? new DamageDone
				{
					hitHull = true,
					hitShield = false,
					poppedShield = false
				} : ship.NormalDamage(s, c, damage, raycastResult.worldX, piercing));
				Part? partAtWorldX = ship.GetPartAtWorldX(raycastResult.worldX);
				if (partAtWorldX != null && partAtWorldX.stunModifier == PStunMod.stunnable)
				{
					stunEnemy = true;
				}
				if (!isBeam && (ship.Get(Status.payback) > 0 || ship.Get(Status.tempPayback) > 0) && paybackCounter < 100)
				{
					c.QueueImmediate(new AAttack
					{
						paybackCounter = paybackCounter + 1,
						damage = Card.GetActualDamage(s, ship.Get(Status.payback) + ship.Get(Status.tempPayback), !targetPlayer),
						targetPlayer = !targetPlayer,
						fast = true,
						storyFromPayback = true
					});
				}
				if (moveEnemy != 0)
				{
					c.QueueImmediate(new AMove
					{
						dir = moveEnemy,
						targetPlayer = targetPlayer
					});
				}
				if (status.HasValue)
				{
					c.QueueImmediate(new AStatus
					{
						status = status.Value,
						statusAmount = statusAmount,
						targetPlayer = targetPlayer
					});
				}
				if (givesEnergy.HasValue && targetPlayer)
				{
					c.QueueImmediate(new AEnergy
					{
						changeAmount = 1
					});
				}
				if (cardOnHit != null)
				{
					c.QueueImmediate(new AAddCard
					{
						card = Mutil.DeepCopy(cardOnHit),
						destination = destination
					});
				}
				if (stunEnemy && !targetPlayer)
				{
					c.QueueImmediate(new AStunPart
					{
						worldX = raycastResult.worldX
					});
				}
				if (weaken)
				{
					c.QueueImmediate(new AWeaken
					{
						worldX = raycastResult.worldX,
						targetPlayer = targetPlayer
					});
				}
				if (brittle)
				{
					c.QueueImmediate(new ABrittle
					{
						worldX = raycastResult.worldX,
						targetPlayer = targetPlayer
					});
				}
				if (ship.Get(Status.reflexiveCoating) >= 1)
				{
					c.QueueImmediate(new AArmor
					{
						worldX = raycastResult.worldX,
						targetPlayer = targetPlayer,
						justTheActiveOverride = true
					});
				}
				if (armorize)
				{
					c.QueueImmediate(new AArmor
					{
						worldX = raycastResult.worldX,
						targetPlayer = targetPlayer
					});
				}
                /* Saturation implementation */
                var saturationStatus = VionheartScarlet.Instance.Saturation.Status;
				ship.Add(saturationStatus, 1);
				while (true)
				{
					var saturationValue = ship.Get(saturationStatus);
                    if (saturationValue >= 2)
                    {
                        ship.NormalDamage(s, c, 1, null, false);
                        ship.Add(saturationStatus, -2);
                        Audio.Play(StatusMeta.GetSound(saturationStatus, false));
					}
                    else
                    {
                        break;
                    }
				}
				/* Saturation implementation */
			}
			if (flag)
			{
				DoLibraEffect(c, ship2);
			}
			if (!isBeam && !targetPlayer && !fromDroneX.HasValue)
			{
				Input.Rumble(0.5);
			}
			if (!isBeam)
			{
				if (targetPlayer)
				{
					if (!raycastResult.hitShip && !raycastResult.hitDrone)
					{
						g.state.storyVars.enemyShotJustMissed = true;
					}
					if (raycastResult.hitShip)
					{
						g.state.storyVars.enemyShotJustHit = true;
					}
					foreach (Artifact item6 in s.EnumerateAllArtifacts())
					{
						item6.OnEnemyAttack(s, c);
					}
					if (!raycastResult.hitShip && !raycastResult.hitDrone)
					{
						foreach (Artifact item7 in s.EnumerateAllArtifacts())
						{
							item7.OnPlayerDodgeHit(s, c);
						}
					}
				}
				else
				{
					if (raycastResult.hitDrone)
					{
						g.state.storyVars.playerJustShotAMidrowObject = true;
					}
					if (!raycastResult.hitShip && !raycastResult.hitDrone)
					{
						g.state.storyVars.playerShotJustMissed = true;
					}
					else
					{
						g.state.storyVars.playerShotJustMissed = false;
					}
					if (raycastResult.hitShip)
					{
						g.state.storyVars.playerShotJustHit = true;
					}
					g.state.storyVars.playerShotWasFromStrafe = storyFromStrafe;
					g.state.storyVars.playerShotWasFromPayback = storyFromPayback;
					if (!fromDroneX.HasValue)
					{
						foreach (Artifact item8 in s.EnumerateAllArtifacts())
						{
							item8.OnPlayerAttack(s, c);
						}
					}
					if (raycastResult.hitShip)
					{
						if (c.otherShip.ai != null)
						{
							c.otherShip.ai.OnHitByAttack(s, c, raycastResult.worldX, this);
						}
						foreach (Artifact item9 in s.EnumerateAllArtifacts())
						{
							item9.OnEnemyGetHit(s, c, c.otherShip.GetPartAtWorldX(raycastResult.worldX));
						}
					}
					if (!raycastResult.hitShip && !raycastResult.hitDrone && !fromDroneX.HasValue)
					{
						foreach (Artifact item10 in s.EnumerateAllArtifacts())
						{
							item10.OnEnemyDodgePlayerAttack(s, c);
						}
					}
					if (!raycastResult.hitShip && !raycastResult.hitDrone)
					{
						bool flag3 = false;
						for (int i = -1; i <= 1; i += 2)
						{
							if (CombatUtils.RaycastGlobal(c, ship, fromDrone: true, raycastResult.worldX + i).hitShip)
							{
								flag3 = true;
							}
						}
						if (flag3)
						{
							foreach (Artifact item11 in s.EnumerateAllArtifacts())
							{
								item11.OnEnemyDodgePlayerAttackByOneTile(s, c);
							}
						}
					}
				}
				if (ship.hull <= 0 && onKillActions != null)
				{
					List<CardAction> list = Mutil.DeepCopy(onKillActions);
					foreach (CardAction item12 in list)
					{
						item12.canRunAfterKill = true;
					}
					c.QueueImmediate(list);
				}
			}
			if (fromDroneX.HasValue)
			{
				c.stuff.TryGetValue(fromDroneX.Value, out StuffBase? value);
				if (value is AttackDrone attackDrone)
				{
					attackDrone.pulse = 1.0;
				}
				if (value is EnergyDrone energyDrone)
				{
					energyDrone.pulse = 1.0;
				}
				if (value is ShieldDrone shieldDrone)
				{
					shieldDrone.pulse = 1.0;
				}
				if (value is JupiterDrone jupiterDrone)
				{
					jupiterDrone.pulse = 1.0;
				}
				if (value is DualDrone dualDrone)
				{
					dualDrone.pulse = 1.0;
				}
			}
			else
			{
				Part? partAtLocalX = ship2.GetPartAtLocalX(num.HasValue ? num.Value : 0);
				if (partAtLocalX != null)
				{
					partAtLocalX.pulse = 1.0;
				}
			}
			EffectSpawnerExtension.CannonAngled(g, targetPlayer, fromDroneX.HasValue ? fromDroneX.Value : (num.HasValue ? num.Value : 0), raycastResult, dmg);
		}
	}
}