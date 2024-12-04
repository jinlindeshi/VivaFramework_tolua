
using VivaFramework;

public class StartUpCommand : ControllerCommand {

    public override void Execute(IMessage message) {
        if (!Util.CheckEnvironment()) return;
        
        //-----------------初始化管理器-----------------------
        AppFacade.Instance.AddManager<LuaManager>(ManagerName.Lua);
        AppFacade.Instance.AddManager<ResourceManager>(ManagerName.Resource);
        AppFacade.Instance.AddManager<SceneManager>(ManagerName.Scene);
        AppFacade.Instance.AddManager<NetworkManager>(ManagerName.Network);
		AppFacade.Instance.AddManager<GameManager>(ManagerName.Game);
        AppFacade.Instance.AddManager<AudioManager>(ManagerName.Audio);
    }
}