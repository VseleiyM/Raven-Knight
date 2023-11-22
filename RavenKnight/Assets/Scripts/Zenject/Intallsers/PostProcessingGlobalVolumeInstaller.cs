using UnityEngine;
using UnityEngine.Rendering;

namespace Zenject.Installers
{
    public class PostProcessingGlobalVolumeInstaller : MonoInstaller
    {
        [SerializeField] private Volume volumeTemplate = null;
        public override void InstallBindings()
        {
            Volume instance = Container
                .InstantiatePrefabForComponent<Volume>(volumeTemplate);

            Container.Bind<Volume>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
        }
    }
}