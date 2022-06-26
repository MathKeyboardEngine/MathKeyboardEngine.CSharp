using MathKeyboardEngine;

namespace BlazorWebAssemblyAppWithKatex;

public class PhysicalKeyHandler
{
    private readonly Func<string, bool> _keyPredicate;
    private readonly Action<KeyboardMemory, string> _actionForKeyboardMemoryAndKey;

    public PhysicalKeyHandler(string key, Action<KeyboardMemory, string> actionForKeyboardMemoryAndKey)
    {
        _keyPredicate = (inputKey) => inputKey == key;
        _actionForKeyboardMemoryAndKey = actionForKeyboardMemoryAndKey;
    }

    public PhysicalKeyHandler(Func<string, bool> keyPredicate, Action<KeyboardMemory, string> actionForKeyboardMemoryAndKey)
    {
        _keyPredicate = keyPredicate;
        _actionForKeyboardMemoryAndKey = actionForKeyboardMemoryAndKey;
    }

    public bool CanHandle(string key)
    {
        return _keyPredicate(key);
    }

    public void Handle(KeyboardMemory k, string key)
    {
        _actionForKeyboardMemoryAndKey(k, key);
    }
}
