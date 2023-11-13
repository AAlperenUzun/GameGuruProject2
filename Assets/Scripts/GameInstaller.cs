using Interface;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlatformController>().To<PlatformController>().FromComponentInHierarchy().AsCached();
        Container.Bind<ICharacterController>().To<CharacterController>().FromComponentInHierarchy().AsCached();
        Container.Bind<SoundManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<ParticleManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<PlatformFactory>().FromComponentInHierarchy().AsCached();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<CameraController>().FromComponentInHierarchy().AsCached();
        Container.Bind<InputManager>().FromComponentInHierarchy().AsCached();
    }
}