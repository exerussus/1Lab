namespace OneLab.Scripts.ECS.Core.Interfaces
{
    public interface IEasyUpdate
    {
        public void EasyUpdate();
    }
    
    public interface IEasyFixedUpdate
    {
        public void EasyFixedUpdate();
    }
    
    public interface IEasyLateUpdate
    {
        public void EasyLateUpdate();
    }
}