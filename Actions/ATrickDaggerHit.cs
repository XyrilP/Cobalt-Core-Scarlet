using FSPRO;
using XyrilP.VionheartScarlet.Midrow;

namespace XyrilP.VionheartScarlet.Actions;

public class ATrickDaggerHit : AMissileHit
{
    public override void Update(G g, State s, Combat c)
    {
        c.stuff.TryGetValue(worldX, out StuffBase? value);
        if (!(value is Missile missile))
        {
            timer -= g.dt;
            return;
        }

        Ship ship = targetPlayer ? s.ship : c.otherShip;
        if (ship == null)
        {
          return;
        }

        RaycastResult raycastResult = CombatUtils.RaycastGlobal(c, ship, fromDrone: true, worldX);
        bool flag = false;
        if (raycastResult.hitShip)
        {
            Part? partAtWorldX = ship.GetPartAtWorldX(raycastResult.worldX);
            if (partAtWorldX == null || partAtWorldX.type != PType.empty)
            {
                flag = true;
            }
        }

        /* Original seeking code */
        if (Missile.missileData[missile.missileType].seeking)
        {
            raycastResult.worldX = missile.GetSeekerImpact(s, c);
            Part partAtWorldX2 = ship.GetPartAtWorldX(raycastResult.worldX)!;
            flag = partAtWorldX2 != null && partAtWorldX2.type != PType.empty;
        }

        if (!missile.isHitting)
        {
            Audio.Play(flag ? Event.Drones_MissileIncoming : Event.Drones_MissileMiss);
            missile.isHitting = true;
        }

        if (!(missile.yAnimation >= 3.5))
        {
            return;
        }

        if (flag)
        {
            int num = outgoingDamage;
            foreach (Artifact item in s.EnumerateAllArtifacts())
            {
                num += item.ModifyBaseMissileDamage(s, s.route as Combat, targetPlayer);
            }

            if (num < 0)
            {
                num = 0;
            }

            DamageDone dmg = ship.NormalDamage(s, c, num, raycastResult.worldX, true);
            EffectSpawner.NonCannonHit(g, targetPlayer, raycastResult, dmg);
            if (xPush != 0)
            {
                c.QueueImmediate(new AMove
                {
                    targetPlayer = targetPlayer,
                    dir = xPush
                });
            }

            Part? partAtWorldX3 = ship.GetPartAtWorldX(raycastResult.worldX);
            if (partAtWorldX3 != null && partAtWorldX3.stunModifier == PStunMod.stunnable)
            {
                c.QueueImmediate(new AStunPart
                {
                    worldX = raycastResult.worldX
                });
            }
            if(partAtWorldX3 != null && partAtWorldX3.damageModifier == PDamMod.armor)
            {
              partAtWorldX3.damageModifier = PDamMod.none;
            }

            if (status.HasValue && flag)
            {
                c.QueueImmediate(new AStatus
                {
                    status = status.Value,
                    statusAmount = statusAmount,
                    targetPlayer = targetPlayer
                });
            }

            if (weaken && flag)
            {
                c.QueueImmediate(new AWeaken
                {
                    worldX = worldX,
                    targetPlayer = targetPlayer
                });
            }

            if (ship.Get(Status.payback) > 0 || ship.Get(Status.tempPayback) > 0)
            {
                c.QueueImmediate(new AAttack
                {
                    damage = Card.GetActualDamage(s, ship.Get(Status.payback) + ship.Get(Status.tempPayback), !targetPlayer),
                    targetPlayer = !targetPlayer,
                    fast = true
                }
                );
            }
        }

        c.stuff.Remove(worldX);
        if (!(raycastResult.hitDrone || flag))
        {
            c.stuffOutro.Add(missile);
        }
    }
}