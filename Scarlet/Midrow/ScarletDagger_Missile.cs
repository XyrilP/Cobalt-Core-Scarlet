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
    public static readonly int BASE_DAMAGE = 1;
    public ScarletDagger_Missile()
    {
      switch (missileType)
      {
        case MissileType.normal:
          DB.drones[MIDROW_OBJECT_NAME] = VionheartScarlet.Instance.TrickDagger.Sprite;
          skin = MIDROW_OBJECT_NAME;
          break;
        case MissileType.heavy:
          DB.drones[MIDROW_OBJECT_NAME + "_Heavy"] = VionheartScarlet.Instance.TrickDagger_Heavy.Sprite;
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
    public override Spr? GetIcon()
    {
      switch (missileType)
      {
        case MissileType.heavy:
          return VionheartScarlet.Instance.TrickDagger_Heavy_Icon.Sprite;
        case MissileType.seeker:
          return VionheartScarlet.Instance.TrickDagger_Seeker_Icon.Sprite;
        default:
          return VionheartScarlet.Instance.TrickDagger_Icon.Sprite;
      }
    }
    public override void Render(G g, Vec v)
    {
      Vec offset = GetOffset(g, true);
      Vec v1 = v + offset;
      bool flipX = false;
      bool targetPlayer = this.targetPlayer;
      switch (missileType)
      {
        case MissileType.normal:
          Spr sprite_normal = VionheartScarlet.Instance.TrickDagger.Sprite;
          DrawWithHilight(g, sprite_normal, v1, flipX, targetPlayer);
          break;
        case MissileType.heavy:
          Spr sprite_heavy = VionheartScarlet.Instance.TrickDagger_Heavy.Sprite;
          DrawWithHilight(g, sprite_heavy, v1, flipX, targetPlayer);
          break;
        case MissileType.seeker:
          int num = (missileData[missileType].seeking && g.state.route is Combat c) ? Math.Sign(GetSeekerImpact(g.state, c) - x) : 0;
          Spr sprite_seeker = VionheartScarlet.Instance.TrickDagger_Seeker.Sprite;
          Spr sprite_seeker_angled = VionheartScarlet.Instance.TrickDagger_Seeker_Angled.Sprite;
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
      switch (missileType)
      {
        case MissileType.heavy:
          return [new GlossaryTooltip($"{VionheartScarlet.Instance.Package.Manifest.UniqueName}::Midrow::{VionheartScarlet.Instance.Localizations.Localize(["midrow", "YashaDagger_Missile", "name"])}")
          {
            Icon = VionheartScarlet.Instance.TrickDagger_Heavy_Icon.Sprite,
            TitleColor = Colors.midrow,
            Title = VionheartScarlet.Instance.Localizations.Localize(["midrow", "YashaDagger_Missile", "name"]),
            Description = VionheartScarlet.Instance.Localizations.Localize(["midrow", "YashaDagger_Missile", "description"])
          }
          ];
        case MissileType.seeker:
          return [new GlossaryTooltip($"{VionheartScarlet.Instance.Package.Manifest.UniqueName}::Midrow::{VionheartScarlet.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "name"])}")
          {
            Icon = VionheartScarlet.Instance.TrickDagger_Seeker_Icon.Sprite,
            TitleColor = Colors.midrow,
            Title = VionheartScarlet.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "name"]),
            Description = VionheartScarlet.Instance.Localizations.Localize(["midrow", "SangeDagger_Missile", "description"])
          }
          ];
        default:
          return [new GlossaryTooltip($"{VionheartScarlet.Instance.Package.Manifest.UniqueName}::Midrow::{VionheartScarlet.Instance.Localizations.Localize(["midrow", "TrickDagger_Missile", "name"])}")
          {
            Icon = VionheartScarlet.Instance.TrickDagger_Icon.Sprite,
            TitleColor = Colors.midrow,
            Title = VionheartScarlet.Instance.Localizations.Localize(["midrow", "TrickDagger_Missile", "name"]),
            Description = VionheartScarlet.Instance.Localizations.Localize(["midrow", "TrickDagger_Missile", "description"])
          }
          ];
      }
    }
    public override List<CardAction>? GetActions(State s, Combat c)
    {
      return new List<CardAction>()
      {
        new ATrickDaggerHit()
        {
          worldX = x,
          outgoingDamage = BASE_DAMAGE + this.getJuggleDamage(),
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
      TrickDagger.increaseJuggleDamage(this, 1);
      if (missileType == MissileType.heavy)
      {
        TrickDagger.increaseJuggleDamage(this, 1);
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
    public static Missile increaseJuggleDamage(this Missile data, int amount)
    {
      var _ = VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(data, "juggleDamage", 0) + amount;
      VionheartScarlet.Instance.Helper.ModData.SetModData(data, "juggleDamage", _);
      return data;
    }
    public static int getJuggleDamage(this Missile data)
    {
      return VionheartScarlet.Instance.Helper.ModData.GetModDataOrDefault(data, "juggleDamage", 0);
    }
  }
}
