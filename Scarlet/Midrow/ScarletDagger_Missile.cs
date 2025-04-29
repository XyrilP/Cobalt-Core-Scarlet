using FSPRO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Converters;
using Nickel;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using VionheartScarlet.Actions;

namespace VionheartScarlet.Midrow
{
  public class ScarletDagger_Missile : Missile
  {
    public static readonly string MIDROW_OBJECT_NAME = "ScarletDagger_Missile";
    public static readonly int BASE_DAMAGE = 2;
    public static readonly Color exhaustColor = new Color("919191");
    private static int jugglesNormal = 0;
    private static int jugglesHeavy = 0;
    private static string? daggerTitle { get; set; }
    private static string? daggerDescription { get; set; }
    private static ISpriteEntry? daggerIcon { get; set; }
    public ScarletDagger_Missile()
    {
      switch (missileType)
      {
        case MissileType.normal:
          DB.drones[MIDROW_OBJECT_NAME] = VionheartScarlet.Instance.TrickDagger.Sprite;
          skin = MIDROW_OBJECT_NAME;
          break;
        case MissileType.heavy:
          DB.drones[MIDROW_OBJECT_NAME + "_Heavy"] = VionheartScarlet.Instance.TrickDagger.Sprite;
          skin = MIDROW_OBJECT_NAME + "_Heavy";
          break;
        case MissileType.seeker:
          DB.drones[MIDROW_OBJECT_NAME + "_Seeker"] = VionheartScarlet.Instance.TrickDagger_Seeker.Sprite;
          skin = MIDROW_OBJECT_NAME + "_Seeker";
          break;
      }
    }
    public override double GetWiggleAmount() => 1.0;

    public override double GetWiggleRate() => 1.0;

    public override string GetDialogueTag() => MIDROW_OBJECT_NAME;

    public override Spr? GetIcon() => new Spr?(VionheartScarlet.Instance.TrickDagger_Icon.Sprite);

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
      //Draw.Sprite(nullable1, num1, num2, flag1, flag2, 0.0, new Vec?(), nullable2, new Vec?(), new Rect?(), nullable3);
      // Glow.Draw(vec2 + new Vec(0.5, -2.5), 25.0, exhaustColor * new Color(1.0, 0.5, 0.5).gain(0.2 + 0.1 * Math.Sin(g.state.time * 30.0 + x) * 0.5));
      switch (missileType)
      {
        case MissileType.normal:
          Spr sprite_normal = VionheartScarlet.Instance.TrickDagger.Sprite;
          DrawWithHilight(g, sprite_normal, v1, flipX, targetPlayer);
          daggerTitle = VionheartScarlet.Instance.Localizations.Localize(["midrow", "TrickDagger_Missile", "name"]);
          daggerDescription = VionheartScarlet.Instance.Localizations.Localize(["midrow", "TrickDagger_Missile", "description"]);
          daggerIcon = VionheartScarlet.Instance.TrickDagger_Icon;
          break;
        case MissileType.heavy:
          Spr sprite_heavy = VionheartScarlet.Instance.TrickDagger.Sprite;
          DrawWithHilight(g, sprite_heavy, v1, flipX, targetPlayer);
          daggerTitle = VionheartScarlet.Instance.Localizations.Localize(["midrow", "YashaDagger_Missile", "name"]);
          daggerDescription = VionheartScarlet.Instance.Localizations.Localize(["midrow", "YashaDagger_Missile", "description"]);
          daggerIcon = VionheartScarlet.Instance.TrickDagger_Icon;
          break;
        case MissileType.seeker:
          int num = (missileData[missileType].seeking && g.state.route is Combat c) ? Math.Sign(GetSeekerImpact(g.state, c) - x) : 0;
          Spr sprite_seeker = VionheartScarlet.Instance.TrickDagger_Seeker.Sprite;
          Spr sprite_seeker_angled = VionheartScarlet.Instance.TrickDagger_Seeker_Angled.Sprite;
          daggerTitle = VionheartScarlet.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "name"]);
          daggerDescription = VionheartScarlet.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "description"]);
          daggerIcon = VionheartScarlet.Instance.TrickDagger_Seeker_Icon;
          if (num == 0)
          {
            DrawWithHilight(g, sprite_seeker, v1, flipX, targetPlayer);
          }
          else
          {
            flipX = num < 0;
            DrawWithHilight(g, sprite_seeker_angled, v1, flipX, targetPlayer);
          }
          break;
      }
    }

    public override List<Tooltip> GetTooltips()
    {
      return [new GlossaryTooltip($"{VionheartScarlet.Instance.Package.Manifest.UniqueName}::Midrow::{daggerTitle}")
			{
				Icon = daggerIcon?.Sprite,
				TitleColor = Colors.midrow,
				Title = daggerTitle,
				Description = daggerDescription
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
      jugglesNormal+=1;
      if (missileType == MissileType.heavy)
      {
        jugglesHeavy+=1;
      }
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
  public static class TrickDagger
    {
        public static Missile isTrickDagger(this Missile data)
        {
            VionheartScarlet.Instance.Helper.ModData.SetModData(data, "isTrickDagger", true);
            return data;
        }
        public static Missile juggleDamage(this Missile data)
        {
            VionheartScarlet.Instance.Helper.ModData.SetModData(data, "juggleDamage", 0);
            return data;
        }
    }
}
