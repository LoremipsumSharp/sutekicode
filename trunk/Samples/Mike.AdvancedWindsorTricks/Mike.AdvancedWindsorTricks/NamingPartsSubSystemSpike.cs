using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Naming;
using Castle.Windsor;

namespace Mike.AdvancedWindsorTricks
{
    public class NamingPartsSubSystemSpike
    {
       public void GetComponentsByHostname()
       {
           var container = new WindsorContainer();
           container.Kernel.AddSubSystem(SubSystemConstants.NamingKey, new NamingPartsSubSystem());

           container.Register(
                   Component.For<IThingy>().ImplementedBy<Thing1>().Named("thing:colour=red,version=1"),
                   Component.For<IThingy>().ImplementedBy<Thing2>().Named("thing:colour=blue,version=1"),
                   Component.For<IThingy>().ImplementedBy<Thing3>().Named("thing:colour=red,version=2"),
                   Component.For<IThingy>().ImplementedBy<Thing4>().Named("thing:colour=blue,version=2")
               );

           var defaultThing = container.Resolve("thing");
           Console.WriteLine("defaultThing = {0}", defaultThing.GetType().Name);

           var redThing = container.Resolve("thing:colour=red");
           Console.WriteLine("redThing.GetType().Name = {0}", redThing.GetType().Name);

           var blueThing = container.Resolve("thing:colour=blue");
           Console.Out.WriteLine("blueThing.GetType().Name = {0}", blueThing.GetType().Name);

           var version2 = container.Resolve("thing:version=2");
           Console.Out.WriteLine("version2.GetType().Name = {0}", version2.GetType().Name);

           var blueThingVersion2 = container.Resolve("thing:colour=blue,version=2");
           Console.Out.WriteLine("blueThingVersion2.GetType().Name = {0}", blueThingVersion2.GetType().Name);
       }

       private interface IThingy{}
       private class Thing1 : IThingy {}
       private class Thing2 : IThingy {}
       private class Thing3 : IThingy {}
       private class Thing4 : IThingy {}
   }
}