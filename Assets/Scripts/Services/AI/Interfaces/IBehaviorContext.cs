namespace Services.AI.Interfaces
{
    public interface IBehaviorContext
    {
        IBehaviorDataProvider Target { get; }
        IBehaviorDataProvider Current { get; set; }
        
    }
    public interface IBehaviorDataProvider
    {
        T Read<T>(string property);
        void Write<T>(string path, T value);
        
        /*TODO
         read only?
         properties list
         */
    }
}