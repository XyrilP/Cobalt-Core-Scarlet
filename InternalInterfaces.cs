using Nickel;

namespace XyrilP.VionheartScarlet;

/* Much like a namespace, these interfaces can be named whatever you'd like.
 * We recommend using descriptive names for what they're supposed to do.
 * In this case, we use the IDemoCard interface to call for a Card's 'Register' method */
internal interface IScarletCard
{
    static abstract void Register(IModHelper helper);
}

internal interface IScarletArtifact
{
    static abstract void Register(IModHelper helper);
}