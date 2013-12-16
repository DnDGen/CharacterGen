using D20Dice;
using Ninject.Modules;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;

namespace NPCGen.Bootstrap
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICharacterFactory>().To<CharacterFactory>();
            Bind<IDice>().ToMethod(c => DiceFactory.Create()).InSingletonScope();
        }
    }
}