![](https://badgen.net/badge/test%20coverage/100%25/green)

## MathKeyboardEngine for C#

MathKeyboardEngine provides the logic - in C# and LaTeX - for a highly customizable virtual math keyboard. It is intended for use together with any LaTeX typesetting library.

Also available: [MathKeyboardEngine for JavaScript](https://github.com/MathKeyboardEngine/MathKeyboardEngine).

#### Which LaTeX typesetting libraries?

- If you're making a [Blazor WebAssembly app](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor), then use for example [KaTeX](https://katex.org/) or [MathJax](https://www.mathjax.org/). You have the keyboard-logic from MathKeyboardEngine in C# and an extreme amount of LaTeX commands after just a few lines of JavaScript glue code.
- If you're making a Windows desktop application, then use for example [WPF-Math](https://github.com/ForNeVeR/wpf-math). Note that MathKeyboardEngine has its own syntax tree and that it works independently.
- In all other cases, use for example [CSharpMath](https://github.com/verybadcat/CSharpMath). CSharpMath can render LaTeX even for mobile development. Note that CSharpMath distributes several packages and that it also has its own keyboard functionality - however, you may want to choose MathKeyboardEngine for its flexibility.

#### An execution timeline

1. You load a page with your customized virtual math keyboard (examples coming soon). On load the LaTeX for each key is typeset (by KaTeX, MathJax, WPF-Math or CSharpMath, etc.) and a cursor is displayed in a textbox-look-a-like element.
1. On your customized virtual math keyboard, you press a key. The key calls a MathKeyboardEngine function, for example `Insert(someMatrixNode)` or `MoveUp()`, `DeleteCurrent()`, etc.
1. Calling `GetEditModeLatex()` outputs the total of LaTeX you typed, for example `\frac{3}{4}\blacksquare` (if `\blacksquare` is your cursor), which you then feed to the typesetting library for display.
1. Calling `GetViewModeLatex()` outputs the LaTeX without a cursor.

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
- this library also supports handling input from a physical keyboard, where - for example - the forward slash "/" key can be programmed to result in encapsulating a previously typed number as the numerator of a fraction. (No example available yet.)
- almost forgotten: it's open source, free to use, free to modify (please fork this repo)!


## How to use this library

In Visual Studio's `Solution Explorer`, right-click a project and click `Manage NuGet Packages...`. Browse "MathKeyboardEngine". (Currenlty only a pre-release/beta version is available.)

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
Note that MathKeyboardEngine for C# is a port from [MathKeyboardEngine for JavaScript](https://github.com/MathKeyboardEngine/MathKeyboardEngine) and that the documentation has not been ported (yet).

The [Examples](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/tree/master/Examples) folder does not contain much yet. Pull requests are welcome :)

## Ask or contribute

- [ask questions](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/discussions) about anything that is not clear or when you'd like help.
- [share](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/discussions) ideas or what you've made.
- [report a bug](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/issues).
- [request an enhancement](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/issues).
- [open a pull request](https://github.com/MathKeyboardEngine/MathKeyboardEngine.CSharp/pulls).
