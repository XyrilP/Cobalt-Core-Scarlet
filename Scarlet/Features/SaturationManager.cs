using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VionheartScarlet.ExternalAPI;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VionheartScarlet.Actions;
using static VionheartScarlet.ExternalAPI.IKokoroApi.IV2.IStatusRenderingApi.IHook;

namespace VionheartScarlet.Features;

public class SaturationManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public SaturationManager(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        VionheartScarlet.Instance.KokoroApi.StatusRendering.RegisterHook(this);
    }
}