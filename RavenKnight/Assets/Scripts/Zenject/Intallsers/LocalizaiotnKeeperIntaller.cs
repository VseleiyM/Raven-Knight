using Project.UI;
using UnityEngine;

namespace Zenject.Installers
{
    public class LocalizaiotnKeeperIntaller : MonoInstaller
    {
        [SerializeField] private LocalizaiotnKeeper LocalizaiotnKeeperTemplate = null;
        public override void InstallBindings()
        {
            LocalizaiotnKeeper instance = Container
                .InstantiatePrefabForComponent<LocalizaiotnKeeper>(LocalizaiotnKeeperTemplate);

            Container.Bind<LocalizaiotnKeeper>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
        }
    }
}