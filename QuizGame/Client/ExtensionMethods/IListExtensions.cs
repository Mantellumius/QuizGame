namespace QuizGame.Client.ExtensionMethods;

public static class IListExtensions
{
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        var random = new Random();
        for (var i = list.Count; i > 0;)
        {
            var k = random.Next(i--);
            (list[i], list[k]) = (list[k], list[i]);
        }

        return list;
    }
}