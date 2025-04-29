using FSPRO;
using Microsoft.Xna.Framework.Graphics;
using Nickel;
using System;
using System.Collections.Generic;
using Vionheart.Actions;

namespace Vionheart.Midrow
{
  public class SangeDagger_Missile : Missile
  {
    public static readonly string MIDROW_OBJECT_NAME = "TrickDagger_Seeker";
    public static readonly int BASE_DAMAGE = 2;
    public static readonly Color exhaustColor = new Color("919191");

    static SangeDagger_Missile()
    {
      DB.drones[MIDROW_OBJECT_NAME] = Vionheart.Instance.TrickDagger_Seeker.Sprite;
    }

    public SangeDagger_Missile() => this.skin = MIDROW_OBJECT_NAME;

    public override double GetWiggleAmount() => 2.0;

    public override double GetWiggleRate() => 10.0;

    public override string GetDialogueTag() => MIDROW_OBJECT_NAME;

    public override Spr? GetIcon() => new Spr?(Vionheart.Instance.TrickDagger_Seeker_Icon.Sprite);

    public override void Render(G g, Vec v)
    {
      Vec offset = GetOffset(g, true);
      Vec v1 = v + offset;
      Vec vec1 = new Vec();
      if (!this.targetPlayer)
        vec1 += new Vec(y: 21.0);
      Vec vec2 = v1 + vec1 + new Vec(7.0, 8.0);
      bool flipX = false;
      Spr? nullable1 = new Spr?(exhaustSprites.GetModulo((int) (g.state.time * 36.0 + x * 10)));
      double num1 = vec2.x - 5.0;
      double num2 = vec2.y + (!this.targetPlayer ? 14.0 : 0.0);
      Vec? nullable2 = new Vec?(new Vec(y: 1.0));
      bool targetPlayer = this.targetPlayer;
      bool flag1 = flipX;
      bool flag2 = !targetPlayer;
      // Color? nullable3 = new Color?(exhaustColor);
      missileType = MissileType.seeker;
      Spr sprite_animated = Vionheart.Instance.TrickDagger_Seeker_Animated.Sprite;
      Spr sprite = Vionheart.Instance.TrickDagger_Seeker.Sprite; 
      Spr sprite_angled = Vionheart.Instance.TrickDagger_Seeker_Angled.Sprite;
      int num = (missileData[missileType].seeking && g.state.route is Combat c) ? Math.Sign(GetSeekerImpact(g.state, c) - x) : 0;
      if (num == 0)
      {
        DrawWithHilight(g, sprite, v1, flipX, targetPlayer);
      }
      else
      {
        flipX = num < 0;
        DrawWithHilight(g, sprite_angled, v1, flipX, targetPlayer);
      }
      //Draw.Sprite(nullable1, num1, num2, flag1, flag2, 0.0, new Vec?(), nullable2, new Vec?(), new Rect?(), nullable3);
      // Glow.Draw(vec2 + new Vec(0.5, -2.5), 25.0, exhaustColor * new Color(1.0, 0.5, 0.5).gain(0.2 + 0.1 * Math.Sin(g.state.time * 30.0 + x) * 0.5));
    }

    public override List<Tooltip> GetTooltips()
    {
      return [new GlossaryTooltip($"{Vionheart.Instance.Package.Manifest.UniqueName}::Midrow::SangeDagger_Missile")
			{
				Icon = Vionheart.Instance.TrickDagger_Seeker_Icon.Sprite,
				TitleColor = Colors.midrow,
				Title = Vionheart.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "name"]),
				Description = Vionheart.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "description"])
			}];
    }

    public override List<CardAction>? GetActions(State s, Combat c)
    {
      return new List<CardAction>()
      {
        new ATrickDaggerHit()
        {
          worldX = x,
          outgoingDamage = BASE_DAMAGE,
          targetPlayer = targetPlayer
        }
      };
    }
    public override bool Invincible()
    {
        return true;
    }
    public override List<CardAction>? GetActionsOnShotWhileInvincible(State s, Combat c, bool wasPlayer, int damage)
    {
      var dir = s.rngActions.Next();
      if (dir < 0.5) dir = -1;
      else dir = 1;
      return new List<CardAction>
      {
        new AKickMiette
        {
          x = x,
          dir = Convert.ToInt32(dir)
        }
      };
    }
  }
}
