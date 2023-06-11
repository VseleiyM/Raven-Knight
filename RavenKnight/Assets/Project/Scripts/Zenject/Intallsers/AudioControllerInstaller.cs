using Audio;
using UnityEngine;

namespace Zenject.Installers
{
    public class AudioControllerInstaller : MonoInstaller
    {
        [SerializeField] private AudioController audioControllerTemplate = null;
        public override void InstallBindings()
        {
            AudioController instance = Container
                .InstantiatePrefabForComponent<AudioController>(audioControllerTemplate);

            Container.Bind<AudioController>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
        }
    }
}