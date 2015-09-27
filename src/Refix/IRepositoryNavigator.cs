namespace Refix
{
    public interface IRepositoryNavigator : IRepositoryNavigable
    {
        string Name { get; }

        ItemType Type { get; }

        bool HasChildren { get; }

        bool MoveToFirstChild();

        bool MoveToChild(string name);

        bool MoveToChild(ItemType type);

        bool MoveToNext();

        bool MoveToNext(string name);

        bool MoveToNext(ItemType type);

        bool MoveToPrevious();

        bool MoveToParent();

        void MoveToRoot();
    }
}