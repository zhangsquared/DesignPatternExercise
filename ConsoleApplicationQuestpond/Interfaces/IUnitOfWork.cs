namespace ConsoleApplicationQuestpond.Interfaces
{
    /// <summary>
    /// Design Pattern : Unit of Pattern pattern
    /// </summary>
    public interface IUnitOfWork
    {
        void Commit();
        void RollBack();
    }
}
