namespace SimRegisPortal.Core.Extensions;

public static class CollectionExtensions
{
    public static void UpdateManyToMany<TLink, TKey>(
        this ICollection<TLink> existingLinks,
        IEnumerable<TKey> newKeys,
        Func<TLink, TKey> getKey,
        Func<TKey, TLink> createNewLink)
        where TLink : class
        where TKey : struct
    {
        var existingKeys = existingLinks.Select(getKey).ToHashSet();

        var keysToRemove = existingKeys.Except(newKeys).ToHashSet();
        var keysToAdd = newKeys.Except(existingKeys).ToHashSet();

        var linksToRemove = existingLinks.Where(link => keysToRemove.Contains(getKey(link))).ToList();
        foreach (var link in linksToRemove)
        {
            existingLinks.Remove(link);
        }

        foreach (var key in keysToAdd)
        {
            existingLinks.Add(createNewLink(key));
        }
    }
}