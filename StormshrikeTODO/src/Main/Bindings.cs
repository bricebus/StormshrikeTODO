﻿using Ninject.Modules;
using Ninject;
using StormshrikeTODO.Persistence;
using StormshrikeTODO.Model;

namespace StormshrikeTODO
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<Session>().To<Session>();
            //Bind<IPersistence>().To<BinFilePersistence>();
            //Bind<IPersistence>().To<XmlFilePersistence>();
            Bind<IPersistence>().To<SQLitePersistence>();
        }
    }
}