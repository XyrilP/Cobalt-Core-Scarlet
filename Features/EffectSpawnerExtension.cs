using FMOD;
using FSPRO;
using Microsoft.Xna.Framework.Graphics;

namespace VionheartScarlet.Features;

internal class EffectSpawnerExtension
{
	public static void CannonAngled(G g, bool targetPlayer, int originX, RaycastResult ray, DamageDone dmg)
	{
		Route route = g.state.route;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Combat combat = (Combat)(object)((route is Combat) ? route : null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (combat != null)
		{
			Vec start = ray.fromDrone ? FxPositions.DroneCannon(originX, !targetPlayer) : targetPlayer ? FxPositions.Cannon(combat.otherShip.x + originX, !targetPlayer) : FxPositions.Cannon(g.state.ship.x + originX, !targetPlayer);
			Vec end = (!ray.hitDrone && !ray.hitShip) ? FxPositions.Miss(ray.worldX, targetPlayer) : (ray.hitDrone ? FxPositions.Drone(ray.worldX) : (dmg.hitHull ? FxPositions.Hull(ray.worldX, targetPlayer) : FxPositions.Shield(ray.worldX, targetPlayer)));
			combat.fx.Add((FX)(object)new CannonBeamAngled
			{
				start = start,
				end = end,
				w = 1.0
			});
			GUID? gUID = null;
			if (ray.hitShip)
			{
				ParticleBursts.HullImpact(g, end, targetPlayer, !ray.hitDrone, ray.fromDrone);
			}
			if (dmg.hitShield && !dmg.hitHull)
			{
				combat.fx.Add((FX)new ShieldHit
				{
					pos = FxPositions.Shield(ray.worldX, targetPlayer)
				});
				ParticleBursts.ShieldImpact(g, FxPositions.Shield(ray.worldX, targetPlayer), targetPlayer);
			}
			if (dmg.poppedShield)
			{
				combat.fx.Add((FX)new ShieldPop
				{
					pos = FxPositions.Shield(ray.worldX, targetPlayer)
				});
			}
			if (dmg.poppedShield)
			{
				gUID = Event.Hits_ShieldPop;
			}
			else if (dmg.hitShield)
			{
				gUID = Event.Hits_ShieldHit;
			}
			if (!ray.hitDrone && !ray.hitShip)
			{
				gUID = Event.Hits_Miss;
			}
			else if (dmg.hitHull)
			{
				gUID = (!targetPlayer) ? new GUID?(Event.Hits_OutgoingHit) : new GUID?(Event.Hits_HitHurt);
			}
			else if (ray.hitDrone)
			{
				gUID = Event.Hits_HitDrone;
			}
			if (gUID.HasValue)
			{
				Audio.Play((GUID?)gUID.Value, true);
			}
		}
	}
}
public class CannonBeamAngled : FX
{
	public Vec start;

	public Vec end;

	public double w;

	public static Color cannonBeam = new Color("ff8866");

	public static Color cannonBeamCore = new Color("ffffff");

	public override void Render(G g, Vec v)
	{
		Rect r = Rect.FromPoints(start, end);
		double num = 0.1;
		if (base.age < num)
		{
			double num2 = 2.0 * (1.0 - base.age / num);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Draw.Line(v.x + start.x, v.y + start.y, v.x + end.x, v.y + end.y, w + 2.0 * (num2 + 1.0), cannonBeam, BlendMode.Screen, (SamplerState)null, (Effect)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Draw.Line(v.x + start.x, v.y + start.y, v.x + end.x, v.y + end.y, w + num2 * 2.0, cannonBeamCore, (BlendState)null, (SamplerState)null, (Effect)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }
	}
}