﻿using SIS.WebServer.Routing;

namespace SIS.MvcFramework.Contracts
{
    public interface IMvcApplication
    {
        void Configure(ServerRoutingTable routing);

        void ConfigureServices();
    }
}