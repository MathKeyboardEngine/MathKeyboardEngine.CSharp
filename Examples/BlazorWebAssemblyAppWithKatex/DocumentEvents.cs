using Microsoft.JSInterop;

namespace BlazorWebAssemblyAppWithKatex;

public static class DocumentEvents
{
    public static Func<string, Task>? OnPhysicalKeyDownHandler { get; set; }
    [JSInvokable]
    public static async Task OnPhysicalKeyDown(string key)
    {
        Console.WriteLine("Hello from static method DOWN: " + key);
        if (OnPhysicalKeyDownHandler is not null)
        {
            await OnPhysicalKeyDownHandler(key);
        }
    }
    public static Func<string, Task>? OnPhysicalKeyUpHandler { get; set; }
    [JSInvokable]
    public static async Task OnPhysicalKeyUp(string key)
    {
        Console.WriteLine("Hello from static method UP: " + key);
        if (OnPhysicalKeyUpHandler is not null)
        {
            await OnPhysicalKeyUpHandler(key);
        }
    }
}
