using _Project.Scripts.EventSignals;
using Scripts.Services.AssetManagement;
using Scripts.Services.Pause;
using Scripts.Services.SceneLoader;
using Scripts.Services.TimerService;
using Zenject;

namespace _Project.Scripts.Installers.ProjectContext
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
           Container.BindInterfacesTo<AssetProvider>().AsSingle();
           Container.BindInterfacesTo<AsyncSceneProvider>().AsSingle();
           Container.BindInterfacesTo<PauseManager>().AsSingle();
           Container.BindInterfacesTo<TimerManager>().AsSingle();
           Container.Bind<ProjectSignals>().AsSingle();
        }
    }
}