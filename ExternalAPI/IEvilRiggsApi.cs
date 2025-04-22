using System;
using System.Collections.Generic;
using Nickel;

namespace XyrilP.ExternalAPI;

public interface IEvilRiggsApi
{
    IDeckEntry EvilRiggsDeck { get; }
    IStatusEntry rage { get; }
}