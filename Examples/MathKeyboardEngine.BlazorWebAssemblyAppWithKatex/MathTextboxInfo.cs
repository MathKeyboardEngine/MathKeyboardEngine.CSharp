namespace MathKeyboardEngine.BlazorWebAssemblyAppWithKatex;

public class MathTextboxInfo
{
    public LatexConfiguration LatexConfiguration { get; set; } = new();
    public KeyboardMemory KeyboardMemory { get; set; } = new();
    public Func<Task> AfterClickProcessed { get; set; } = () => throw new ArgumentNullException($"{nameof(MathTextboxInfo)}.{nameof(AfterClickProcessed)}");
}
