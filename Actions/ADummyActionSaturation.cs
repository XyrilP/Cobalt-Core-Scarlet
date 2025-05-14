using System.Collections.Generic;

namespace VionheartScarlet.Actions;

public class ADummyActionSaturation : ADummyAction
{
    public override List<Tooltip> GetTooltips(State s)
    {
        return new List<Tooltip>();
    }
}