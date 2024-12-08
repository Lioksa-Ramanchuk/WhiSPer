namespace Lab3.Utils;

public static class Utils
{
    public static string GetControllerName(this Type type)
    {
        var index = type.Name.LastIndexOf("Controller");
        return (index >= 0 ? type.Name.Remove(index) : type.Name).ToLower();
    }
}
