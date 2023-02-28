![](https://badgen.net/badge/test%20coverage/100%25/green) ![NuGet](https://img.shields.io/nuget/vpre/MathKeyboardEngine.svg)

## MathKeyboardEngine for C#

MathKeyboardEngine for C# provides the logic for a highly customizable virtual math keyboard. It is intended for use together with any LaTeX typesetting library.

Also available:
- [MathKeyboardEngine for JavaScript](https://github.com/MathKeyboardEngine/MathKeyboardEngine#readme).
- [MathKeyboardEngine for Python](https://github.com/MathKeyboardEngine/MathKeyboardEngine.Python#readme).
- [MathKeyboardEngine for Swift](https://github.com/MathKeyboardEngine/MathKeyboardEngine.Swift#readme).
- [MathKeyboardEngine for other languages](https://github.com/MathKeyboardEngine).


#### Which LaTeX typesetting libraries?

An example of a typesetting library is [KaTeX](https://katex.org/), which is meant for JavaScript. That library could, however, be used in most C# apps when loaded into a WebView (from local JavaScript and CSS files or via an internet connection). That WebView could be used to display the "output" of the key presses. For each virtual key you could use a PNG file.

If you're making a [.NET MAUI Blazor app](https://docs.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui?view=aspnetcore-6.0) or a [Blazor WebAssembly app](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor), then a WebView is automatically used for your whole app. Therefore, you can use [KaTeX](https://katex.org/) or [MathJax](https://www.mathjax.org/) after creating some JavaScript-C# glue code.

If you don't use Blazor AND your don't want to use a WebView, then you need another typesetting library. Examples per project type:

- If you're making a [WPF app](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-5.0) or Windows Forms app, then use [WPF-Math](https://github.com/ForNeVeR/wpf-math) as typesetting library.
- If you're working with UWP, non-Blazor .NET MAUI, Avalonia, Uno, etc., then use [CSharpMath](https://github.com/verybadcat/CSharpMath) as typesetting library.
- ...


#### An execution timeline

1. You load a page with your customized virtual math keyboard (based on one of the examples). On load the LaTeX for each key is typeset (by KaTeX, MathJax, WPF-Math or CSharpMath, etc.) and a cursor is displayed in a textbox-look-a-like element.
1. On your customized virtual math keyboard, you press a key. The key calls a MathKeyboardEngine function, for example `Insert(someMatrixNode)` or `MoveUp()`, `DeleteLeft()`, etc.
1. Calling `GetEditModeLatex()` outputs the total of LaTeX you typed, for example `\frac{3}{4}\blacksquare` (if `\blacksquare` is your cursor), which you then feed to the typesetting library for display.
1. Calling `GetViewModeLatex()` outputs the LaTeX without a cursor.


#### Let me test it now!

Live examples can be tested at [MathKeyboardEngine.GitHub.io](https://mathkeyboardengine.github.io).


#### Pros and cons?

<i>Unique about MathKeyboardEngine:</i>

- it supports (almost?) all math mode LaTeX, including matrices. (Please share if you know anything that is not supported.)
- its syntax tree consists of very few different parts: the `StandardLeafNode`, `StandardBranchingNode`, `AscendingBranchingNode` and `DescendingBranchingNode` can be used for almost all LaTeX, including fractions, powers, combinations, subscript, etc. with ready-to-use up/down/left/right navigation.
- it can be used with any LaTeX math typesetting library you like.

<i>A con:</i>

- this library will never be able to handle setting the cursor with the touch of a finger on a typeset formula. (But it DOES support up/down/left/right navigation and has a selection mode via arrow keys.)

<i>More pros:</i>

- you have full control over what you display on the virtual keyboard keys and what a virtual key press actually does.
- customize the editor output at runtime: dot or comma as decimal separator, cross or dot for multiplication, cursor style, colors, etc.
- this library also supports handling input from a physical keyboard, where - for example - the forward slash "/" key can be programmed to result in encapsulating a previously typed number as the numerator of a fraction. (See the examples from this C# repository or the JavaScript reporitory.)
- almost forgotten: it's open source, free to use, free to modify (please fork this repo)!


## How to use this library

In [Visual Studio](https://visualstudio.microsoft.com/downloads/)'s `Solution Explorer`, right-click a project and click `Manage NuGet Packages...`. Browse and install "MathKeyboardEngine".

Add
```csharp
using MathKeyboardEngine;
```

Then you can use:
```csharp
var latexConfiguration = new LatexConfiguration();
var keyboardMemory = new KeyboardMemory();

// Handle button clicks, etc.
```


## Documentation

Visit the [documentation](https://mathkeyboardengine.github.io/docs/csharp/0.2/) and (the right version of)\* the [Examples folder](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/tree/master/Examples) for more implementation details. There's only one example - for Blazor WebAssembly - but even if you work with something else - like Xamarin, .NET MAUI (especially .NET MAUI Blazor!), WPF or UWP - you may find parts that you want to copy.

\* If you use a version tag in the url like this: https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/tree/v0.1.1, you can see the git repository as it was for that version. That may not be needed if the [changelog](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/tree/main/CHANGELOG.md) doesn't note any important changes.


## License

The MathKeyboardEngine repositories use the most permissive licensing available. The "BSD Zero Clause License" ([0BSD](https://choosealicense.com/licenses/0bsd/)) allows for<br/>
commercial + non-commercial use, closed + open source, with + without modifications, etc. and [is equivalent](https://github.com/github/choosealicense.com/issues/805) to licenses like:

- "MIT No Attribution License" ([MIT-0](https://choosealicense.com/licenses/mit-0//)).
- "The Unlicense" ([Unlicense](https://choosealicense.com/licenses/unlicense/)).
- "CC0" ([CC0](https://choosealicense.com/licenses/cc0/)).

The "BSD Zero Clause License" ([0BSD](https://choosealicense.com/licenses/0bsd/)) does not have the condition

> (...), provided that the above copyright notice and this permission notice appear in all copies.

which is part of the "MIT License" ([MIT](https://choosealicense.com/licenses/mit/)) and its shorter equivalent "ISC License" ([ISC](https://choosealicense.com/licenses/isc/)). Apart from that they are all equivalent.


## Ask or contribute

- [ask questions](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/discussions) about anything that is not clear or when you'd like help.
- [share](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/discussions) ideas or what you've made.
- [report a bug](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/issues).
- [request an enhancement](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/issues).
- [open a pull request](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/pulls).
